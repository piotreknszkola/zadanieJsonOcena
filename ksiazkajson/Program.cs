using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json; 

namespace Ksiegarnia
{
    /*
                          klasa: ksiazka
                           opis: klasa tworzy zmienne id, tytul, autor, rokwydania,gatunek
                            zmienne: id - liczbowa ktora jest dawana automatycznie kazdej nowostworzonej ksiazce
                               tytul - zmienna typu string ktora przechowuje nazwy ksiazki
                                autor - zmienna typu string ktora przechowuje nazwe autora
                                rokWydania- zmienna liczbowa ktora przechowuje rok wydania ksiazki
                                gatunek - zmienna typu string ktora przechowuje nazwe gatunku ksiazki



     */
    class Ksiazka
    {
        public int ID { get; set; }
        public string Tytul { get; set; }
        public string Autor { get; set; }
        public int RokWydania { get; set; }
        public string Gatunek { get; set; }
    }

    class Program
    {
        private const string filePath = "ksiazka.json";
        private static List<Ksiazka> ksiazki = new List<Ksiazka>();

        static void Main(string[] args)
        {
            WczytajKsiazkiZPliku();

            while (true)
            {
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Dodaj nową książkę");
                Console.WriteLine("2. Wyświetl listę książek");
                Console.WriteLine("3. Wyświetl szczegóły książki");
                Console.WriteLine("4. Usuń książkę");
                Console.WriteLine("5. Wyjdź");

                int wybor;
                if (!int.TryParse(Console.ReadLine(), out wybor))
                {
                    Console.WriteLine("Niepoprawna opcja. Wybierz ponownie.");
                    continue;
                }

                switch (wybor)
                {
                    case 1:
                        DodajKsiazke();
                        break;
                    case 2:
                        WyswietlListeKsiazek();
                        break;
                    case 3:
                        WyswietlSzczegolyKsiazki();
                        break;
                    case 4:
                        UsunKsiazke();
                        break;
                    case 5:
                        ZapiszKsiazkiDoPliku();
                        return;
                    default:
                        Console.WriteLine("Niepoprawna opcja. Wybierz ponownie.");
                        break;
                }
            }
        }

        static void WczytajKsiazkiZPliku()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                ksiazki = JsonConvert.DeserializeObject<List<Ksiazka>>(json);
            }
        }

        static void ZapiszKsiazkiDoPliku()
        {
            string json = JsonConvert.SerializeObject(ksiazki);
            File.WriteAllText(filePath, json);
        }

        static void DodajKsiazke()
        {
            Ksiazka nowaKsiazka = new Ksiazka();

            Console.WriteLine("Podaj tytuł:");
            nowaKsiazka.Tytul = Console.ReadLine();

            Console.WriteLine("Podaj autora:");
            nowaKsiazka.Autor = Console.ReadLine();

            Console.WriteLine("Podaj rok wydania:");
            int rok;
            if (!int.TryParse(Console.ReadLine(), out rok))
            {
                Console.WriteLine("Niepoprawny format roku. Operacja przerwana.");
                return;
            }
            nowaKsiazka.RokWydania = rok;

            Console.WriteLine("Podaj gatunek:");
            nowaKsiazka.Gatunek = Console.ReadLine();

            nowaKsiazka.ID = ksiazki.Count + 1;
            ksiazki.Add(nowaKsiazka);
            Console.WriteLine("Książka dodana pomyślnie.");


            ZapiszKsiazkiDoPliku();
        }

        static void WyswietlListeKsiazek()
        {
            if (ksiazki.Count == 0)
            {
                Console.WriteLine("Brak książek.");
                return;
            }

            foreach (var ksiazka in ksiazki)
            {
                Console.WriteLine($"ID: {ksiazka.ID}, Tytuł: {ksiazka.Tytul}");
            }
        }

        static void WyswietlSzczegolyKsiazki()
        {
            Console.WriteLine("Podaj ID książki, której szczegóły chcesz wyświetlić:");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Niepoprawne ID. Operacja przerwana.");
                return;
            }

            Ksiazka znalezionaKsiazka = ksiazki.Find(k => k.ID == id);
            if (znalezionaKsiazka != null)
            {
                Console.WriteLine($"ID: {znalezionaKsiazka.ID}");
                Console.WriteLine($"Tytuł: {znalezionaKsiazka.Tytul}");
                Console.WriteLine($"Autor: {znalezionaKsiazka.Autor}");
                Console.WriteLine($"Rok wydania: {znalezionaKsiazka.RokWydania}");
                Console.WriteLine($"Gatunek: {znalezionaKsiazka.Gatunek}");
            }
            else
            {
                Console.WriteLine("Książka o podanym ID nie istnieje.");
            }
        }

        static void UsunKsiazke()
        {
            Console.WriteLine("Podaj ID książki, którą chcesz usunąć:");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Niepoprawne ID. Operacja przerwana.");
                return;
            }

            Ksiazka doUsuniecia = ksiazki.Find(k => k.ID == id);
            if (doUsuniecia != null)
            {
                ksiazki.Remove(doUsuniecia);
                Console.WriteLine("Książka usunięta pomyślnie.");
            }
            else
            {
                Console.WriteLine("Książka o podanym ID nie istnieje.");
            }
        }
    }
}
