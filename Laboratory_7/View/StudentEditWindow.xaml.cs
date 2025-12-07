using System.Windows;

namespace Laboratory_7.View
{
    /// <summary>
    /// Interaction logic for StudentEditWindow.xaml
    /// </summary>
    public partial class StudentEditWindow : Window
    {
        public StudentEditWindow(StudentEditViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
