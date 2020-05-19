using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FakeCMS.BL.Models.Item
{
    public class CreateItemDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int? Value { get; set; }

        [Required]
        public int? Count { get; set; }

    }
}
