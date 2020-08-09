using System.Collections.Generic;

namespace Intouch.Edm.Models.Dtos.UserDto
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string userName { get; set; }
        public string emailAddress { get; set; }
        public string phoneNumber { get; set; }
        public object password { get; set; }
        public bool isActive { get; set; }
        public bool shouldChangePasswordOnNextLogin { get; set; }
        public bool isTwoFactorEnabled { get; set; }
        public bool isLockoutEnabled { get; set; }
        public object jobTitleId { get; set; }
        public object jobTitle { get; set; }
        public object unitId { get; set; }
        public object unit { get; set; }
    }

    public class Role
    {
        public int roleId { get; set; }
        public string roleName { get; set; }
        public string roleDisplayName { get; set; }
        public bool isAssigned { get; set; }
    }

    public class Result
    {
        public string profilePictureId { get; set; }
        public User user { get; set; }
        public List<Role> roles { get; set; }
        public List<object> allOrganizationUnits { get; set; }
        public List<object> memberedOrganizationUnits { get; set; }
    }

    public class Root
    {
        public Result result { get; set; }
        public object targetUrl { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
        public bool __abp { get; set; }
    }


}
