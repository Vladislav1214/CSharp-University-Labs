using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab_1
{
    public partial class RegistrationView : Window
    {
        private User _userInProgress = new User();

        private string confirmPassword;

        public RegistrationView()
        {
            InitializeComponent();
            _userInProgress = new User();
            confirmPassword = string.Empty;
        }

        private void FinalRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPass = ConfirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != confirmPass)
            {
                MessageBox.Show("Паролі не співпадають.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (UserService.IsUsernameNotUnique(username))
            {
                MessageBox.Show($"Користувач '{username}' вже існує.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (UserService.IsEmailNotUnique(email))
            {
                MessageBox.Show($"Email '{email}' вже зареєстрований.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User newUser = new User
            {
                Username = username,
                Email = email,
                Password = PasswordHasher.Hash(password)
            };

            UserService.RegisterUser(newUser);

            MessageBox.Show("Реєстрація успішна!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);

            new LoginView().Show();
            this.Close();
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

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            new LoginView().Show();
            this.Close();
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
