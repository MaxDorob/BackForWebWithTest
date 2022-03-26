using BackForWebEducation.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackForWebEducation
{
    public class Program
    {
        static string adminEmail= "mdorobolyuk.advlt@gmail.com",password ="1212k3h12j3H@";
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var services = host.Services.CreateScope().ServiceProvider;
            var userManager = services.GetRequiredService<UserManager<User>>();
            if (await userManager.FindByEmailAsync(adminEmail) == null)//Создание пользователя админа
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
