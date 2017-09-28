using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StudentsMVVM.Windows.Tests
{
    [TestClass]
    public class ObservableObjectTests
    {
        [TestMethod]
        public void PropertyChangedEvantHandlerIsRaised()
        {
            var obj = new StubObservableObject();

            bool raised = false;

            obj.PropertyChanged += (sender, e) =>
            {
                Assert.IsTrue(e.PropertyName == "ChangedProperty");
                raised = true;
            };

            obj.ChangedProperty = "Some Value";

            if (!raised)
            {
                Assert.Fail("PropertyChanged was never invoked.");
            }
        }

        private class StubObservableObject : ObservableObject
        {
            private string changedProperty;

            public string ChangedProperty
            {
                get { return this.changedProperty; }
                set
                {
                    this.changedProperty = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }

    
    
}
