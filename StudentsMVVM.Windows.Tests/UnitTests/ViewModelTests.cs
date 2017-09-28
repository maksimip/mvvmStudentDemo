using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StudentsMVVM.Windows.Tests
{
    [TestClass]
    public class ViewModelTests
    {
        [TestMethod]
        public void IsAbstractBaseClass()
        {
            Type t = typeof(ViewModel);

            Assert.IsTrue(t.IsAbstract);
        }

        [TestMethod]
        public void IsIDataErrorInfo()
        {
            Assert.IsTrue(typeof(IDataErrorInfo).IsAssignableFrom(typeof(ViewModel)));
        }

        [TestMethod]
        public void IsObservableObject()
        {
            Assert.IsTrue(typeof(ViewModel).BaseType == typeof(ObservableObject));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void IDataErrorInfo_ErrorProperty_IsNotSupported()
        {
            var viewModel = new StubViewModel();
            string value = viewModel.Error;
        }

        [TestMethod]
        public void IndexerValidatesPropertyNameWithInvalidValue()
        {
            var viewModel = new StubViewModel();
            Assert.IsNotNull(viewModel["RequiredProperty"]);
        }

        [TestMethod]
        public void IndexerValidatesPropertyNameWithValidValue()
        {
            var viewModel = new StubViewModel()
            {
                RequiredProperty = "Some Value"
            };

            Assert.IsNull(viewModel["RequiredProperty"]);
        }

        [TestMethod]
        public void IndexerReturnsErrorMessageForRequestedInvalidProperty()
        {
            var viewModel = new StubViewModel()
            {
                RequiredProperty = null,
                SomeOtherProperty = null
            };

            var msg = viewModel["SomeOtherProperty"];

            Assert.AreEqual("The SomeOtherProperty field is required.", msg);
        }

        private class StubViewModel : ViewModel
        {
            [Required]
            public string RequiredProperty { get; set; }

            [Required]
            public string SomeOtherProperty { get; set; }
        }
    }
}
