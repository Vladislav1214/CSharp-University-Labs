using Laboratory_7.Service;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Laboratory_7.Model
{
    public class Student : INotifyPropertyChanged, IDataErrorInfo
    {
        private string? _lastName;
        private string? _firstName;
        private int? _age;
        private string? _gender;
        private bool _isSelected;

        public string LastName
        {
            get => _lastName ?? string.Empty;
            set
            {
                _lastName = ValidationService.FixFullNameCase(value);
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string FirstName
        {
            get => _firstName ?? string.Empty;
            set
            {
                _firstName = ValidationService.FixFullNameCase(value);
                OnPropertyChanged(nameof(FirstName));
            }

        }

        public int? Age
        {
            get => _age;
            set { _age = value; OnPropertyChanged(nameof(Age)); }
        }

        public string Gender
        {
            get => _gender ?? string.Empty;
            set { _gender = value; OnPropertyChanged(nameof(Gender)); }
        }

        [XmlIgnore]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public Student() { }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string? error = null;
                switch (columnName)
                {
                    case nameof(FirstName):
                        error = ValidationService.ValidatePartOfName(FirstName);
                        break;
                    case nameof(LastName):
                        error = ValidationService.ValidatePartOfName(LastName);
                        break;
                    case nameof(Gender):
                        if (string.IsNullOrWhiteSpace(Gender))
                            error = "Стать є обов'язковою для заповнення.";
                        break;
                    case nameof(Age):
                        error = ValidationService.ValidateAge(Age);
                        break;
                }
                return error ?? string.Empty;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
