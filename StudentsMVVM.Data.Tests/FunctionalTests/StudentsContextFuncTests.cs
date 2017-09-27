using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace StudentsMVVM.Data.Tests.FunctionalTests
{
    [TestClass]
    public class StudentsContextFuncTests
    {
        [TestMethod]
        public void GetStudentsList_ReturnsListOfStudentEntities()
        {
            //Arrange
            StudentsContext studentsContext = new StudentsContext();

            //Act
            var list = studentsContext.GetStudents();

            int countExpected = list.Count();

            //Assert
            Assert.IsTrue(countExpected == 8);
        }

        [TestMethod]
        public void CreateStudent_AddNewStudentElementInXML()
        {
            //arrrange
            StudentsContext context = new StudentsContext();
            int maxId = context.xDoc.Root.Elements("Student").Max(s => (int) s.Attribute("Id"));
            
            Student newStudent = new Student()
            {
                Id = ++maxId,
                FirstName = "Smit",
                Last = "Uilson",
                Age = "32",
                Gender = "0"
            };

            //Act
            context.CreateStudent(newStudent);
            bool exist = context.xDoc.Root.Elements("Student").Any(s => (int) s.Attribute("Id") == newStudent.Id);
            //Assert
            Assert.IsTrue(exist);
        }

        [TestMethod]
        public void UpdateStudent_EditElementStudentInXML()
        {
            //Arrange
            StudentsContext context = new StudentsContext();

            Student student = new Student()
            {
                Id = 9,
                FirstName = "Smit",
                Last = "Robenson",
                Age = "32",
                Gender = "0"
            };

            //act
            context.UpdateStudent(student);

            //Assert
            var element = context.xDoc.Root.Elements("Student").Select(s => s).Where(s => (int)s.Attribute("Id") == 9)
                .First();
            
            Assert.IsTrue(element.Element("FirstName").Value == student.FirstName);
            Assert.IsTrue(element.Element("Last").Value == student.Last);
            Assert.IsTrue(element.Element("Age").Value == student.Age);
            Assert.IsTrue(element.Element("Gender").Value == student.Gender);

        }
    }
}
