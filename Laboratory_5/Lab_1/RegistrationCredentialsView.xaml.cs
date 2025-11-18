using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab_1
{
    public partial class RegistrationCredentialsView : Window
    {
        private User _userInProgress;

        private string confirmPassword;

        public RegistrationCredentialsView(User userFromStep1)
        {
            InitializeComponent();
            _userInProgress = userFromStep1;
            confirmPassword = string.Empty;
        }

        private void FinalRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Username cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(_userInProgress.Email) && UserService.UserUniqueEmail(_userInProgress))
            {
                MessageBox.Show($"Email cannot be empty or this email '{_userInProgress.Email}' exists", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(_userInProgress.Password))
            {
                MessageBox.Show("Password cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_userInProgress.Password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _userInProgress.Password = PasswordHasher.Hash(_userInProgress.Password);

            _userInProgress.Username = username;

            bool isRegistered = UserService.RegisterUser(_userInProgress);

            if (isRegistered)
            {
                MessageBox.Show("Registration successful! You can now log in.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                var loginView = new LoginView();
                loginView.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show($"Username '{username}' is already taken. Please choose another one.", "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                switch (textBox.Name)
                {
                    case "EmailTextBox":
                        ValidateEmail();
                        break;
                }
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                switch (passwordBox.Name)
                {
                    case "PasswordBox":
                        ValidatePassword();
                        break;

                    case "ConfirmPasswordBox":
                        ValidateConfirmPassword();
                        break;
                }
            }
        }

        private void ValidateEmail()
        {
            EmailErrorText.Visibility = Visibility.Collapsed;
            EmailTextBox.BorderBrush = SystemColors.ControlDarkBrush;

            string? error = ValidationService.ValidateEmail(EmailTextBox.Text);
            if (error != null)
            {
                EmailErrorText.Text = error;
                EmailErrorText.Visibility = Visibility.Visible;
                EmailTextBox.BorderBrush = Brushes.Red;
            }
            else _userInProgress.Email = EmailTextBox.Text;
        }

        private void ValidatePassword()
        {
            PasswordErrorText.Visibility = Visibility.Collapsed;
            PasswordBox.BorderBrush = SystemColors.ControlDarkBrush;

            string? error = ValidationService.ValidatePassword(PasswordBox.Password);
            if (error != null)
            {
                PasswordErrorText.Text = error;
                PasswordErrorText.Visibility = Visibility.Visible;
                PasswordBox.BorderBrush = Brushes.Red;
            }
            else _userInProgress.Password = PasswordBox.Password;
        }

        private void ValidateConfirmPassword()
        {
            ConfirmPasswordErrorText.Visibility = Visibility.Collapsed;
            ConfirmPasswordBox.BorderBrush = SystemColors.ControlDarkBrush;

            if (_userInProgress.Password != ConfirmPasswordBox.Password)
            {
                ConfirmPasswordErrorText.Text = "Паролі не збігаються.";
                ConfirmPasswordErrorText.Visibility = Visibility.Visible;
                ConfirmPasswordBox.BorderBrush = Brushes.Red;
            }
            else confirmPassword = ConfirmPasswordBox.Password;
        }
    }
}
