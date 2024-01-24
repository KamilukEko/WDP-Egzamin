using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egzamin4
{
    /* Treść
     
      Zadanie 1.
      CzySaParzysteJednakowe(int[] t, int[] s) zwraca true jesli istnieja indeksy i oraz j takie ze t[i] jest rowne s[i] i t[i] i s[i] sa parzyste
      
      Zadanie 2.
      int[] ZwrocTablicę(int[] T) zwraca tablicę liczb parzystych z liczb T, do wypelnienia tablicy uzyj petli while i do wyswietlenia do while

      Zadanie 3.
      Sumuj3A(int n) rekurencyjnie sumuje wszystkie liczby od 0 do n, ktorych ostatnia cyfra to 3

      Zadanie 4.
      Klasa student z publicznymi polami string nazwisko oraz int ocena. Metoda statyczna static Student Prymus(Student[] studenci) która zwraca studenta z najwyższa oceną.

      Zadanie 5.
      int IleWyrazowA(string tekst, char c) zwraca ile w podanym tekście jest wyrazów zawierających co najmniej dwa wystąpienia znaku c, tekst zawiera tylko małe litery.
     
     */ 
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CzySaParzysteJednakowe(new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 7, 3, 6 })); // False

            Console.WriteLine(CzySaParzysteJednakowe(new int[] { 1, 7, 3, 4, 5 }, new int[] { 1, 7, 3, 4, 5, 6 })); // True

            var tablica = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var tablicaParzystych = ZwrocTablicę(tablica);
            var i = 0;

            do // Wyswietlenie danych wejsciowych
            {
                Console.Write(tablica[i] + "*");
                i += 1;
            } while (i < tablica.Length);

            i = 0;
            Console.WriteLine();

            do // Wyswietlenie danych wynikowych
            {
                Console.Write(tablicaParzystych[i] + "*") ;
                i += 1;
            } while (i < tablicaParzystych.Length);

            Console.WriteLine();
            Console.WriteLine(Sumuj3A(25)); // 23 + 13 + 3 = 39

            var tablicaStudentow = new Student[]
            {
                new Student {ocena = 3, nazwisko = "Rzecki"},
                new Student {ocena = 4, nazwisko = "Ochocki"},
                new Student {ocena = 2, nazwisko = "Stawski"},
                new Student {ocena = 5, nazwisko = "Wokulski"},
                new Student {ocena = 3, nazwisko = "Geist"},
            };

            Console.WriteLine(Prymus(tablicaStudentow).nazwisko); // Wokulski

            Console.WriteLine(IleWyrazowA("ala ma katar", 'a')); // 2

            Console.ReadKey();
        }

        #region Zadanie 1 (3 minuty)

        static bool CzySaParzysteJednakowe(int[] t, int[] s)
        {
            int dlugoscMniejszej = 0;

            if (t.Length > s.Length)
            {
                dlugoscMniejszej = s.Length;
            }
            else
            {
                dlugoscMniejszej = t.Length;
            }

            for (int i = 0; i < dlugoscMniejszej; i++)
            {
                if (t[i] != s[i])
                    continue;

                if (t[i] % 2 != 0)
                    continue;

                return true;
            }

            return false;
        }

        #endregion

        #region Zadanie 2 (4 minuty)

        static bool CzyParzysta(int liczba)
        {
            if (liczba % 2 == 0)
                return true;

            return false;
        }

        static int[] ZwrocTablicę(int[] T)
        {
            var iloscParzystych = 0;

            foreach (var liczba in T)
                if (CzyParzysta(liczba))
                    iloscParzystych += 1;

            int[] tablicaParzystych = new int[iloscParzystych];
            int i = 0;

            while (iloscParzystych > 0) 
            {
                var liczba = T[i];
                i++;

                if (CzyParzysta(liczba))
                {
                    iloscParzystych -= 1;
                    tablicaParzystych[iloscParzystych] = liczba;
                }
            }
                
            return tablicaParzystych;
        }

        #endregion

        #region Zadanie 3 (3 minuty)

        static int OstatniaCyfra(int n)
        {
            if (n < 10)
                return n;

            return OstatniaCyfra(n % 10);
        }

        static int Sumuj3A(int n)
        {
            if (n == 0)
                return 0;

            if (OstatniaCyfra(n) == 3)
                return n + Sumuj3A(n - 1);

            return Sumuj3A(n - 1);
        }

        #endregion

        #region Zadanie 4 (6 minut, niepotrzebnie dodalem static do pól w klasie student i potem męczyłem sie z tym czemu nie moge uzyskac do nich dostępu

        public class Student
        {
            public string nazwisko;
            public int ocena;
        }

        static Student Prymus(Student[] studenci)
        {
            var najwyzszaOcena = int.MinValue;
            var pierwszyStudentZNajwyzsza = studenci[0];

            foreach (Student student in studenci)
            {
                if (student.ocena > najwyzszaOcena)
                {
                    najwyzszaOcena = student.ocena;
                    pierwszyStudentZNajwyzsza = student;
                }
            }

            return pierwszyStudentZNajwyzsza;
        }

        #endregion

        #region Zadanie 5 (4 minuty)

        static bool CzyLitera(char znak)
        {
            if (znak >= 'a' && znak <= 'z')
                return true;

            if (znak >= 'A' && znak <= 'Z')
                return true;

            return false;
        }

        static int IleWyrazowA(string tekst, char c)
        {
            int ileWyrazowA = 0;
            int wystapienC = 0;

            foreach (var znak in tekst)
            {
                if (!CzyLitera(znak))
                {
                    if (wystapienC >= 2)
                        ileWyrazowA += 1;

                    wystapienC = 0;
                }

                if (znak == c)
                    wystapienC += 1;
            }

            if (wystapienC >= 2) // Jeśli ciag znakow zakonczy sie znakiem to nie dodamy ostatniego wyrazu :(
                ileWyrazowA += 1;

            return ileWyrazowA;
        }

        #endregion
    }
}
