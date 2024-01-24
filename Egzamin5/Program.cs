using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egzamin5
{
    class Program
    {
        /* Treść
          1. Zwróc średnią liczb pierwszych z tablicy
          2. Zwróc pierwszą znaczącą liczbę w zapisie siódemkowym liczby podanej w zapisie dziesiętnym
          3. Zwróć ostatnie słowo w tekście którego liczba współgłosek jest większa od połowy tego słowa, jeżel nie ma takiego słowa zwróc '*'
          4. Odejmij od bitów od 7 do 9 liczbę jeden. Nie zmieniaj innych bitów. Jeżeli bity od 7-9 są 000 to po wykonaniu mają mieć wartość 111.
        */

        static void Main(string[] args)
        {
            Console.WriteLine(Srednia(new int[] { 1, 2, 4, 5, 11, 22, 68, 23 })); // (2 + 11 + 23 + 5) / 4 = 10,25
            Console.WriteLine(Slowo("aaaaaaaaaaaammmmm amammamamm aaaaaaaaaaammmm mmmma aaarrr")); // mmmma
            Console.WriteLine(PierwszaZnaczaca(2137)); // 6142 czyli 6

            Console.WriteLine(OperacjaBitowa(2137)); // 100001011001 
                                                     // 100000011001 czyli 2073

            Console.WriteLine(OperacjaBitowa(2073)); // 100000011001 
                                                     // 100111011001 czyli 2521

            Console.ReadKey();
        }

        #region Zadanie 1

        public static bool CzyPierwsza(int liczba)
        {
            if (liczba < 2)
                return false;

            for (int i = 2; i < liczba; i++)
            {
                if (liczba % i == 0)
                    return false;
            }

            return true;
        }

        public static double Srednia(int[] T)
        {
            double suma = 0;
            int ilosc = 0;

            foreach (var liczba in T)
            {
                if (!CzyPierwsza(liczba))
                    continue;

                suma += liczba;
                ilosc += 1;
            }

            return suma / ilosc;
        }

        #endregion

        #region Zadanie 2
        static int PierwszaZnaczaca(int liczbaWDziesiatkowym)
        {
            while (liczbaWDziesiatkowym >= 7)
                liczbaWDziesiatkowym = liczbaWDziesiatkowym / 7;

            return liczbaWDziesiatkowym;
        }

        #endregion

        #region Zadanie 3

        public static bool CzySamogloska(char znak)
        {
            char[] samogloski = { 
                'a', 'ą', 'ę', 'e', 'o', 'u', 'y', 'i', 'ó',
                'A', 'Ą', 'Ę', 'E', 'O', 'U', 'Y', 'I', 'Ó'
            };

            foreach (var samogloska in samogloski)
                if (znak == samogloska)
                    return true;

            return false;
        }

        public static bool CzyLitera(char znak)
        {
            if (znak >= 'a' && znak <= 'z')
                return true;

            if(znak >= 'A' && znak <= 'Z')
                return true;

            return false;
        }

        public static string Slowo(string zdanie)
        {
            string slowo = "*";

            string aktualneSlowo = "*";
            int iloscSamoglosek = 0;

            foreach (var znak in zdanie)
            {
                if (!CzyLitera(znak))
                {
                    if ((aktualneSlowo.Length - iloscSamoglosek) > iloscSamoglosek) // Wiecej wspolgosek
                        slowo = aktualneSlowo;

                    aktualneSlowo = "";
                    iloscSamoglosek = 0;
                    continue;
                }

                if (CzySamogloska(znak))
                    iloscSamoglosek += 1;

                aktualneSlowo += znak;
            }

            if ((aktualneSlowo.Length - iloscSamoglosek) > iloscSamoglosek) // Wiecej wspolgosek
                slowo = aktualneSlowo;

            return slowo;
        }

        #endregion

        #region Zadanie 4

        public static long OperacjaBitowa(long dane)
        {
            var c1 = dane & 0b111111; // Pierwszych 6 bitów
            var c2 = (dane >> 6) & 0b111; // Od 7 do 10 bita
            var c3 = (dane >> 10) << 10; // Wyzerowane 10 pierwszych bitów;

            if (c2 == 0b000)
            {
                c2 = 0b111;
            }
            else
            {
                c2 -= 1;
            }

            c2 = c2 << 6; // Rozszerzenie o 6 dla zgodności bitów

            return c3 | c2 | c1; // Zlepienie 
        }

        #endregion
    }
}
