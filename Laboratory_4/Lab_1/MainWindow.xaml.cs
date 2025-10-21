using System.Windows;
using System.Windows.Controls;

namespace Lab_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CalculatorModel _calculatorModel = new CalculatorModel();

        public MainWindow()
        {
            InitializeComponent();
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            MainDisplay.Text = _calculatorModel.CurrentDisplay;
            ExpressionDisplay.Text = _calculatorModel.FullExpressionDisplay;
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            char digit = button.Content.ToString()[0];

            _calculatorModel.AddDigit(digit);

            UpdateDisplay();
        }

        private void DecimalPointButton_Click(object sender, RoutedEventArgs e)
        {
            _calculatorModel.AddDecimalPoint();
            UpdateDisplay();
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            char operation = button.Content.ToString()[0];

            _calculatorModel.SetOperation(operation);
            UpdateDisplay();
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            _calculatorModel.CalculateResult();
            UpdateDisplay();
        }

        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            _calculatorModel.ClearEntry();
            UpdateDisplay();
        }

        private void ClearAllButton_Click(object sender, RoutedEventArgs e)
        {
            _calculatorModel.ClearAll();
            UpdateDisplay();
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            _calculatorModel.Backspace();
            UpdateDisplay();
        }
    }
}
