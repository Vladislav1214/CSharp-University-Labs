using System.Windows;
using System.Windows.Controls;

namespace Lab_1
{
    public partial class GoogleLogin : Window
    {
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;

        public GoogleLogin()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            EmailErrorText.Visibility = Visibility.Collapsed;
            PasswordErrorText.Visibility = Visibility.Collapsed;

            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                EmailErrorText.Text = "Email cannot be empty.";
                EmailErrorText.Visibility = Visibility.Visible;
                return;
            }

            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                PasswordErrorText.Text = "Password cannot be empty.";
                PasswordErrorText.Visibility = Visibility.Visible;
                return;
            }

            this.Email = EmailTextBox.Text;
            this.Password = PasswordBox.Password;

            this.DialogResult = true;
            this.Close();
        }
    }
}
