using System.Linq;
using CenturyLinkCloudSDK.ServiceModels;
using ServerDetail = CenturyLink.Cloud.Commands.Infrastructure.Model.ServerDetail;

namespace CenturyLink.Cloud.Commands.Infrastructure.Translator
{
    internal class ServerTranslator : ITranslator<CenturyLinkCloudSDK.ServiceModels.Server, ServerDetail>
    {
        public ServerDetail Translate(Server source)
        {
            ServerDetail detail = new ServerDetail
            {
                Cpu = source.Details.Cpu,
                CreatedBy = source.ChangeInfo.CreatedBy,
                CreatedDate = source.ChangeInfo.CreatedDate,
                //CustomFields = source.Details.CustomFields,
                Description = source.Description,
                DiskCount = source.Details.DiskCount,
                //Disks =  source.Details.Disks,
                GroupId = source.GroupId,
                HostName = source.Details.HostName,
                Id = source.Id,
                InMaintenanceMode = source.Details.InMaintenanceMode,
                //IpAddresses = source.Details.IpAddresses,
                IsTemplate = source.IsTemplate,
                LocationId = source.LocationId,
                MemoryMB = source.Details.MemoryMB,
                ModifiedBy = source.ChangeInfo.ModifiedBy,
                ModifiedDate = source.ChangeInfo.ModifiedDate,
                Name = source.Name,
                OsType = source.OsType,
                //Partitions = source.Details.Partitions,
                PowerState = source.Details.PowerState,
                Snapshots = source.Details.Snapshots.Select(s => s.Name),
                Status = source.Status,
                StorageGB = source.Details.StorageGB,
                StorageType = source.StorageType,
                Type = source.Type
            };

            return detail;
        }
    }
}
