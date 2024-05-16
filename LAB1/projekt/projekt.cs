using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
class Zadanie
{
    public int Id { get; set; }
    public string Nazwa { get; set; }
    public string Opis { get; set; }
    public DateTime DataZakonczenia { get; set; }
    public bool CzyWykonane { get; set; }

    public Zadanie(int id, string nazwa, string opis, DateTime dataZakonczenia, bool czyWykonane)
    {
        Id = id;
        Nazwa = nazwa;
        Opis = opis;
        DataZakonczenia = dataZakonczenia;
        CzyWykonane = czyWykonane;
    }
}

class ManagerZadan
{
    private List<Zadanie> listaZadan = new List<Zadanie>();

    public void DodajZadanie(Zadanie zadanie)
    {
        listaZadan.Add(zadanie);
    }

    public void UsunZadanie(int id)
    {
        listaZadan.RemoveAll(z => z.Id == id);
    }

    public void WyswietlZadania()
    {
        foreach (var zadanie in listaZadan)
        {
            Console.WriteLine($"ID: {zadanie.Id}, Nazwa: {zadanie.Nazwa}, Opis: {zadanie.Opis}, Data zakończenia: {zadanie.DataZakonczenia}, Czy wykonane: {zadanie.CzyWykonane}");
        }
    }

    public void Serializuj(string nazwaPliku)
    {
        using (Stream stream = File.Open(nazwaPliku, FileMode.Create))
        {
            BinaryFormatter binarnyFormater = new BinaryFormatter();
            binarnyFormater.Serialize(stream, listaZadan);
        }
    }

    public void Deserializuj(string nazwaPliku)
    {
        using (Stream stream = File.Open(nazwaPliku, FileMode.Open))
        {
            BinaryFormatter binarnyFormater = new BinaryFormatter();
            listaZadan = (List<Zadanie>)binarnyFormater.Deserialize(stream);
        }
    }

    public bool CzyIstniejeZadanie(int id)
    {
        return listaZadan.Any(z => z.Id == id);
    }
}

class Program
{
    static void Main(string[] args)
    {
        ManagerZadan manager = new ManagerZadan();

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Dodaj zadanie");
            Console.WriteLine("2. Usuń zadanie");
            Console.WriteLine("3. Wyświetl zadania");
            Console.WriteLine("4. Zapisz listę zadań do pliku");
            Console.WriteLine("5. Wczytaj listę zadań z pliku");
            Console.WriteLine("0. Wyjście");

            Console.Write("\nWybierz opcję: ");
            string opcja = Console.ReadLine();

            switch (opcja)
            {
                case "1":
                    DodajZadanie(manager);
                    break;
                case "2":
                    UsunZadanie(manager);
                    break;
                case "3":
                    WyswietlZadania(manager);
                    break;
                case "4":
                    ZapiszDoPliku(manager);
                    break;
                case "5":
                    WczytajZPliku(manager);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Błąd: Nieprawidłowa opcja.");
                    break;
            }
        }
    }

    static void DodajZadanie(ManagerZadan manager)
    {
        Console.WriteLine("\nDodawanie nowego zadania:");

        Console.Write("Id: ");
        int id;
        if (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.WriteLine("Błąd: Niepoprawny format id.");
            return;
        }

        if (manager.CzyIstniejeZadanie(id))
        {
            Console.WriteLine("Błąd: Zadanie o podanym ID już istnieje.");
            return;
        }

        Console.Write("Nazwa: ");
        string nazwa = Console.ReadLine();

        Console.Write("Opis: ");
        string opis = Console.ReadLine();

        Console.Write("Data zakończenia (RRRR-MM-DD): ");
        DateTime dataZakonczenia;
        if (!DateTime.TryParse(Console.ReadLine(), out dataZakonczenia))
        {
            Console.WriteLine("Błąd: Niepoprawny format daty.");
            return;
        }

        manager.DodajZadanie(new Zadanie(id, nazwa, opis, dataZakonczenia, false));
        Console.WriteLine("Zadanie zostało dodane.");
    }

    static void UsunZadanie(ManagerZadan manager)
    {
        Console.Write("\nPodaj ID zadania do usunięcia: ");
        int id;
        if (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.WriteLine("Błąd: Niepoprawny format id.");
            return;
        }

        manager.UsunZadanie(id);
        Console.WriteLine("Zadanie zostało usunięte.");
    }

    static void WyswietlZadania(ManagerZadan manager)
    {
        Console.WriteLine("\nLista zadań:");
        manager.WyswietlZadania();
    }

    static void ZapiszDoPliku(ManagerZadan manager)
    {
        Console.Write("\nPodaj nazwę pliku do zapisu: ");
        string nazwaPliku = Console.ReadLine();

        try
        {
            manager.Serializuj(nazwaPliku);
            Console.WriteLine("Lista zadań została zapisana do pliku.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas zapisu: {ex.Message}");
        }
    }

    static void WczytajZPliku(ManagerZadan manager)
    {
        Console.Write("\nPodaj nazwę pliku do wczytania: ");
        string nazwaPliku = Console.ReadLine();

        try
        {
            manager.Deserializuj(nazwaPliku);
            Console.WriteLine("Lista zadań została wczytana z pliku.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Błąd: Plik nie istnieje.");
        }
        catch (SerializationException)
        {
            Console.WriteLine("Błąd: Niepoprawny format pliku.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas wczytywania: {ex.Message}");
        }
    }
}
