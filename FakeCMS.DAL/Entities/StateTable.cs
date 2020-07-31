using FakeCms.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeCMS.DAL.Entities
{
    public class StateTable
    {
        public int StateId { get; set; }

        public int TableId { get; set; }

        public int IndexInTable { get; set; }

        public State State { get; set; }

        public Table Table { get; set; }
    }
}
