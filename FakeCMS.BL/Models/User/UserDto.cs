using System;
using System.Collections.Generic;
using System.Text;
using UserEntity = FakeCMS.DAL.Entities.User;

namespace FakeCMS.BL.Models.User
{
    public class UserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public static UserDto FromEntity(UserEntity user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }
    }
}
