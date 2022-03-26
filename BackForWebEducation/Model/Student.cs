using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackForWebEducation.Model
{
    public class Student
    {
        public int id { get; set; }
        public string name { get; set; }
        public string group_name { get; set; }
        public string email { get; set; }
        public int subgroup { get; set; }
        public int card_number { get; set; }
    }
}
