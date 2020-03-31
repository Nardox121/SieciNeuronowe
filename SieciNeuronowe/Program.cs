using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace SieciNeuronowe
{
    class Program
    {
        static void Main(string[] args)
        {
            //double[] punkty = { 0.1, 2.0, 5.6, 10.2, -12.1, -32.2, -4.3, 15.4, 20.6, 25.9, -14.2, -16.3, 0.6, 7.4, 3.6, 6.7, 2.1, 5.4, 22, 25, 55, -46, 2.0, 3.8 };
            double[] punkty = { -12.7, -12.1, -11.1, -8.1, -1.4, 0, 1.8, 8.7, 9.7, 11.9 };
            double[] centroidy = { -10, 0, 10 };
            Klasteryzacja klasteryzacja = new Klasteryzacja(punkty, centroidy);
            klasteryzacja.Klasteryzuj();
            List<double>[] sklasteryzowane = klasteryzacja.zwrocPunkty();
            double[] noweCentroidy = klasteryzacja.zwrocCentroidy();
            Console.WriteLine("Nowe wartości centroidów:");
            for (int i = 0; i < noweCentroidy.Length; i++)
                Console.Write(noweCentroidy[i] + " | ");
            Console.Write("\n");
            for (int i = 0; i < sklasteryzowane.Length; i++)
            {
                Console.WriteLine("Elementy przypisane do centroidu " + i);
                for (int j = 0; j < sklasteryzowane[i].Count; j++)
                    Console.Write(sklasteryzowane[i][j] + " | ");
                Console.Write("\n");
            }

            PNN pnn = new PNN(klasteryzacja.liczbaCentroidów);
            for (int i = 0; i < klasteryzacja.liczbaCentroidów; i++)
            {
                for (int j = 0; j < sklasteryzowane[i].Count; j++)
                {
                    pnn.ucz(sklasteryzowane[i][j], i);
                }
            }
            double element = -4.8;
            double[] prawdopodobienstwa = pnn.prawdopodobienstwa(element);
            Console.WriteLine("Prawdopodobienstwo ze element " + element + " bedzie w kolejnych klasach:");
            for (int i = 0; i < prawdopodobienstwa.Length; i++)
                Console.Write(Math.Round(prawdopodobienstwa[i], 2) + " | ");

        }
    }
}
