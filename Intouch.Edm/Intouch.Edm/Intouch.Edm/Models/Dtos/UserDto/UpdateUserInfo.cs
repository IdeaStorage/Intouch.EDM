using System;
using System.Collections.Generic;
using System.Text;

namespace Intouch.Edm.Models.Dtos.UserDto
{
    public class UpdateUserInfo
    {
        public class User
        {
            public int id { get; set; }
            public string name { get; set; }
            public string surname { get; set; }
            public string userName { get; set; }
            public string emailAddress { get; set; }
            public string phoneNumber { get; set; }
            public string password { get; set; }
            public bool isActive { get; set; }
            public bool shouldChangePasswordOnNextLogin { get; set; }
            public bool isTwoFactorEnabled { get; set; }
            public bool isLockoutEnabled { get; set; }
            public int jobTitleId { get; set; }
            public string jobTitle { get; set; }
            public int unitId { get; set; }
            public string unit { get; set; }
        }

        public class Root
        {
            public User user { get; set; }
            public List<string> assignedRoleNames { get; set; }
            public bool sendActivationEmail { get; set; }
            public bool setRandomPassword { get; set; }
            public List<int> organizationUnits { get; set; }
        }
    }

}
