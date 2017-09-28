using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StudentsMVVM.Windows
{
    public abstract class ViewModel : ObservableObject, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get { return OnValidate(columnName); }
        }

        protected virtual string OnValidate(string propertyName)
        {
            var context = new ValidationContext(this)
            {
                MemberName = propertyName
            };

            var results = new Collection<ValidationResult>();
            bool isValid = Validator.TryValidateObject(this, context, results, true);

            if (!isValid)
            {
                ValidationResult result =
                    results.SingleOrDefault(p => p.MemberNames.Any(memberName => memberName == propertyName));

                return result == null ? null : result.ErrorMessage;
            }

            return null;
        }

        [Obsolete]
        public string Error { get{throw new NotSupportedException();}}
    }
}
