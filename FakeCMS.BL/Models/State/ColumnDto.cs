using System;
using System.Collections.Generic;
using System.Text;

namespace FakeCMS.BL.Models.State
{
    public class ColumnDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Index { get; set; }

        public int BoardId { get; set; }
    }
}
