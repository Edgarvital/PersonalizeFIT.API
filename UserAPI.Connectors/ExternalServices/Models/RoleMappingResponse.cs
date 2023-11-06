using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAPI.Connectors.ExternalServices.Models
{
    public class RoleMappingResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Composite { get; set; }
        public bool ClientRole { get; set; }
        public string ContainerId { get; set; }
        public static List<string> RoleNamesToInclude { get; } = new List<string> { "student-role", "admin-role", "trainer-role" };
    }
}
