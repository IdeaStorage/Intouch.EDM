using System;
using System.Collections.Generic;
using System.Text;

namespace Intouch.Edm.Models.Dtos.UserDto
{
    public class UpdateUserInfo
    {
        public class Root
        {
            public int id { get; set; }
            public string name { get; set; }
            public string surname { get; set; }
            public string emailAddress { get; set; }
            public string phoneNumber { get; set; }
            public int jobTitleId { get; set; }
            public int unitId { get; set; }
        }
    }
}