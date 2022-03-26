using BackForWebEducation.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackForWebEducation.Controllers
{
    [ApiController]
    [Route("book")]
    public class BooksController : ControllerBase
    {
        MyContext context;
        public BooksController(MyContext context)
        {
            this.context = context;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            // var students = new List<Book>() { new Book() { name = "Max", group_name = "pin-191", subgroup = 1, card_number = 100, email = "md@mail.ru" } };

            return context.Books;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Book Get(int id)
        {
            return context.Books.FirstOrDefault(x => x.id == id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public Book Post([FromBody] Book value)
        {
            var toReturn = context.Add(value).Entity;
            context.SaveChanges();
            return toReturn;

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Book value)
        {
            //var book = context.Books.FirstOrDefault(x => x.id == id);
            value.id = id;
            if (value != null)
            {
                context.Books.Update(value);
                context.SaveChanges();
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var item = context.Books.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                context.Books.Remove(item);
                context.SaveChanges();
            }
        }
    }
}
