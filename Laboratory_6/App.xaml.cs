using Laboratory_6.Service;
using Laboratory_6.View;
using System.Windows;

namespace Laboratory_6
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static EmployeeService EmployeeService { get; private set; } = new EmployeeService();

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            EmployeeService = await EmployeeService.CreateAsync();

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }

}
