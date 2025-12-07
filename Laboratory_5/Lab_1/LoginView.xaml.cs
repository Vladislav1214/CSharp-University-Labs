using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab_1
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            User? loggedInUser = UserService.LoginUserName(username, password);

            LogInUser(loggedInUser);
        }

        private void SignUp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var registrationView = new RegistrationView();
            registrationView.Show();

            this.Close();
        }

        private void GoogleLogin_Click(object sender, RoutedEventArgs e)
        {
            var googleWindow = new GoogleLogin();

            bool? result = googleWindow.ShowDialog();

            if (result == true)
            {
                User? loggedInUser = UserService.LoginUserEmail(googleWindow.Email, googleWindow.Password);

                LogInUser(loggedInUser);
            }
        }

        private void LogInUser(User? loggedInUser)
        {
            if (loggedInUser != null)
            {
                MessageBox.Show($"Welcome, {loggedInUser.Username}!", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);

                UsernameTextBox.Text = string.Empty;
                PasswordBox.Password = string.Empty;
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
