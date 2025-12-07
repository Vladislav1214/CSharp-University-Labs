using System.Windows;
using System.Windows.Controls;

namespace Lab_1
{
    public partial class MainWindow : Window
    {
        private readonly CalculatorModel _model = new CalculatorModel();

        public MainWindow()
        {
            InitializeComponent();
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            MainDisplay.Text = _model.CurrentExpression;
            ExpressionDisplay.Text = _model.HistoryDisplay;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Content != null)
            {
                string text = btn.Content.ToString();

                switch (text)
                {
                    case "=":
                        _model.CalculateResult();
                        break;
                    case "CE":
                        _model.ClearEntry();
                        break;
                    case "C":
                        _model.ClearAll();
                        break;
                    case "<":
                        _model.Backspace();
                        break;
                    case ".":
                        _model.AddDecimalPoint();
                        break;
                    default:
                        _model.AddSymbol(text);
                        break;
                }
                UpdateDisplay();
            }
        }
    }
}