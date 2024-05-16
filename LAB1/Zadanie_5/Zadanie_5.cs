using System;
using System.IO;

class Zadanie_5
{
    static void Main(string[] args)
    {
        Console.WriteLine("Wybierz opcję:");
        Console.WriteLine("1. Zapisz dane do pliku binarnego");
        Console.WriteLine("2. Odczytaj dane z pliku binarnego");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ZapiszDaneDoPliku();
                break;
            case "2":
                OdczytajDaneZPliku();
                break;
            default:
                Console.WriteLine("Niepoprawny wybór.");
                break;
        }
    }

    static void ZapiszDaneDoPliku()
    {
        Console.WriteLine("Podaj imię:");
        string imie = Console.ReadLine();

        Console.WriteLine("Podaj wiek:");
        string wiek = Console.ReadLine(); // Zmiana na string

        Console.WriteLine("Podaj adres:");
        string adres = Console.ReadLine();

        using (FileStream fs = new FileStream("dane.bin", FileMode.Create))
        using (BinaryWriter writer = new BinaryWriter(fs))
        {
            writer.Write(imie);
            writer.Write(wiek); // Zapis wieku jako string
            writer.Write(adres);
        }

        Console.WriteLine("Dane zostały zapisane do pliku.");
    }

    static void OdczytajDaneZPliku()
    {
        if (File.Exists("dane.bin"))
        {
            using (FileStream fs = new FileStream("dane.bin", FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                string imie = reader.ReadString();
                string wiek = reader.ReadString(); // Odczyt wieku jako string
                string adres = reader.ReadString();

                Console.WriteLine("Imię: " + imie);
                Console.WriteLine("Wiek: " + wiek); // Wyświetlenie wieku
                Console.WriteLine("Adres: " + adres);
            }
        }
        else
        {
            Console.WriteLine("Plik nie istnieje.");
        }
    }
}
