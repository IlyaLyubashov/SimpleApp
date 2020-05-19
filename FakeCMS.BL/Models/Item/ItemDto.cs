using ItemEntity = FakeCMS.DAL.Entities.Item;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FakeCMS.BL.Models.Item
{
    public class ItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public int Count { get; set; }


        public static ItemDto FromEntity(ItemEntity item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Count = item.Count,
                Description = item.Description,
                Value = item.Value
            };
        }
    }
}
