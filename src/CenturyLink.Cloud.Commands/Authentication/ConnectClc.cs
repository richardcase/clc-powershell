using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using CenturyLink.Cloud.Commands.Authentication.Model;

namespace CenturyLink.Cloud.Commands.Authentication
{
    [Cmdlet("Connect", "Clc")]
    [OutputType(typeof(AccountDetails))]
    public class ConnectClc : Cmdlet
    {
        [Parameter(Position = 1, ValueFromPipeline = true)]
        public PSCredential ClcCredential { get; set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            if (ClcCredential == null)
            {
                throw new ArgumentNullException("You must supply a value for ClcCredential");
            }
        }

        protected override void ProcessRecord()
        {
            AccountDetails details = null;

            if (File.Exists(this.GetConfigPath()))
            {
                WriteVerbose("Cached CLC credential found, importing");
                try
                {
                    details = ImportConfiguration(GetConfigPath());
                }
                catch (Exception ex)
                {
                    WriteWarning("Corrupt CLC credential file, logging in again");
                    WriteWarning("\t" + ex.Message);
                }
            }

            if (details == null)
            {
                var client = new CenturyLinkCloudSDK.Client(ClcCredential.GetNetworkCredential().UserName, ClcCredential.GetNetworkCredential().Password);
                var authenticationInfo = client.Authentication;

                details = new AccountDetails
                {
                    Alias = authenticationInfo.AccountAlias,
                    Computer = Environment.MachineName,
                    Token = authenticationInfo.BearerToken,
                    TokenAcquiredOn = DateTime.UtcNow
                };

                SaveConfiguration(details, GetConfigPath());
            }

            string message = string.Format("Connected to CLC with account alias {0}", details.Alias);
            WriteVerbose(message);

            WriteObject(details);
        }


        private AccountDetails ImportConfiguration(string configPath)
        {
            PowerShell ps = PowerShell.Create();
            ps.AddCommand("Import-CliXml");
            ps.AddParameter("Path", configPath);

            AccountDetails details = ps.Invoke<AccountDetails>().FirstOrDefault();
            
            return details;
        }

        private void SaveConfiguration(AccountDetails accountDetails, string configPath)
        {
            EnsurePathExists(configPath);

            Runspace rs = RunspaceFactory.CreateRunspace();
            rs.Open();
            Pipeline pipeline = rs.CreatePipeline();

            Command export = new Command("Export-CliXml");
            export.Parameters.Add("Path", configPath);

            pipeline.Input.Write(accountDetails);
            pipeline.Commands.Add(export);
            pipeline.Invoke();
        }

        private string GetConfigPath()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return $"{appData}\\clc-powershell\\Config.ps1xml";
        }

        private void EnsurePathExists(string configPath)
        {
            string directory = Path.GetDirectoryName(configPath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

    }
}
