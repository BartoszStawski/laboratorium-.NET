using System;
using System.IO;

class Zadanie_6
{
    static void Main(string[] args)
    {
        Console.WriteLine("Podaj nazwę pliku źródłowego:");
        string sourceFileName = Console.ReadLine();

        Console.WriteLine("Podaj nazwę pliku docelowego:");
        string destinationFileName = Console.ReadLine();

        try
        {
            CopyFile(sourceFileName, destinationFileName);
            Console.WriteLine("Plik został pomyślnie skopiowany.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        }
    }

    static void CopyFile(string sourceFileName, string destinationFileName)
    {
        // Otwieramy plik źródłowy do odczytu
        using (FileStream sourceStream = new FileStream(sourceFileName, FileMode.Open))
        {
            // Tworzymy lub nadpisujemy plik docelowy do zapisu
            using (FileStream destinationStream = new FileStream(destinationFileName, FileMode.Create))
            {
                // Kopiujemy zawartość pliku źródłowego do pliku docelowego
                sourceStream.CopyTo(destinationStream);
            }
        }
    }
}
