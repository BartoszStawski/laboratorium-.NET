using System;
using System.IO;

class Zadanie_3
{
    static void Main(string[] args)
    {
        string filePath = "C:/Users/barte/Documents/wsb/4 sem/.NET/LAB1/files/sample.txt";

        try
        {
            // Sprawdzenie, czy plik istnieje
            if (File.Exists(filePath))
            {
                // Otwieranie pliku za pomocą StreamReader
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    // Odczytanie zawartości pliku i wyświetlenie na konsoli
                    string content = streamReader.ReadToEnd();
                    Console.WriteLine("Zawartość pliku:");
                    Console.WriteLine(content);
                }
            }
            else
            {
                Console.WriteLine("Plik nie istnieje.");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Plik nie został znaleziony.");
        }
        catch (IOException ex)
        {
            Console.WriteLine("Wystąpił błąd wejścia-wyjścia: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił nieoczekiwany błąd: " + ex.Message);
        }
    }
}
