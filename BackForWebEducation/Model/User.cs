using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using System.Threading.Tasks;

namespace BackForWebEducation.Model
{
    public class User:IdentityUser
    {
        public DateTime Birthday { get; set; }
        [NotMapped]
        public Guid token { get; set; } 
    }
}
