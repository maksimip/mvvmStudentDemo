using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StudentsMVVM.Data
{
    public class StudentsContext
    {
        private static string xmlFilePath = @"d:\dev\C#\StudentsMVVM\Students.xml";
        public XDocument xDoc;

        public StudentsContext()
        {
            xDoc = XDocument.Load(xmlFilePath);
        }

        public IEnumerable<Student> GetStudents()
        {
            xDoc = XDocument.Load(xmlFilePath);

            foreach (var item in xDoc.Root.Elements("Student"))
            {
                var id = (int) item.Attribute("Id");
                var firstName = (string) item.Element("FirstName");
                var last = (string) item.Element("Last");
                var age = (string) item.Element("Age");
                var gender = (string) item.Element("Gender");

                yield return new Student()
                {
                    Id = id,
                    FirstName = firstName,
                    Last = last,
                    Age = age,
                    Gender = gender
                };
            }
        }

        public void CreateStudent(Student student)
        {
            xDoc = XDocument.Load(xmlFilePath);

            xDoc.Element("Students").Add(new XElement("Student",
                new XAttribute("Id", student.Id),
                new XElement("FirstName", student.FirstName),
                new XElement("Last", student.Last),
                new XElement("Age", student.Age),
                new XElement("Gender", student.Gender)));

            xDoc.Save(xmlFilePath);
        }

        public void UpdateStudent(Student student)
        {
            xDoc = XDocument.Load(xmlFilePath);

            foreach (var item in xDoc.Root.Elements("Student"))
            {
                if ((int)item.Attribute("Id") == student.Id)
                {
                    item.Element("FirstName").Value = student.FirstName;
                    item.Element("Last").Value = student.Last;
                    item.Element("Age").Value = student.Age;
                    item.Element("Gender").Value = student.Gender;
                }
            }

            xDoc.Save(xmlFilePath);
          
        }

        public void Test()
        {
            
        }
    }
}
