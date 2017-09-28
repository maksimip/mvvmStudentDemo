using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentsMVVM.Data;
using StudentsMVVM.DesktopClient.ViewModels;

namespace StudentsMVVM.DesktopClient.Tests.UnitTests
{
    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
        public void GetStudentListCommand_PopulatesStudentsPropertyWithExpectedCollectionFromXML()
        {
            var context = new StudentsContext();

            var viewModel = new MainViewModel(context);

            viewModel.GetStudentListCommand.Execute(null);

            Assert.IsTrue(viewModel.Students.Count == 10);
        }
    }
}
