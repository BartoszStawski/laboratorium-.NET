using System;
using System.IO;

class Zadanie_4
{
    static void Main(string[] args)
    {
        // Ścieżka do pliku tekstowego
        string filePath = "C:/Users/barte/Documents/wsb/4 sem/.NET/LAB1/files/sample.txt";

        try
        {
            // Sprawdzenie, czy plik istnieje
            if (File.Exists(filePath))
            {
                // Otwieranie pliku za pomocą StreamReader
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    // Odczytanie i wyświetlenie kolejnych linii od końca do początku
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        char[] charArray = line.ToCharArray();
                        Array.Reverse(charArray);
                        Console.WriteLine(new string(charArray));
                    }
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
