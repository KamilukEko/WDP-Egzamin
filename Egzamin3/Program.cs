using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egzamin3
{
    class Program
    {
        /* Treść
          F1 - Zwróć średnią liczb niepierwszych z tablicy
          F2 - Zwróć ostatnie słowo w tekście, które ma taką samą liczbę spółgłosek co samogłosek, jeżeli nie ma takiego słowa zwróć '!'
          F3 - Dodaj do bitów od 6 do 9 liczbę 1. Nie zmieniaj innych bitów. Jeżeli bity 6-9 to 1111, to po wykonaniu funkcji będą miały wartość 0000
          F4 - Zwróć sumę cyfr po konwersji z systemu dziesiętnego na szóstkowy. 
        */

        static void Main(string[] args)
        {
            Console.WriteLine(F1(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 })); // (1 + 4 + 6 + 8 + 9 + 10) / 6 = 6,3
            Console.WriteLine(F2("Nie znam słow z taką aaaawwww samą ilością spółgłosek aaawww hihihaha")); // aaawww
            Console.WriteLine(F3(2137)); // W binarnym 100001011001 czyli powinno byc 2169 (100001111001)
            Console.WriteLine(F4(2137)); // Po konwersji 13521 czyli 1 + 3 + 5 + 2 + 1 = 12

            Console.ReadKey();
        }

        #region Zadanie 1 (5 minut z sprawdzeniem)

        static bool CzyPierwsza(int liczba)
        {
            if (liczba < 2)
                return false;

            for (int i = 2; i < liczba; i++)
                if (liczba % i == 0)
                    return false;

            return true;
        }

        static double F1(int[] tablica)
        {
            double suma = 0;
            uint ilosc = 0;

            foreach (int liczba in tablica)
            {
                if (CzyPierwsza(liczba))
                    continue;

                suma += liczba;
                ilosc += 1;
            }

            return suma / ilosc;
        }

        #endregion

        #region Zadanie 2 (10 minut z sprawdzeniem) 

        static bool CzyLitera(char znak)
        {
            if (znak >= 'a' && znak <= 'z')
                return true;

            if (znak >= 'A' && znak <= 'Z')
                return true;

            return false;
        }

        static bool CzySpolgloska(char znak)
        {
            char[] spolgloski = { 'a', 'ą', 'e', 'ę', 'o', 'u', 'y', 'i',
                                  'A', 'Ą', 'E', 'Ę', 'O', 'U', 'Y', 'I' };

            foreach (char spolgloska in spolgloski)
                if (znak == spolgloska)
                    return true;

            return false;
        }

        static string F2(string zdanie)
        {
            string wartoscZwrotna = "!";

            string biezaceSlowo = "";
            int spółgłosek = 0;
            int samogłosek = 0;

            foreach (char znak in zdanie)
            {
                if (!CzyLitera(znak))
                {
                    if (spółgłosek != 0 && spółgłosek == samogłosek)
                        wartoscZwrotna = biezaceSlowo;

                    spółgłosek = 0;
                    samogłosek = 0;
                    biezaceSlowo = "";
                    continue;
                }

                biezaceSlowo += znak;

                if (CzySpolgloska(znak))
                {
                    spółgłosek += 1;
                    continue;
                }

                samogłosek += 1;
            }

            return wartoscZwrotna;
        }

        #endregion

        #region Zadanie 3 (5 minut przedz sprawdzeniem) 

        static ulong F3(ulong dane)
        {
            var c1 = dane & 0b11111; // Pierwsze 5 bitów
            var c2 = dane >> 5; // Bity od 6 do konca;
            c2 = c2 & 0b1111; // Zminiejszenie do interesującego nas przedziału 

            var c3 = (dane >> 9) << 9; // Wyzerowanie pierwszych 9 bitow

            c2 += 1;
            c2 = c2 & 0b1111; // Zapewnienie warunku 1111 -> 0000
            c2 = c2 << 5; // Zapewnienie zgodności indeksów

            return c3 | c2 | c1;
        }

        #endregion

        #region Zadanie 4 (3 minut przed sprawdzeniem) 

        static ulong F4(ulong liczbaDoKonwersji)
        {
            ulong suma = 0;

            while (liczbaDoKonwersji > 6)
            {
                suma += liczbaDoKonwersji % 6;
                liczbaDoKonwersji /= 6;
            }

            return suma + liczbaDoKonwersji;
        }

        #endregion
    }
}
