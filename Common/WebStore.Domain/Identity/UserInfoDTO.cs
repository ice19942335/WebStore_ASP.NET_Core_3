using System;
using System.Text;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Domain.Identity
{
    public abstract class UserInfoDTO
    {
        public User User { get; set; }
    }
}
