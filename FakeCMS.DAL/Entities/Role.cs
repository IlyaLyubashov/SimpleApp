using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FakeCMS.DAL.Entities
{
    public class Role : IdentityRole<int>
    {
        public List<RoleLink> LinksToChildren { get; set; }

        public List<RoleLink> LinksToParents { get; set; }
    }
}
