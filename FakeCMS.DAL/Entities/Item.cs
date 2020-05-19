using FakeCms.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeCMS.DAL.Entities
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Value { get; set; }

        public int Count { get; set; }
    }
}
