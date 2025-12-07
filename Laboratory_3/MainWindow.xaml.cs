using System;
using System.Windows;
using System.Windows.Controls;

namespace Laboratory_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Завдання 1
        private void HelloButton_Click(object sender, RoutedEventArgs e)
        {
            InfoLabel.Content = "Привіт";
        }

        private void GoodbyeButton_Click(object sender, RoutedEventArgs e)
        {
            InfoLabel.Content = "До побачення";
        }

        //  Завдання 2
        private void HideButton_Click_Task2(object sender, RoutedEventArgs e)
        {
            InfoTextBlock.Visibility = Visibility.Collapsed;
        }

        private void ShowButton_Click_Task2(object sender, RoutedEventArgs e)
        {
            InfoTextBlock.Visibility = Visibility.Visible;
        }

        // Завдання 3
        private void HideButton_Click_Task3(object sender, RoutedEventArgs e)
        {
            InputTextBox.Visibility = Visibility.Collapsed;
        }

        private void ShowButton_Click_Task3(object sender, RoutedEventArgs e)
        {
            InputTextBox.Visibility = Visibility.Visible;
        }

        private void ClearButton_Click_Task3(object sender, RoutedEventArgs e)
        {
            InputTextBox.Clear();
        }

        // Завдання 4
        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                clickedButton.Visibility = Visibility.Collapsed;

                var buttons = new[] { Button1, Button2, Button3, Button4, Button5 };

                int visibleButtonsCount = 0;
                foreach (var btn in buttons)
                {
                    if (btn.Visibility == Visibility.Visible)
                    {
                        visibleButtonsCount++;
                    }
                }

                if (visibleButtonsCount == 0)
                {

                    MessageBox.Show("Вітаємо! Ви приховали усі кнопки!", "Перемога!");

                    foreach (var btn in buttons)
                    {
                        btn.Visibility = Visibility.Visible;
                    }

                    return;
                }


                Random rand = new Random();

                while (true)
                {
                    int buttonIndexToShow = rand.Next(0, buttons.Length);

                    if (buttons[buttonIndexToShow] != clickedButton)
                    {
                        buttons[buttonIndexToShow].Visibility = Visibility.Visible;

                        return;
                    }
                    else if (rand.Next(0, 2) == 1)
                    {
                        return;
                    }
                }
            }
        }

        // Завдання 5
        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            // 1 фунт = 0.453592 кг
            const double poundsToKgsRatio = 0.453592;

            if (double.TryParse(PoundsTextBox.Text, out double pounds))
            {
                double kgs = pounds * poundsToKgsRatio;
                KgsTextBox.Text = kgs.ToString("F3");
            }
            else
            {
                KgsTextBox.Text = "Помилка вводу!";
            }
        }
    }
}
