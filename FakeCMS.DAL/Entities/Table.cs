using FakeCms.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeCMS.DAL.Entities
{
    public class Table : BaseEntity
    {
        public string Name { get; set; }

        public string TypeName { get; set; }

        public List<StateTable> StateTables { get; set; }
    }
}
