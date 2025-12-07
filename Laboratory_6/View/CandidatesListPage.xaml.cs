using Laboratory_6.Model;
using Laboratory_6.Service;
using System.Windows;
using System.Windows.Controls;


namespace Laboratory_6.View
{
    public partial class CandidatesListPage : Page
    {
        public CandidatesListPage()
        {
            InitializeComponent();
            LoadCandidates();

            this.Loaded += CandidatesListPage_Loaded;
        }

        private void LoadCandidates()
        {
            EducationFilterComboBox.ItemsSource = EnumHelper.GetEnumValuesWithAll<EducationLevel>(); ;
            EducationFilterComboBox.SelectedIndex = 0;

            ComputerLiteracyFilterComboBox.ItemsSource = EnumHelper.GetEnumValuesWithAll<ComputerLiteracyLevel>();
            ComputerLiteracyFilterComboBox.SelectedIndex = 0;

            CandidatesDataGrid.ItemsSource = App.EmployeeService.GetAllEmployees();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CandidateEditPage());
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Employee? selectedEmployee = CandidatesDataGrid.SelectedItem as Employee;
            if (selectedEmployee == null)
            {
                MessageBox.Show("Будь ласка, оберіть кандидата для редагування.", "Помилка");
                return;
            }

            this.NavigationService.Navigate(new CandidateEditPage(selectedEmployee));
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Employee? selectedEmployee = CandidatesDataGrid.SelectedItem as Employee;
            if (selectedEmployee == null)
            {
                MessageBox.Show("Будь ласка, оберіть кандидата для видалення.", "Помилка");
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Ви впевнені, що хочете видалити кандидата {selectedEmployee.FullName}?", "Підтвердження видалення", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                App.EmployeeService.DeleteEmployee(selectedEmployee);
                await App.EmployeeService.SaveEmployeesAsync();
                ApplyFilters();
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var educationFilter = EnumHelper.GetEnumFromComboBox<EducationLevel>(EducationFilterComboBox.SelectedIndex, EducationFilterComboBox.SelectedItem);

            var computerLiteracyFilter = EnumHelper.GetEnumFromComboBox<ComputerLiteracyLevel>(ComputerLiteracyFilterComboBox.SelectedIndex, ComputerLiteracyFilterComboBox.SelectedItem);

            int? minExperience = int.TryParse(MinExperienceTextBox.Text, out int minExp) ? minExp : (int?)null;
            int? maxExperience = int.TryParse(MaxExperienceTextBox.Text, out int maxExp) ? maxExp : (int?)null;

            var filteredList = App.EmployeeService.FilterEmployees(
                educationFilter,
                minExperience,
                maxExperience,
                computerLiteracyFilter
            );

            CandidatesDataGrid.ItemsSource = filteredList;
        }

        private void ResetFilterButton_Click(object sender, RoutedEventArgs e)
        {
            EducationFilterComboBox.SelectedIndex = 0;
            MinExperienceTextBox.Text = string.Empty;
            MaxExperienceTextBox.Text = string.Empty;
            ComputerLiteracyFilterComboBox.SelectedIndex = 0;

            CandidatesDataGrid.ItemsSource = App.EmployeeService.GetAllEmployees();
        }

        private void CandidatesListPage_Loaded(object sender, RoutedEventArgs e)
        {
            var frame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
            if (frame != null)
            {
                frame.Navigated += MainFrame_Navigated;
            }
        }

        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Content == this)
            {
                ApplyFilters();
            }
        }
    }
}
