using System.Management.Automation;
using CenturyLink.Cloud.Commands.Infrastructure.Translator;
using CenturyLinkCloudSDK.ServiceModels;

namespace CenturyLink.Cloud.Commands.Infrastructure
{
    [Cmdlet(VerbsCommon.Get, "ClcServers")]
    public class GetServers : ClcCmdletBase
    {
        [Parameter(Position = 1, ValueFromPipeline = true, Mandatory = true)]
        public string DataCenter { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var client = GetClcClient();
            var dc = client.DataCenters.GetDataCenter(this.DataCenter, false).Result;
            var rootGroupId = dc.RootGroupId;

            var group = client.Groups.GetGroup(rootGroupId).Result;

            var servers = group.GetServers(true).Result;

            ServerTranslator translator = new ServerTranslator();
            foreach (Server server in servers)
            {
                //TODO: this needs to be changed to a model
                WriteObject(translator.Translate(server));
            }

        }
    }
}
