using System;
using System.Management.Automation;
using CenturyLink.Cloud.Commands.Authentication.Model;

namespace CenturyLink.Cloud.Commands.Authentication
{
    [Cmdlet("Connect", "Clc")]
    [OutputType(typeof(AccountDetails))]
    public class ConnectClc : ClcCmdletBase
    {

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
            AccountDetails details = ConnectToClc();

            string message = string.Format("Connected to CLC with account alias {0}", details.Alias);
            WriteVerbose(message);

            WriteObject(details);
        }
    }
}
