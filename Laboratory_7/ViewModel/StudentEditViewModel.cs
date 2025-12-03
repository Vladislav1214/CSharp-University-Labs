using Laboratory_7.Model;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Laboratory_7
{
    public class StudentEditViewModel : INotifyPropertyChanged
    {
        public Student Student { get; private set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public StudentEditViewModel(Student student)
        {
            Student = student;

            SaveCommand = new RelayCommand(Save, CanSave);
            CancelCommand = new RelayCommand(Cancel);
        }

        private bool CanSave(object? parameter)
        {
            var propertiesToValidate = new[] { nameof(Student.FirstName), nameof(Student.LastName), nameof(Student.Age), nameof(Student.Gender) };
            return !propertiesToValidate.Any(prop => !string.IsNullOrEmpty(Student[prop]));
        }

        private void Save(object? parameter)
        {
            if (parameter is Window window)
            {
                window.DialogResult = true;
                window.Close();
            }
        }

        private void Cancel(object? parameter)
        {
            if (parameter is Window window)
            {
                window.DialogResult = false;
                window.Close();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
