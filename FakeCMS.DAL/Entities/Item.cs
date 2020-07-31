using FakeCms.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeCMS.DAL.Entities
{
    [StatefullEntity(TableName = nameof(Item),
        TitleTemplate ="Name - [[Name]]",
        DescriptionTemplate = "Description - [[Description]]")]
    public class Item : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Value { get; set; }

        public int Count { get; set; }
    }
}
