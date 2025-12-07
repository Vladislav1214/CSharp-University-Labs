using Laboratory_6.Model;
using Laboratory_6.Service;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Laboratory_6.View
{
    public partial class CandidateEditPage : Page
    {
        private readonly Employee _originalEmployee;
        private readonly bool _isEditMode;

        private readonly ObservableCollection<Language> _selectedLanguages = new ObservableCollection<Language>();

        public CandidateEditPage()
        {
            InitializeComponent();
            _isEditMode = false;
            _originalEmployee = new Employee();
            SetupPage();
        }

        public CandidateEditPage(Employee employeeToEdit)
        {
            InitializeComponent();
            _isEditMode = true;
            _originalEmployee = employeeToEdit;
            SetupPage();
        }

        private void SetupPage()
        {
            PageTitle.Text = _isEditMode ? "Редагування кандидата" : "Додавання нового кандидата";

            EducationComboBox.ItemsSource = Enum.GetValues(typeof(EducationLevel));
            ComputerLiteracyComboBox.ItemsSource = EnumHelper.GetEnumValues<ComputerLiteracyLevel>();
            LanguagesToAddComboBox.ItemsSource = new List<string> { "Англійська", "Німецька", "Французька" };

            _selectedLanguages.Clear();
            foreach (var lang in _originalEmployee.KnowLanguage)
            {
                _selectedLanguages.Add(lang);
            }
            SelectedLanguagesItemsControl.ItemsSource = _selectedLanguages;

            if (_isEditMode)
            {
                FullNameTextBox.Text = _originalEmployee.FullName;
                DateOfBirthPicker.SelectedDate = _originalEmployee.DateOfBirth;
                EducationComboBox.SelectedItem = _originalEmployee.Education;
                WorkExperienceTextBox.Text = _originalEmployee.WorkExperience.ToString();
                ComputerLiteracyComboBox.SelectedItem = _originalEmployee.ComputerLiteracy.ToString().Replace('_', ' ');
                RecommendationsCheckBox.IsChecked = _originalEmployee.Recommendations;
            }
        }

        private void AddLanguageButton_Click(object sender, RoutedEventArgs e)
        {
            if (LanguagesToAddComboBox.SelectedItem is string languageName)
            {
                if (!_selectedLanguages.Any(l => l.LanguageName == languageName))
                {
                    _selectedLanguages.Add(new Language { LanguageName = languageName, FluentIn = LanguageProficiency.Зі_словником });
                }
            }
        }

        private void RemoveLanguageButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is Language languageToRemove)
            {
                _selectedLanguages.Remove(languageToRemove);
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool isFullNameValid = ValidateFullName();
            bool isWorkExperienceValid = ValidateWorkExperience();
            bool isDateValid = ValidateDateOfBirth();


            if (!isFullNameValid || !isWorkExperienceValid || !isDateValid)
            {
                MessageBox.Show("Будь ласка, виправте помилки у формі.", "Помилка валідації");
                return;
            }

            var updatedEmployee = new Employee
            {
                FullName = ValidationService.FixName(FullNameTextBox.Text),
                DateOfBirth = DateOfBirthPicker.SelectedDate.Value,
                Education = (EducationLevel)EducationComboBox.SelectedItem,
                WorkExperience = int.Parse(WorkExperienceTextBox.Text),
                ComputerLiteracy = EnumHelper.GetEnumFromObject<ComputerLiteracyLevel>(ComputerLiteracyComboBox.SelectedItem),
                Recommendations = RecommendationsCheckBox.IsChecked ?? false,
                KnowLanguage = new List<Language>(_selectedLanguages)
            };

            if (_isEditMode)
                App.EmployeeService.UpdateEmployee(_originalEmployee, updatedEmployee);
            else
                App.EmployeeService.AddEmployee(updatedEmployee);

            await App.EmployeeService.SaveEmployeesAsync();

            this.NavigationService?.GoBack();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.GoBack();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                switch (textBox.Name)
                {
                    case "FullNameTextBox":
                        ValidateFullName();
                        break;

                    case "WorkExperienceTextBox":
                        ValidateWorkExperience();
                        break;
                }
            }
            else if (sender is DatePicker datePicker)
            {
                if (datePicker.Name == "DateOfBirthPicker")
                {
                    ValidateDateOfBirth();
                }
            }
        }

        private bool ValidateDateOfBirth()
        {
            DateOfBirthPickerErrorText.Visibility = Visibility.Collapsed;
            DateOfBirthPicker.BorderBrush = SystemColors.ControlDarkBrush;

            string? error = ValidationService.ValidateDateOfBirth(DateOfBirthPicker.SelectedDate.Value);
            if (error != null)
            {
                DateOfBirthPickerErrorText.Text = error;
                DateOfBirthPickerErrorText.Visibility = Visibility.Visible;
                DateOfBirthPicker.BorderBrush = Brushes.Red;
                return false;
            }

            return true;
        }


        private bool ValidateWorkExperience()
        {
            WorkExperienceErrorText.Visibility = Visibility.Collapsed;
            WorkExperienceTextBox.BorderBrush = SystemColors.ControlDarkBrush;

            string? error = ValidationService.ValidateWorkExperience(WorkExperienceTextBox.Text);
            if (error != null)
            {
                WorkExperienceErrorText.Text = error;
                WorkExperienceErrorText.Visibility = Visibility.Visible;
                WorkExperienceTextBox.BorderBrush = Brushes.Red;
                return false;
            }

            return true;
        }

        private bool ValidateFullName()
        {
            FullNameErrorText.Visibility = Visibility.Collapsed;
            FullNameTextBox.BorderBrush = SystemColors.ControlDarkBrush;

            string? error = ValidationService.ValideFullName(FullNameTextBox.Text);
            if (error != null)
            {
                FullNameErrorText.Text = error;
                FullNameErrorText.Visibility = Visibility.Visible;
                FullNameTextBox.BorderBrush = Brushes.Red;
                return false;
            }

            return true;
        }
    }
}
