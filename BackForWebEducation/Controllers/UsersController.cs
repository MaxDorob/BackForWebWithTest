using BackForWebEducation.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackForWebEducation.Controllers
{
    [Route("login")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        UserManager<User> userManager;
        SignInManager<User> signInManager;
        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<UsersController>/5
        //[HttpGet("{id}")]
        //public async Task<JsonResult> Get(int id)
        //{
        //    return await Post(new AuthRequest() { email = "mdorobolyuk.advlt@gmail.com", password = "1212k3h12j3H@" });
        //}

        // POST api/<UsersController>
        [HttpPost]
        public async Task<JsonResult> Post([FromBody] AuthRequest value)
        {
            var user = await userManager.FindByEmailAsync(value.email as string);
            if (user != null)
            {
                if ((await signInManager.CheckPasswordSignInAsync(user, value.password as string, false)).Succeeded)
                {
                    user.token = Guid.NewGuid();
                    var contractResolver = new DynamicContractResolver();
                    AuthResponse response = new AuthResponse() {
                        ttl = int.MaxValue.ToString(),
                        refresh_ttl = int.MaxValue.ToString(),
                        
                        user = user
                    };
                    var toReturn = new JsonResult(response, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver=contractResolver});




                    return toReturn;
                }
            }
            return null;
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        public class DynamicContractResolver : DefaultContractResolver//Необходим для переопределения формирования js-файла
        {
            private readonly List<string> PropertyNames = new List<string>() { "Name","UserName", "Id", "Email","user","token", "refresh_ttl", "ttl" };
            public DynamicContractResolver()
            {
            }
            protected override string ResolvePropertyName(string propertyName)
            {
                switch (propertyName)
                {
                    case "UserName":
                        return "name";
                    case "Email":
                        return "email";
                    default:
                        
                return base.ResolvePropertyName(propertyName);
                }
            }
            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

                // only serializer properties that start with the specified character
                
                properties =
                  properties.Where(p => PropertyNames.Select(x=>x.ToLower()).Contains(p.PropertyName.ToLower())).ToList();

                return properties;
            }
        }
    }
}
