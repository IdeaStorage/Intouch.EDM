using System;
using System.Collections.Generic;
using System.Text;

namespace Intouch.Edm.Models.Dtos.TaskOptionUpdateDto
{
    public class TaskOptionUpdateDto
    {
    }

    public class CheckedOption
    {
        public bool completed { get; set; }
        public int id { get; set; }
    }

    public class RootObject
    {
        public List<CheckedOption> checkedOptions { get; set; }
        public string description { get; set; }
        public string id { get; set; }
    }
}
