using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackForWebEducation.Model
{
    public class Book
    {
        public int id { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public string publisher { get; set; }
        public int pages_count { get; set; }
    }
}
