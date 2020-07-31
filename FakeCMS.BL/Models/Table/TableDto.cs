using System;
using System.Collections.Generic;
using System.Text;
using TableEntity = FakeCMS.DAL.Entities.Table;

namespace FakeCMS.BL.Models.Table
{
    public class TableDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static TableDto Map(TableEntity entity)
        {
            return new TableDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
