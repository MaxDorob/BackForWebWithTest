using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackForWebEducation.Model
{
    public class MyContext: IdentityDbContext<User>
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            if (Database.EnsureCreated())   // создаем базу данных при первом обращении. Миграция: https://metanit.com/sharp/entityframeworkcore/2.15.php
            {
                
                Students.Add(
                new Student()
                {
                    name = "Max",
                    group_name = "pin-191",
                    subgroup = 1,
                    card_number = 100,
                    email = "md@mail.ru"
                });
                this.SaveChanges();
            }
        }
    }
}
