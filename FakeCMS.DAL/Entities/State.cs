using FakeCms.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeCMS.DAL.Entities
{
    public class State : BaseEntity
    {   
        public string Name { get; set; }

        public List<StateTable> StateTables { get; set; }
    }
}
