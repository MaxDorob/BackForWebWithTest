using BackForWebEducation.Controllers;
using BackForWebEducation.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UnitTest
{
    public class StudentControllerTest
    {
        [Fact]
        public void SearchIsContainsAllElements()
        {
            var builder = new DbContextOptionsBuilder<MyContext>();

            using (MyContext context = new MyContext(builder.UseSqlServer("Server=localhost;Database=WebBackendTest;Trusted_Connection=True;").Options))
            {
                StudentController controller = new StudentController(context);
                Assert.All(controller.Get(), (x) => context.Students.Any(y => y.id == x.id));
            }
        }
        [Fact]
        public void CreateNewStudent()
        {
            Student testStudent;
            var builder = new DbContextOptionsBuilder<MyContext>();
            testStudent = new Student()
            {
                group_name = "TestGroup",
                card_number = (new Random()).Next(1000)
            };
            using (MyContext context = new MyContext(builder.UseSqlServer("Server=localhost;Database=WebBackendTest;Trusted_Connection=True;").Options))
            {

                StudentController controller = new StudentController(context);
                Assert.Equal(controller.Post(testStudent), testStudent);
                Assert.True(context.Students.Contains(testStudent));
                Assert.Contains(testStudent, context.Students.Local);
            }
            using (MyContext context = new MyContext(builder.UseSqlServer("Server=localhost;Database=WebBackendTest;Trusted_Connection=True;").Options))
            {

                Assert.True(context.Students.Contains(testStudent));
                Assert.DoesNotContain(testStudent, context.Students.Local);//Локал возвращает только Added,unchanged(Полученные, как предполагаю), modified
                if (context.Students.Contains(testStudent))
                {
                    context.Students.Remove(testStudent);
                    context.SaveChanges();
                }

            }
        }

        [Fact]
        public void PutStudentInfo()
        {
            var builder = new DbContextOptionsBuilder<MyContext>();

            var testStudent = new Student()
            {

                group_name = "TestGroup",
                card_number = (new Random()).Next(1000)
            };
            using (MyContext context = new MyContext(builder.UseSqlServer("Server=localhost;Database=WebBackendTest;Trusted_Connection=True;").Options))
            {

                StudentController controller = new StudentController(context);
                controller.Post(testStudent);
                testStudent.group_name = "NewTestGroup";
                controller.Put(testStudent.id, testStudent);
                Assert.Equal(testStudent.card_number, context.Students.First(x => x.id == testStudent.id).card_number);
            }
            using (MyContext context = new MyContext(builder.UseSqlServer("Server=localhost;Database=WebBackendTest;Trusted_Connection=True;").Options))
            {
                Assert.Equal(context.Students.First(x => x.id == testStudent.id).group_name, "NewTestGroup");
                context.Students.Remove(context.Students.First(x => x.id == testStudent.id));
                context.SaveChanges();
            }

        }

        [Fact]
        public void DeleteStudent()
        {
            var builder = new DbContextOptionsBuilder<MyContext>();
            var testStudent = new Student()
            {

                group_name = "TestGroup",
                card_number = (new Random()).Next(1000)
            };
            using (MyContext context = new MyContext(builder.UseSqlServer("Server=localhost;Database=WebBackendTest;Trusted_Connection=True;").Options))
            {
                StudentController controller = new StudentController(context);

                controller.Post(testStudent);

                controller.Delete(testStudent.id);

                Assert.DoesNotContain(testStudent, context.Students);
            }
        }
    }

}
