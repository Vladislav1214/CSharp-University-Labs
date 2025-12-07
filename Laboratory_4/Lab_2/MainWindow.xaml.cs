using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Lab_2
{
    public partial class MainWindow : Window
    {
        private BestOilModel _model = new BestOilModel();

        public MainWindow()
        {
            InitializeComponent();
            SetupWindow();
            this.Closing += MainWindow_Closing;
        }

        private void SetupWindow()
        {
            FuelTypeComboBox.ItemsSource = _model.Fuels;
            FuelTypeComboBox.SelectedIndex = 0;

            HotDogPriceTextBox.Text = _model.CafeItems[0].Price.ToString("F2");
            HamburgerPriceTextBox.Text = _model.CafeItems[1].Price.ToString("F2");
            FriesPriceTextBox.Text = _model.CafeItems[2].Price.ToString("F2");
            ColaPriceTextBox.Text = _model.CafeItems[3].Price.ToString("F2");
        }

        private void CalculateButton(object sender, RoutedEventArgs e)
        {
            decimal currentSale = 0;

            if (decimal.TryParse(FuelTotalCostTextBlock.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal fuelCost))
            {
                currentSale += fuelCost;
            }

            if (decimal.TryParse(CafeTotalCostTextBlock.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal cafeSum))
            {
                currentSale += cafeSum;
            }

            GrandTotalTextBlock.Text = currentSale.ToString("F2", CultureInfo.InvariantCulture);

            if (currentSale > 0)
            {
                _model.AddToDailyIncome(currentSale);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            FuelTypeComboBox.SelectedIndex = 0;
            ByQuantityRadioButton.IsChecked = true;
            FuelQuantityTextBox.Text = "";
            FuelSumTextBox.Text = "";
            FuelTotalCostTextBlock.Text = "0.00";
            HotDogCheckBox.IsChecked = false;
            HamburgerCheckBox.IsChecked = false;
            FriesCheckBox.IsChecked = false;
            ColaCheckBox.IsChecked = false;
            CafeTotalCostTextBlock.Text = "0.00";
            GrandTotalTextBlock.Text = "0.00";
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = $"Робочий день завершено.\nЗагальна виручка: {_model.DailyIncome.ToString("F2")} грн.";
            MessageBox.Show(message, "Звіт BestOil", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void FuelTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FuelPriceTextBox == null) return;
            var selectedFuel = FuelTypeComboBox.SelectedItem as Fuel;
            if (selectedFuel != null)
            {
                FuelPriceTextBox.Text = selectedFuel.Price.ToString("F2");
                CalculateFuelCost();
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (FuelQuantityTextBox == null || FuelSumTextBox == null) return;

            FuelQuantityTextBox.Text = "";
            FuelSumTextBox.Text = "";
            FuelTotalCostTextBlock.Text = "0.00";

            if (ByQuantityRadioButton.IsChecked == true)
            {
                DoVudachi_GroupBox.Header = "До оплати";
                DoVudachi_TextBlock.Text = "грн.";
                FuelQuantityTextBox.IsEnabled = true;
                FuelSumTextBox.IsEnabled = false;
            }
            else
            {
                DoVudachi_GroupBox.Header = "До видачі";
                DoVudachi_TextBlock.Text = "л.";
                FuelQuantityTextBox.IsEnabled = false;
                FuelSumTextBox.IsEnabled = true;
            }
        }

        private void FuelTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateFuelCost();
        }

        private void CalculateFuelCost()
        {
            if (FuelTypeComboBox == null || FuelTotalCostTextBlock == null || _model == null) return;

            var selectedFuel = FuelTypeComboBox.SelectedItem as Fuel;
            if (selectedFuel == null) return;

            FuelTotalCostTextBlock.Text = "0.00";

            if (ByQuantityRadioButton.IsChecked == true)
            {
                if (double.TryParse(FuelQuantityTextBox.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double liters) && liters > 0)
                {
                    decimal cost = _model.CalculateFuelCostByLiters(selectedFuel, liters);
                    FuelTotalCostTextBlock.Text = cost.ToString("F2", CultureInfo.InvariantCulture);
                }
            }
            else
            {
                if (decimal.TryParse(FuelSumTextBox.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal sum) && sum > 0)
                {
                    decimal liters = _model.CalculateLitersBySum(selectedFuel, sum);
                    FuelTotalCostTextBlock.Text = liters.ToString("F2", CultureInfo.InvariantCulture);
                }
            }
        }

        private void CafeCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                TextBox targetBox = null;

                if (checkBox == HotDogCheckBox) targetBox = HotDogQuantityTextBox;
                else if (checkBox == HamburgerCheckBox) targetBox = HamburgerQuantityTextBox;
                else if (checkBox == FriesCheckBox) targetBox = FriesQuantityTextBox;
                else if (checkBox == ColaCheckBox) targetBox = ColaQuantityTextBox;

                if (targetBox != null)
                {
                    targetBox.IsEnabled = (checkBox.IsChecked == true);
                    if (checkBox.IsChecked == false)
                    {
                        targetBox.Text = "";
                    }
                    else
                    {
                        targetBox.Focus();
                    }
                }
            }
            CalculateCafeCost();
        }

        private void QuantityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateCafeCost();
        }

        private void CalculateCafeCost()
        {
            if (CafeTotalCostTextBlock == null || _model == null) return;

            decimal total = 0;

            decimal GetItemCost(int index, TextBox qtyBox, CheckBox checkBox)
            {
                if (checkBox.IsChecked == true && int.TryParse(qtyBox.Text, out int q) && q > 0)
                {
                    return _model.CafeItems[index].Price * q;
                }
                return 0;
            }

            total += GetItemCost(0, HotDogQuantityTextBox, HotDogCheckBox);
            total += GetItemCost(1, HamburgerQuantityTextBox, HamburgerCheckBox);
            total += GetItemCost(2, FriesQuantityTextBox, FriesCheckBox);
            total += GetItemCost(3, ColaQuantityTextBox, ColaCheckBox);

            CafeTotalCostTextBlock.Text = total.ToString("F2", CultureInfo.InvariantCulture);
        }
    }
}