using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectors.ExternalServices.Models
{
    public class AuthUserWithRoles
    {
        public string FirstName { get; set;}
        public string FamilyName { get; set;}
        public string Email { get; set;}
        public List<RoleMappingResponse> Role { get; set;}
    }
}
