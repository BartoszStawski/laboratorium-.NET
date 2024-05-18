using System;
using System.Diagnostics;
using System.IO;

namespace SystemMonitorTool
{
    class Program
    {
        static void Main(string[] args)
        {
            // Utwórz liczniki wykorzystania procesora i pamięci RAM
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            // Utwórz wpis w dzienniku zdarzeń
            string source = "SystemMonitorTool";
            string log = "Application";

            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, log);
            }

            // Ścieżka do pliku logu
            string logFilePath = "system_monitor_log.txt";

            // Wczytaj wartość graniczną dla użycia procesora
            Console.Write("Podaj wartość graniczną dla użycia procesora (%): ");
            float cpuThreshold = float.Parse(Console.ReadLine());

            // Wczytaj wartość graniczną dla dostępnej pamięci RAM
            Console.Write("Podaj wartość graniczną dla dostępnej pamięci RAM (MB): ");
            float ramThreshold = float.Parse(Console.ReadLine());

            // Wczytaj czas zbierania danych
            Console.Write("Podaj czas zbierania danych (w sekundach): ");
            int collectionTime = int.Parse(Console.ReadLine());

            // Pobierz i zapisz dane co sekundę przez określony czas
            DateTime startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalSeconds < collectionTime)
            {
                float cpuUsage = cpuCounter.NextValue();
                float ramUsage = ramCounter.NextValue();

                // Sprawdź, czy przekroczono próg użycia procesora
                if (cpuUsage > cpuThreshold)
                {
                    // Zapisz błąd do systemu zdarzeń
                    EventLog.WriteEntry(source, $"High CPU Usage: {cpuUsage}%", EventLogEntryType.Error);

                    // Zapisz błąd do pliku
                    using (StreamWriter writer = new StreamWriter(logFilePath, true))
                    {
                        writer.WriteLine($"{DateTime.Now}: High CPU Usage: {cpuUsage}%");
                    }
                }

                // Sprawdź, czy przekroczono próg użycia pamięci RAM
                if (ramUsage < ramThreshold)
                {
                    // Zapisz błąd do systemu zdarzeń
                    EventLog.WriteEntry(source, $"Low Available RAM: {ramUsage} MB", EventLogEntryType.Error);

                    // Zapisz błąd do pliku
                    using (StreamWriter writer = new StreamWriter(logFilePath, true))
                    {
                        writer.WriteLine($"{DateTime.Now}: Low Available RAM: {ramUsage} MB");
                    }
                }

                System.Threading.Thread.Sleep(1000);
            }

            Console.WriteLine("Zakończono zbieranie danych. Naciśnij dowolny klawisz, aby wyjść...");
            Console.ReadKey();
        }
    }
}
