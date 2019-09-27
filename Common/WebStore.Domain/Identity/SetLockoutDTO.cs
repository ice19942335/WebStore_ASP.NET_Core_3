using System;

namespace WebStore.Domain.Identity
{
    public class SetLockoutDTO : UserInfoDTO
    {
        public DateTimeOffset? LockoutStart { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}