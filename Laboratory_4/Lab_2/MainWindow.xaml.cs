using System.Windows;
using System.Windows.Controls;

namespace Lab_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BestOilModel _model = new BestOilModel();
        public MainWindow()
        {
            InitializeComponent();
            SetupWindow();
        }

        private void CalculateButton(object sender, RoutedEventArgs e)
        {
            if (CafeTotalCostTextBlock == null || FuelTotalCostTextBlock == null)
            {
                return;
            }

            decimal dailyIncome = 0;

            if (ByQuantityRadioButton.IsChecked == true)
            {
                dailyIncome += decimal.Parse(FuelTotalCostTextBlock.Text);
            }
            else if (FuelSumTextBox.Text != string.Empty)
            {
                dailyIncome += decimal.Parse(FuelSumTextBox.Text);
            }

            dailyIncome += decimal.Parse(CafeTotalCostTextBlock.Text);

            GrandTotalTextBlock.Text = dailyIncome.ToString();
        }

        private void FuelTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (FuelPriceTextBox == null)
            {
                return;
            }

            FuelPriceTextBox.Text = _model.Fuels[FuelTypeComboBox.SelectedIndex].Price.ToString();
        }

        private void ByQuantityRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (FuelQuantityTextBox == null || FuelSumTextBox == null)
            {
                return;
            }
            FuelQuantityTextBox.IsEnabled = true;
            FuelSumTextBox.IsEnabled = false;
            FuelSumTextBox.IsReadOnly = true;
            FuelSumTextBox.Text = string.Empty;
            FuelQuantityTextBox.IsReadOnly = false;
            FuelQuantityTextBox.Text = string.Empty;
            DoVudachi_GroupBox.Header = "До оплати";
            DoVudachi_TextBlock.Text = "грн.";

        }

        private void BySumRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (FuelQuantityTextBox == null || FuelSumTextBox == null)
            {
                return;
            }
            FuelQuantityTextBox.IsEnabled = false;
            FuelSumTextBox.IsEnabled = true;
            FuelSumTextBox.IsReadOnly = false;
            FuelSumTextBox.Text = string.Empty;
            FuelQuantityTextBox.IsReadOnly = true;
            FuelQuantityTextBox.Text = string.Empty;
            DoVudachi_GroupBox.Header = "До видачі";
            DoVudachi_TextBlock.Text = "л.";
        }


        private void SetupWindow()
        {
            FuelTypeComboBox.ItemsSource = _model.Fuels; // Заповнюємо ComboBox
            FuelTypeComboBox.SelectedIndex = 0; // Вибираємо перший елемент за замовчуванням
            FuelPriceTextBox.Text = _model.Fuels[0].Price.ToString();
            HotDogPriceTextBox.Text = _model.CafeItems[0].Price.ToString();
            HamburgerPriceTextBox.Text = _model.CafeItems[1].Price.ToString();
            FriesPriceTextBox.Text = _model.CafeItems[2].Price.ToString();
            ColaPriceTextBox.Text = _model.CafeItems[3].Price.ToString();
        }

        private void HotDogCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            HotDogQuantityTextBox.IsReadOnly = false;
            HotDogQuantityTextBox.IsEnabled = true;
        }
        private void HotDogCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            HotDogQuantityTextBox.IsReadOnly = true;
            HotDogQuantityTextBox.IsEnabled = false;
            HotDogQuantityTextBox.Text = string.Empty;
            CalculateCafeCost();
        }

        private void HamburgerCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            HamburgerQuantityTextBox.IsReadOnly = false;
            HamburgerQuantityTextBox.IsEnabled = true;
        }
        private void HamburgerCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            HamburgerQuantityTextBox.IsReadOnly = true;
            HamburgerQuantityTextBox.IsEnabled = false;
            HamburgerQuantityTextBox.Text = string.Empty;
            CalculateCafeCost();
        }

        private void FriesCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            FriesQuantityTextBox.IsReadOnly = false;
            FriesQuantityTextBox.IsEnabled = true;
        }
        private void FriesCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            FriesQuantityTextBox.IsReadOnly = true;
            FriesQuantityTextBox.IsEnabled = false;
            FriesQuantityTextBox.Text = string.Empty;
            CalculateCafeCost();
        }

        private void ColaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ColaQuantityTextBox.IsReadOnly = false;
            ColaQuantityTextBox.IsEnabled = true;
        }
        private void ColaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ColaQuantityTextBox.IsReadOnly = true;
            ColaQuantityTextBox.IsEnabled = false;
            ColaQuantityTextBox.Text = string.Empty;
            CalculateCafeCost();
        }


        private void QuantityTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CalculateCafeCost();
        }
        private void FuelTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CalculateFuelCost();
        }


        private void CalculateFuelCost()
        {
            // Перевірка, чи завантажені елементи
            if (FuelTypeComboBox == null || FuelTotalCostTextBlock == null)
            {
                return;
            }

            Fuel selectedFuel = FuelTypeComboBox.SelectedItem as Fuel;
            if (selectedFuel == null) return;

            decimal fuelCost = 0;

            // Перевіряємо, який режим активний
            if (ByQuantityRadioButton.IsChecked == true)
            {
                // Режим "Кількість"
                if (double.TryParse(FuelQuantityTextBox.Text.Replace(",", "."), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double liters) && liters > 0)
                {
                    fuelCost = _model.CalculateFuelCostByLiters(selectedFuel, liters);
                }
            }
            else // BySumRadioButton.IsChecked == true
            {
                // Режим "Сума"
                if (decimal.TryParse(FuelSumTextBox.Text.Replace(",", "."), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out decimal sum) && sum > 0)
                {
                    // Вартість до оплати - це і є введена сума
                    fuelCost = _model.CalculateLitersBySum(selectedFuel, sum);
                }
            }

            // Оновлюємо текстовий блок з результатом
            FuelTotalCostTextBlock.Text = fuelCost.ToString("F2");
        }

        private void CalculateCafeCost()
        {
            if (CafeTotalCostTextBlock == null) return;

            decimal cafeTotalCost = 0;

            // Хот-дог
            if (HotDogCheckBox.IsChecked == true && int.TryParse(HotDogQuantityTextBox.Text, out int hotDogQty))
            {
                cafeTotalCost += _model.CafeItems[0].Price * hotDogQty;
            }
            // Гамбургер
            if (HamburgerCheckBox.IsChecked == true && int.TryParse(HamburgerQuantityTextBox.Text, out int hamburgerQty))
            {
                cafeTotalCost += _model.CafeItems[1].Price * hamburgerQty;
            }
            // Картопля
            if (FriesCheckBox.IsChecked == true && int.TryParse(FriesQuantityTextBox.Text, out int friesQty))
            {
                cafeTotalCost += _model.CafeItems[2].Price * friesQty;
            }
            // Кола
            if (ColaCheckBox.IsChecked == true && int.TryParse(ColaQuantityTextBox.Text, out int colaQty))
            {
                cafeTotalCost += _model.CafeItems[3].Price * colaQty;
            }

            CafeTotalCostTextBlock.Text = cafeTotalCost.ToString("F2");
        }
    }
}
