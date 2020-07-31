using System;
using System.Collections.Generic;
using System.Text;

namespace FakeCMS.DAL.Entities
{
    public class ObjectState
    {
        public int TableId { get; set; }

        public int ObjectId { get; set; }

        public int? StateId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Index { get; set; }

        public Table Table { get; set; }

        public State State { get; set; }
    }
}
