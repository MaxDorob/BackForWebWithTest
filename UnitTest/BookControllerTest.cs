using BackForWebEducation.Controllers;
using BackForWebEducation.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTest
{
    public class BookControllerTest
    {
        [Fact]
        public void SearchIsContainsAllElements()
        {
            var builder = new DbContextOptionsBuilder<MyContext>();

            using (MyContext context = new MyContext(builder.UseSqlServer("Server=localhost;Database=WebBackendTest;Trusted_Connection=True;").Options))
            {
                BooksController controller = new BooksController(context);
                Assert.All(controller.Get(), (x) => context.Books.Any(y => y.id == x.id));
            }
        }
        [Fact]
        public void CreateNewBook()
        {
            Book testBook;
            var builder = new DbContextOptionsBuilder<MyContext>();
            testBook = new Book()
            {

                author = "TestAuthor",
                pages_count = (new Random()).Next(1000)
            };
            using (MyContext context = new MyContext(builder.UseSqlServer("Server=localhost;Database=WebBackendTest;Trusted_Connection=True;").Options))
            {

                BooksController controller = new BooksController(context);
                Assert.Equal(controller.Post(testBook), testBook);
                Assert.True(context.Books.Contains(testBook));
                Assert.Contains(testBook, context.Books.Local);
            }
            using (MyContext context = new MyContext(builder.UseSqlServer("Server=localhost;Database=WebBackendTest;Trusted_Connection=True;").Options))
            {

                Assert.True(context.Books.Contains(testBook));
                Assert.DoesNotContain(testBook, context.Books.Local);//Локал возвращает только Added,unchanged(Полученные, как предполагаю), modified
                if (context.Books.Contains(testBook))
                {
                    context.Books.Remove(testBook);
                    context.SaveChanges();
                }

            }
        }

        [Fact]
        public void PutBookInfo()
        {
            var builder = new DbContextOptionsBuilder<MyContext>();

            var testBook = new Book()
            {

                author = "TestGroup",
                pages_count = (new Random()).Next(1000)
            };
            using (MyContext context = new MyContext(builder.UseSqlServer("Server=localhost;Database=WebBackendTest;Trusted_Connection=True;").Options))
            {

                BooksController controller = new BooksController(context);
                controller.Post(testBook);
                testBook.author = "NewTestAuthor";
                controller.Put(testBook.id, testBook);
                Assert.Equal(testBook.author, context.Books.First(x => x.id == testBook.id).author);
            }
            using (MyContext context = new MyContext(builder.UseSqlServer("Server=localhost;Database=WebBackendTest;Trusted_Connection=True;").Options))
            {
                Assert.Equal(context.Books.First(x => x.id == testBook.id).author, "NewTestAuthor");
                context.Books.Remove(context.Books.First(x => x.id == testBook.id));
                context.SaveChanges();
            }

        }

        [Fact]
        public void DeleteBook()
        {
            var builder = new DbContextOptionsBuilder<MyContext>();
            var testBook = new Book()
            {

                author = "TestGroup",
                pages_count = (new Random()).Next(1000)
            };
            using (MyContext context = new MyContext(builder.UseSqlServer("Server=localhost;Database=WebBackendTest;Trusted_Connection=True;").Options))
            {
                BooksController controller = new BooksController(context);

                controller.Post(testBook);

                controller.Delete(testBook.id);

                Assert.DoesNotContain(testBook, context.Books);
            }
        }
    }
}
