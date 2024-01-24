using System;

namespace Egzamin2
{
    class Program
    {
        /* Treść
            Wstęp Do Programowania – Egzamin Praktyczny 2022.01.14
        Zestaw A
        Piszę z pamięci treści zadań (prócz czwartego), na podstawie plików, które wstawiłem na moodle po egzaminie.

        Zadanie 1
        Stwórz funkcję static ulong A1(uint[] T), która zwraca sumę liczb pierwszych z tablicy T. Jeśli w tablicy nie ma liczb pierwszych, funkcja zwraca 1.
        Przykład: Dla tablicy {2}, funkcja zwraca 18, bo (7+11) = 18
        6

        Zadanie 2
        Stwórz funkcję static uint A2(int liczba), która w sposób rekurencyjny zwraca najbardziej znaczącą cyfrę zapisu siódemkowego cyfry dziesiętnej przekazanej jako parametr.
        Przykład: liczba -123 w zapisie siódemkowym to -234. Funkcja zwraca 2.

        Zadanie 3
        Stwórz funkcję static uint A3(string napis), która zwraca liczbę wyrazów, które spełniają warunek, że liczba spółgłosek w wyrazie stanowi co najmniej połowę liter wyrazu. Wyrazy w napisie mogą być oddzielone dowolnymi separatorami (kropka, spacja, wielokropek, średnik, itp.). Napis nie zawiera liter z polskimi ogonkami (ą, ę, itp.). 
        Przykład: Dla napisu „Mama, tata i Ala!”, funkcja zwraca 2.

        Zadanie 4
        Stwórz funkcję static ulong A4(ulong dane), która zmodyfikuje w otrzymanym ciągu 64. bitów wartość przechowywaną na 4. bitach: od 7 do 10 bitu. Funkcja zwiększa tę wartość o 1. Jeśli przechowywana wartość to 1111, zwiększona wartość, to 0000. Pozostałych bitów nie wolno zmodyfikować.
        */

        static void Main(string[] args)
        {
            Console.WriteLine(A1(new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })); // 2 + 3 + 5 + 7 = 17
            Console.WriteLine(A2(-23133123)); // 4
            Console.WriteLine(A3("Mama, tata i Ala!")); // 2
            Console.WriteLine(ZwrocBinarnie(A4(142558))); // Wejsciowa 100010110011011110 return to 100010110100011110
         
            Console.ReadKey();
        }

        #region Zadanie 1

        static bool CzyPierwsza(uint liczba)
        {
            if (liczba < 2)
                return false;

            for (uint i = 2; i < liczba; i += 1)
                if (liczba % i == 0)
                    return false;

            return true;
        }

        static ulong A1(uint[] T)
        {
            ulong suma = 0;

            foreach (uint liczba in T)
            {
                if (CzyPierwsza(liczba))
                    suma += liczba;
            }

            return suma;
        }

        #endregion

        #region Zadanie 2

        // ZADANIE 2
        static uint A2(int liczba) // Na podstawie Przykład #1 - Wyświetl binarnie i https://www.youtube.com/watch?v=VUHwfugYFEA
        {
            if (liczba < 0)
                return A2(-liczba / 7);

            if (liczba < 7)
                return (uint) liczba;

            return A2(liczba / 7);
        }


        #endregion

        #region Zadanie 3

        static bool CzyLitera(char znak)
        {
            if (znak >= 'a' && znak <= 'z')
                return true;

            if (znak >= 'A' && znak <= 'Z')
                return true;

            return false;
        }

        static bool CzySamogłoska(char znak)
        {
            char[] samogłoski = {'a', 'ą', 'e', 'ę', 'i', 'o', 'u', 'y',
                                 'A', 'Ą', 'E', 'Ę', 'I', 'O', 'U', 'Y'};


            foreach (char samogłoska in samogłoski)
                if (samogłoska == znak)
                    return true;

            return false;
        }

        static uint A3(string napis)
        {
            uint wyrazySpelniajaceWarunek = 0;
            uint liczbaSamogłosek = 0;
            uint liczbaSpółgłosek = 0;
            
            foreach (char znak in napis)
            {
                if (CzySamogłoska(znak))
                {
                    liczbaSamogłosek += 1;
                    continue;
                }
                
                if (CzyLitera(znak))
                {
                    liczbaSpółgłosek += 1;
                    continue;
                }

                if (liczbaSpółgłosek != 0 && liczbaSpółgłosek >= liczbaSamogłosek) // Jesli cos nie jest wyrazem to liczba spólglosek i samogłosek bedzie rowna 0, trzeba brac to pod uwage
                    wyrazySpelniajaceWarunek += 1;

                liczbaSamogłosek = 0;
                liczbaSpółgłosek = 0;
            }

            return wyrazySpelniajaceWarunek;
        }

        #endregion

        #region Zadanie 4

        static string ZwrocBinarnie(ulong liczba) // Funkcja pomocnicza
        {
            string wynik = "";

            while (liczba > 0)
            {
                wynik = liczba % 2 + wynik;
                liczba = liczba / 2;
            }

            return wynik;
        }

        static ulong A4(ulong dane) //Wartosc 100010110011011110 | c1 011110 | c2  0011 | c3 100010110000000000 | return 100010110100011110
        {
            var c1 = dane & 0b111111; // Pierwszych 6 bitow
            var c2 = (dane >> 6) & 0b1111; // Od 7 bitu i potem 4 kolejne czyli od 7 do 10 (interesujacy nas przedzial)
            var c3 = (dane >> 10) << 10; // Pierwsze 10 bitów wyzerowane
            
            c2 += 1;
            c2 = c2 & 0b1111; // Zapewnienie warunku ,,Jeśli przechowywana wartość to 1111, zwiększona wartość, to 0000.". Jeśli wartość jest równa 1111 to przekroczy 5 bitów wiec wystarczy uzyc 4 pierwszych które będą równe 0000
            c2 = c2 << 6; // Rozszerzam o brakujace 6 bitow aby zapewnic zgodnosc indeksow

            return c3 | c2 | c1; //Do części 3 dodajemy interesujace nas bity z rozszerzeniem o brakujace bity dla zgodnosci i na koniec część 1
        }
        
        #endregion

    }
}
