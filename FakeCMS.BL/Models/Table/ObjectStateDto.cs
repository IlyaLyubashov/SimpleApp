using FakeCMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeCMS.BL.Models.Table
{
    public class ObjectStateDto
    {
        public int ObjectId { get; set; }

        public int TableId { get; set; }

        public int? StateId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public static ObjectState Map(ObjectStateDto dto)
        {
            return new ObjectState
            {
                ObjectId = dto.ObjectId,
                StateId = dto.StateId,
                TableId = dto.TableId,
                Title = dto.Title,
                Description = dto.Description
            };
        }

        public static ObjectStateDto Map(ObjectState entity)
        {
            return new ObjectStateDto
            {
                ObjectId = entity.ObjectId,
                StateId = entity.StateId,
                TableId = entity.TableId,
                Title = entity.Title,
                Description = entity.Description
            };
        }
    }
}
