using System;

namespace CenturyLink.Cloud.Commands.Authentication.Model
{
    public class AccountDetails
    {
        public string Alias { get; set; }

        public string Token { get; set; }

        public string Computer { get; set; }

        public DateTime TokenAcquiredOn { get; set; } 

    }
}
