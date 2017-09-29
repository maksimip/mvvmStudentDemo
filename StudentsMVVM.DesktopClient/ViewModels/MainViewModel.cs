using StudentsMVVM.Windows;
using StudentsMVVM.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StudentsMVVM.DesktopClient.ViewModels
{
    
    public class MainViewModel : ViewModel
    {
        private readonly StudentsContext context;
        private Student selectedStudent;

        public MainViewModel() : this(new StudentsContext())
        {

        }

        public MainViewModel(StudentsContext context)
        {
            Students = new ObservableCollection<Student>();
            this.context = context;
            GetStudentListCommand.Execute(null);
        }

        public bool CanModify
        {
            get { return SelectedStudent != null; }
        }

        public ICollection<Student> Students
        {
            get;
            private set;
        }

        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set
            {
                selectedStudent = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("CanModify");
            }
        }

        public bool IsValid
        {
            get
            {
                return SelectedStudent == null ||
                       (
                           !String.IsNullOrWhiteSpace(SelectedStudent.FirstName) &&
                           !String.IsNullOrWhiteSpace(SelectedStudent.Last)&&
                           !String.IsNullOrWhiteSpace(SelectedStudent.Age)&&
                           !String.IsNullOrWhiteSpace(SelectedStudent.Gender)
                       );
            }
        }

        public ICommand GetStudentListCommand
        {
            get { return new ActionCommand(p => GetStudentList());}
        }

        private void GetStudentList()
        {
            Students.Clear();
            SelectedStudent = null;

            foreach (var student in context.GetStudents())
            {
                Students.Add(student);
            }
        }
    }
}
