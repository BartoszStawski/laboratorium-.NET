using System;
using System.Diagnostics;

namespace DivisionConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the dividend:");
            string dividendInput = Console.ReadLine();

            Console.WriteLine("Enter the divisor:");
            string divisorInput = Console.ReadLine();

            try
            {
                double dividend = Convert.ToDouble(dividendInput);
                double divisor = Convert.ToDouble(divisorInput);

                if (divisor == 0)
                {
                    throw new DivideByZeroException("Divisor cannot be zero.");
                }

                double result = dividend / divisor;
                Console.WriteLine($"Result: {result}");
            }
            catch (FormatException ex)
            {
                LogErrorToEventLog("Invalid input format. Please enter numeric values.", ex);
                Console.WriteLine("Please enter valid numbers.");
            }
            catch (DivideByZeroException ex)
            {
                LogErrorToEventLog("Attempted to divide by zero.", ex);
                Console.WriteLine("Divisor cannot be zero.");
            }
            catch (Exception ex)
            {
                LogErrorToEventLog("An unexpected error occurred.", ex);
                Console.WriteLine("An unexpected error occurred.");
            }
        }

        private static void LogErrorToEventLog(string message, Exception ex)
        {
            string source = "DivisionConsoleApp";
            string log = "Application";
            string eventMessage = $"{message}\n\nException Details:\n{ex}";

            // Zakłada, że źródło już istnieje
            EventLog.WriteEntry(source, eventMessage, EventLogEntryType.Error);
        }
    }
}
