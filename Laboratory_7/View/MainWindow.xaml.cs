using Laboratory_7.Service;
using Laboratory_7.ViewModel;
using System.Windows;

namespace Laboratory_7.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            IWindowService windowService = new WindowService();

            MainViewModel viewModel = new MainViewModel(windowService);

            this.DataContext = viewModel;
        }
    }
}