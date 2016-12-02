using System.Collections.Generic;

namespace CenturyLink.Cloud.Commands.Infrastructure.Model
{
    public class Disk
    {
        public string Id { get; set; }

        public float SizeGB { get; set; }

        public IEnumerable<string> PartitionPaths { get; set; }
    }
}
