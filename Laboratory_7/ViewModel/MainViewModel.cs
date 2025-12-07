using Laboratory_7.Model;
using Laboratory_7.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Laboratory_7.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Student> Students { get; set; }
        private Student? _selectedStudent;
        private readonly IWindowService _windowService;
        private readonly DataService _dataService;

        public Student? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged(nameof(SelectedStudent));

                (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel(IWindowService windowService)
        {
            _windowService = windowService;
            Students = new ObservableCollection<Student>();
            _dataService = new DataService();
            _ = LoadDataAsync();

            AddCommand = new RelayCommand(AddStudent);
            EditCommand = new RelayCommand(EditStudent, CanEdit);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
        }

        private async Task LoadDataAsync()
        {
            var studentsFromFile = await _dataService.LoadStudentsAsync();

            Students = new ObservableCollection<Student>(studentsFromFile);

            OnPropertyChanged(nameof(Students));
        }

        private async Task SaveDataAsync()
        {
            await _dataService.SaveStudentsAsync(Students.ToList());
        }

        private async void AddStudent(object? parameter)
        {
            var newStudent = new Student();

            if (_windowService.ShowStudentEditDialog(newStudent))
            {
                Students.Add(newStudent);
                await SaveDataAsync();
            }
        }

        private async void EditStudent(object? parameter)
        {
            var tempStudent = new Student
            {
                FirstName = SelectedStudent!.FirstName,
                LastName = SelectedStudent!.LastName,
                Age = SelectedStudent!.Age,
                Gender = SelectedStudent!.Gender
            };

            if (_windowService.ShowStudentEditDialog(tempStudent))
            {
                SelectedStudent.FirstName = tempStudent.FirstName;
                SelectedStudent.LastName = tempStudent.LastName;
                SelectedStudent.Age = tempStudent.Age;
                SelectedStudent.Gender = tempStudent.Gender;
                await SaveDataAsync();
            }
        }

        private async void Delete(object? parameter)
        {
            var studentsToDelete = Students.Where(s => s.IsSelected).ToList();

            if (!studentsToDelete.Any() && SelectedStudent != null)
                studentsToDelete.Add(SelectedStudent);


            if (!studentsToDelete.Any()) return;

            var result = MessageBox.Show($"Ви впевнені, що хочете видалити {studentsToDelete.Count} студент(ів)?",
                "Підтвердження видалення", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                foreach (var student in studentsToDelete)
                    Students.Remove(student);

                await SaveDataAsync();

                (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        private bool CanEdit(object? parameter)
        {
            return Students.Any() && SelectedStudent != null;
        }

        private bool CanDelete(object? parameter)
        {
            return Students.Any() && (SelectedStudent != null || Students.Any(s => s.IsSelected));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
