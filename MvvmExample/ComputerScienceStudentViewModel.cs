using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmExample
{
    public class ComputerScienceStudentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Student
        /// </summary>
        private Student CSStudent { get; init; }

        public string FirstName => CSStudent.FirstName;

        public string LastName => CSStudent.LastName;

        public double GPA => CSStudent. GPA;

        public IEnumerable<CourseRecord> CourseRecords => CSStudent.CourseRecords; 

        public double CSGPA
        {
            get
            {
                var points = 0.0;
                var hours = 0.0;
                foreach (var cr in CSStudent.CourseRecords)
                {
                    if (cr.CourseName.Contains("CIS"))
                    {
                        points += (double)cr.Grade * cr.CreditHours;
                        hours += cr.CreditHours;
                    }
                }
                return points / hours;
            }
        }

        private void HandleStudentPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Student.GPA))
            {
                PropertyChanged?.Invoke(this, e);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CSGPA)));
            }
        }

       public ComputerScienceStudentViewModel(Student student)
        {
            CSStudent = student;
            student.PropertyChanged += HandleStudentPropertyChanged;
        }

    }
}
