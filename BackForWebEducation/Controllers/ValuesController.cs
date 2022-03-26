using BackForWebEducation.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackForWebEducation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        MyContext context;
        public StudentController(MyContext context)
        {
            this.context = context;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Student> Get() =>
             context.Students;
        // var students = new List<Student>() { new Student() { name = "Max", group_name = "pin-191", subgroup = 1, card_number = 100, email = "md@mail.ru" } };



        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Student Get(int id) => context.Students.FirstOrDefault(x => x.id == id);


        // POST api/<ValuesController>
        [HttpPost]
        public Student Post([FromBody] Student value)
        {
            var toReturn = context.Add(value).Entity;
            context.SaveChanges();
            return toReturn;

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Student value)
        {
            value.id = id;
            if (value != null)
            {
                context.Students.Update(value);
                context.SaveChanges();
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var item = context.Students.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                context.Students.Remove(item);
                context.SaveChanges();
            }
        }
    }
}
