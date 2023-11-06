using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAPI.Connectors.ExternalServices.Models
{
    public class AuthUserResponse
    {
        public string Id { get; set; }
        public long CreatedTimestamp { get; set; }
        public string Username { get; set; }
        public bool Enabled { get; set; }
        public bool TOTP { get; set; }
        public bool EmailVerified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> DisableableCredentialTypes { get; set; }
        public List<string> RequiredActions { get; set; }
        public int NotBefore { get; set; }
        public AccessModel Access { get; set; }
    }

    public class AccessModel
    {
        public bool ManageGroupMembership { get; set; }
        public bool View { get; set; }
        public bool MapRoles { get; set; }
        public bool Impersonate { get; set; }
        public bool Manage { get; set; }
    }

}
