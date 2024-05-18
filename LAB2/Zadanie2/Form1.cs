using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private double result = 0;
        private string operation = "";
        private bool isOperationPerformed = false;
        private Stopwatch stopwatch;

        public Form1()
        {
            stopwatch = Stopwatch.StartNew();
            InitializeComponent();
            stopwatch.Stop();

            long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            if (elapsedMilliseconds > 1000) // Próg czasu uruchamiania (np. 1 sekunda)
            {
                LogEvent("Application initialization time exceeded threshold.", $"Initialization time: {elapsedMilliseconds} ms");
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            if ((textBox_Result.Text == "0") || (isOperationPerformed))
                textBox_Result.Clear();

            isOperationPerformed = false;
            Button button = (Button)sender;
            textBox_Result.Text += button.Text;
        }

        private void operator_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (result != 0)
            {
                buttonEquals_Click(sender, e); // Wywołanie metody obsługującej przycisk "="
                operation = button.Text;
                labelCurrentOperation.Text = result + " " + operation;
                isOperationPerformed = true;
            }
            else
            {
                operation = button.Text;
                result = Double.Parse(textBox_Result.Text);
                labelCurrentOperation.Text = result + " " + operation;
                isOperationPerformed = true;
            }
        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            textBox_Result.Text = "0";
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            textBox_Result.Text = "0";
            result = 0;
            labelCurrentOperation.Text = "";
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            switch (operation)
            {
                case "+":
                    textBox_Result.Text = (result + Double.Parse(textBox_Result.Text)).ToString();
                    break;
                case "-":
                    textBox_Result.Text = (result - Double.Parse(textBox_Result.Text)).ToString();
                    break;
                case "*":
                    textBox_Result.Text = (result * Double.Parse(textBox_Result.Text)).ToString();
                    break;
                case "/":
                    textBox_Result.Text = (result / Double.Parse(textBox_Result.Text)).ToString();
                    break;
                default:
                    break;
            }
            result = Double.Parse(textBox_Result.Text);
            operation = "";
            labelCurrentOperation.Text = "";
        }

        private void LogEvent(string message, string details)
        {
            string source = "SimpleCalculatorApp";
            string log = "Application";
            string eventMessage = $"{message}\n\nDetails:\n{details}";

            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, log);
            }

            EventLog.WriteEntry(source, eventMessage, EventLogEntryType.Information);
        }
    }
}
