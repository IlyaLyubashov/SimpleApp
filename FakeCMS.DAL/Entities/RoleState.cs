using System;
using System.Collections.Generic;
using System.Text;

namespace FakeCMS.DAL.Entities
{
    public class RoleState
    {
        public int RoleId { get; set; }

        public int StateId { get; set; }

        public Role Role { get; set; }

        public State State { get; set; }
    }
}
