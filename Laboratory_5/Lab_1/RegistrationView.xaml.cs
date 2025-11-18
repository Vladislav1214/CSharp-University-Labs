using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Lab_1
{
    public partial class RegistrationView : Window
    {
        User regUser;

        public RegistrationView()
        {
            InitializeComponent();
            regUser = new User();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(regUser.FirstName) || string.IsNullOrWhiteSpace(regUser.LastName))
            {
                MessageBox.Show("First Name and Last Name are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(regUser.DateOfBirth))
            {
                MessageBox.Show("Date Of Birth are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            regUser.Address = AddressTextBox.Text;
            regUser.DescribeYourself = DescribeYourselfTextBox.Text;
            regUser.Gender = (MaleRadioButton.IsChecked == true) ? "Male" : (FemaleRadioButton.IsChecked == true) ? "Female" : "";

            var credentialsWindow = new RegistrationCredentialsView(regUser);
            credentialsWindow.Show();

            this.Close();
        }

        private void Login_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();

            this.Close();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                switch (textBox.Name)
                {
                    case "FirstNameTextBox":
                        ValidateFirstName();
                        break;

                    case "LastNameTextBox":
                        ValidateLastName();
                        break;

                    case "DateOfBirthTextBox":
                        ValidateDate();
                        break;

                    case "ZipCodeTextBox":
                        ValidateZipCode();
                        break;

                    case "IdNumberTextBox":
                        ValidateIdNumber();
                        break;
                }
            }
            else return;
        }

        private void ValidateFirstName()
        {
            FirstNameErrorText.Visibility = Visibility.Collapsed;
            FirstNameTextBox.BorderBrush = SystemColors.ControlDarkBrush;

            string? error = ValidationService.ValidatePartOfName(FirstNameTextBox.Text);
            if (error != null)
            {
                FirstNameErrorText.Text = error;
                FirstNameErrorText.Visibility = Visibility.Visible;
                FirstNameTextBox.BorderBrush = Brushes.Red;
            }
            else regUser.FirstName = ValidationService.FixFullNameCase(FirstNameTextBox.Text);
        }

        private void ValidateLastName()
        {
            LastNameErrorText.Visibility = Visibility.Collapsed;
            LastNameTextBox.BorderBrush = SystemColors.ControlDarkBrush;

            string? error = ValidationService.ValidatePartOfName(LastNameTextBox.Text);
            if (error != null)
            {
                LastNameErrorText.Text = error;
                LastNameErrorText.Visibility = Visibility.Visible;
                LastNameTextBox.BorderBrush = Brushes.Red;
            }
            else regUser.LastName = ValidationService.FixFullNameCase(LastNameTextBox.Text);
        }

        private void ValidateDate()
        {
            DateOfBirthErrorText.Visibility = Visibility.Collapsed;
            DateOfBirthTextBox.BorderBrush = SystemColors.ControlDarkBrush;

            string? error = ValidationService.ValidateDateOfBirth(DateOfBirthTextBox.Text);
            if (error != null)
            {
                DateOfBirthErrorText.Text = error;
                DateOfBirthErrorText.Visibility = Visibility.Visible;
                DateOfBirthTextBox.BorderBrush = Brushes.Red;
            }
            else regUser.DateOfBirth = ValidationService.FixFullNameCase(DateOfBirthTextBox.Text);
        }

        private void ValidateZipCode()
        {
            ZipCodeErrorText.Visibility = Visibility.Collapsed;
            ZipCodeTextBox.BorderBrush = SystemColors.ControlDarkBrush;

            string? error = ValidationService.ValidateCode(ZipCodeTextBox.Text);
            if (error != null)
            {
                ZipCodeErrorText.Text = error;
                ZipCodeErrorText.Visibility = Visibility.Visible;
                ZipCodeTextBox.BorderBrush = Brushes.Red;
            }
            else regUser.ZipCode = ValidationService.FixFullNameCase(ZipCodeTextBox.Text);
        }

        private void ValidateIdNumber()
        {
            IdNumberErrorText.Visibility = Visibility.Collapsed;
            IdNumberTextBox.BorderBrush = SystemColors.ControlDarkBrush;

            string? error = ValidationService.ValidateId(IdNumberTextBox.Text);
            if (error != null)
            {
                IdNumberErrorText.Text = error;
                IdNumberErrorText.Visibility = Visibility.Visible;
                IdNumberTextBox.BorderBrush = Brushes.Red;
            }
            else regUser.IdNumber = ValidationService.FixFullNameCase(IdNumberTextBox.Text);
        }
    }
}
