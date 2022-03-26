using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackForWebEducation.Model
{
    public class AuthResponse//Структура для удобства
    {
        public string token { get; set; }
        public string ttl { get; set; }
        public string refresh_ttl { get; set; }
        public User user { get; set; }
    }
}
