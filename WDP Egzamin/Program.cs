using System;
using System.IO;

namespace WDP_Egzamin
{
	class Program
	{
		#region Klasy z zadania 4
		public class Klient
		{
			public string nazwisko; // Domyślnie tutaj nie ma nawet public więc nie da się odczytać nazwiska haha
		}

		public class Węzeł
		{
			public Klient k;
			public Węzeł następny;
		}

		public static class Lista
		{
			public static Węzeł Głowa;
		}

		#endregion

		static void Main(string[] args)
		{
			// Zadanie 1, grupa A
			Console.WriteLine("Zadanie 1, grupa A");
			Console.WriteLine(IleLiczb(new int[] { 1, 11, 3, 4, 5, 6 })); // 1

			// Zadanie 2, grupa A
			Console.WriteLine("Zadanie 2, grupa A");
			Console.WriteLine(IleWyrazow("ala ma katar", 'a')); // 2

			// Zadanie 3, grupa A
			Console.WriteLine("Zadanie 3, grupa A");
			Console.WriteLine(Suma(new double[] { 1, 2, 4, 6, 8, 9 }, 0, 7, 3.14)); // 10

			// Zadanie 4, grupa A (głupie zadanie, nie wiem jak miałoby zostać sprawdzone automatycznie przez runcode, raczej nie powinno być)
			#region Przygotowywanie listy pod testowanie zdania
			var k5 = new Klient();
			k5.nazwisko = "Wokulski";

			var w5 = new Węzeł();
			w5.k = k5;
			w5.następny = null;

			var k4 = new Klient();
			k4.nazwisko = "Rzecki";

			var w4 = new Węzeł();
			w4.k = k4;
			w4.następny = w5;

			var k3 = new Klient();
			k3.nazwisko = "Wokulski";

			var w3 = new Węzeł();
			w3.k = k3;
			w3.następny = w4;

			var k2 = new Klient();
			k2.nazwisko = "Rzecki";

			var w2 = new Węzeł();
			w2.k = k2;
			w2.następny = w3;

			var k1 = new Klient();
			k1.nazwisko = "Starski";

			var w1 = new Węzeł();
			w1.k = k1;
			w1.następny = w2;

			Lista.Głowa = w1;
			#endregion
			Zapisz("zadanie4");

			Console.ReadKey();
		}

		#region Zadanie 4, grupa A

		public static void Zapisz(string nazwa)
		{
			var writer = new StreamWriter(nazwa + ".txt");

			var obecnyWęzeł = Lista.Głowa;

			while (obecnyWęzeł != null)
			{
				writer.WriteLine(obecnyWęzeł.k.nazwisko);
				obecnyWęzeł = obecnyWęzeł.następny;
			}

			writer.Close();
		}


		#endregion

		#region Zadanie 3, grupa A

		static double Suma(double[] T, int i, double x, double y)
		{
			if (i == T.Length)
				return 0;

			if (T[i] < x && T[i] > y)
				return T[i] + Suma(T, i + 1, x, y);

			return Suma(T, i + 1, x, y); // Wow, to zadanie zajęło mi jakąś minutę haha
		}

		#endregion

		#region Zadanie 2, grupa A

		static int IleWyrazow(string tekst, char x)
		{
			int ileZgodnychWyrazow = 0;
			int ileZnakówWBieżącymWyrazie = 0;

			foreach (var znak in tekst) 
			{
				if (!(znak >= 'a' && znak <= 'z') && !(znak >= 'A' && znak <= 'Z'))
				{
					ileZnakówWBieżącymWyrazie = 0;
					continue;
				}

				if (znak != x)
					continue;

				ileZnakówWBieżącymWyrazie += 1;

				if (ileZnakówWBieżącymWyrazie == 2)
					ileZgodnychWyrazow += 1;
			}

			return ileZgodnychWyrazow;
		}

		#endregion

		#region Zadanie 1, grupa A

		static bool CzyPierwsza(int liczba)
		{
			if (liczba < 2)
				return false;

			for (int i = 2; i <= Math.Sqrt(liczba); i++)
			{
				if (liczba % i == 0)
					return false;
			}

			return true;
		}

		static int IleLiczb(int[] T)
		{
			int suma = 0;

			foreach (int liczba in T){
				suma += liczba;
			}

			double średnia = (double) suma / T.Length;

			int ile = 0;

			for (int i = 1; i < T.Length-1; i += 2) 
			{
				int liczba = T[i];

				if (!CzyPierwsza(liczba))
					continue;

				if (liczba > średnia * 2)
					ile += 1;
			}

			return ile;
		}

		#endregion
	}
}
