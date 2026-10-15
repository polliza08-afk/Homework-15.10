using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExampleCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CalculatorEngine _calculator = new CalculatorEngine();
        private bool _isEnteringNewNumber = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        //Обробник для цифр та коми
        private void Button_Digital_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Content is TextBlock textBlock)
            {
                string digit = textBlock.Text;

                if (_isEnteringNewNumber)
                {
                    txtResult.Text = digit == "," ? "0," : digit;
                    _isEnteringNewNumber = false;
                }
                else
                {
                    if (digit == "," && txtResult.Text.Contains(",")) return; //Захист від кількох ком
                    txtResult.Text = txtResult.Text == "0" && digit != "," ? digit : txtResult.Text + digit;
                }
            }
        }

        // Приклад обробника для операцій +, -, *, /
        private void Button_Operation_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Content is TextBlock textBlock)
            {
                if (double.TryParse(txtResult.Text, out double currentValue))
                {
                    string op = textBlock.Text;
                    double result = _calculator.CalculateOperation(currentValue, op);
                    txtResult.Text = result.ToString();
                    _isEnteringNewNumber = true;
                }
            }
        }

        //Кнопка очищення C / CE
        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            _calculator.Clear();
            txtResult.Text = "0";
            _isEnteringNewNumber = true;
        }

        // Кнопка Backspace
        private void Button_Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (!_isEnteringNewNumber && txtResult.Text.Length > 0)
            {
                txtResult.Text = txtResult.Text.Substring(0, txtResult.Text.Length - 1);
                if (string.IsNullOrEmpty(txtResult.Text) || txtResult.Text == "-")
                {
                    txtResult.Text = "0";
                    _isEnteringNewNumber = true;
                }
            }
        }
    }
}