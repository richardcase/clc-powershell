using System;
using System.Collections.Generic;

namespace CenturyLink.Cloud.Commands.Infrastructure.Model
{
    class ServerDetail
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GroupId { get; set; }

        public bool IsTemplate { get; set; }

        public string LocationId { get; set; }

        public string OsType { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }

        public string StorageType { get; set; }

        public IEnumerable<IPAddress> IpAddresses { get; set; }

        //public IEnumerable<AlertPolicy> AlertPolicies { get; set; }

        public int Cpu { get; set; }

        public int DiskCount { get; set; }

        public string HostName { get; set; }

        public bool InMaintenanceMode { get; set; }

        public int MemoryMB { get; set; }

        public string PowerState { get; set; }

        public int StorageGB { get; set; }

        public IEnumerable<Disk> Disks { get; set; }

        public IEnumerable<Partition> Partitions { get; set; }

        public IEnumerable<string> Snapshots { get; set; }

        public IEnumerable<CustomField> CustomFields { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public bool IsWindows
        {
            get { return OsType.ToUpper().Contains("WINDOWS"); }
        }

    }
}
