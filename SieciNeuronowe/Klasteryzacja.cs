using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SieciNeuronowe
{
    internal class Klasteryzacja
    {
        public int liczbaCentroidów { get; set; } = 3;
        double[] centroidy;
        double[] punkty;
        int[] przynaleznosci;

        double[] losowanieCentroidow(double[] Punkty)
        {
            double[] wynik = new double[liczbaCentroidów];
            Random random = new Random();

            for (int i = 0; i < liczbaCentroidów; i++)
                wynik[i] = random.Next(0, Punkty.Length);

            return wynik;
        }

        public Klasteryzacja(double[] Punkty)
        {
            liczbaCentroidów = 3;
            punkty = Punkty;
            przynaleznosci = new int[punkty.Length];
            for (int i = 0; i < punkty.Length; i++)
                przynaleznosci[i] = -1;
            centroidy = losowanieCentroidow(this.punkty);
        }
        public Klasteryzacja(double[] Punkty, double[] Centroidy)
        {
            punkty = Punkty;
            centroidy = Centroidy;
            liczbaCentroidów = centroidy.Length;
            przynaleznosci = new int[punkty.Length];
            for (int i = 0; i < punkty.Length; i++)
                przynaleznosci[i] = -1;
        }

        internal void Klasteryzuj()
        {
            int[] starePrzynaleznosci;
            double[] odleglosci = new double[liczbaCentroidów];
            //ustalanie początkowych przynależności do centroidów
            for (int i = 0; i < punkty.Length; i++)
            {
                for (int j = 0; j < liczbaCentroidów; j++)
                {
                    odleglosci[j] = Math.Abs(punkty[i] - centroidy[j]);
                }
                przynaleznosci[i] = Array.IndexOf(odleglosci, odleglosci.Min());
            }
            do
            {
                starePrzynaleznosci = przynaleznosci;
                //ustalanie nowych wartości centroidów
                double[,] avg = new double[liczbaCentroidów, 2];
                for (int j = 0; j < przynaleznosci.Length; j++)
                {
                    for (int k = 0; k < liczbaCentroidów; k++)
                    {
                        if (przynaleznosci[j] == k)
                        {
                            avg[k, 0] += punkty[j];
                            avg[k, 1] += 1;
                        }
                    }
                }
                for (int i = 0; i < liczbaCentroidów; i++)
                {
                    centroidy[i] = avg[i, 0] / avg[i, 1];
                }
                //ustalanie nowych przynależności do centroidów
                for (int i = 0; i < punkty.Length; i++)
                {
                    for (int j = 0; j < liczbaCentroidów; j++)
                    {
                        odleglosci[j] = Math.Abs(punkty[i] - centroidy[j]);
                    }
                    przynaleznosci[i] = Array.IndexOf(odleglosci, odleglosci.Min());
                }
            } while (!starePrzynaleznosci.SequenceEqual(przynaleznosci));//jeśli przynależność żadnego z punktów nie zmieni się to algorytm kończy się
        }

        internal double[] zwrocCentroidy()
        {
            return centroidy;
        }

        internal List<double>[] zwrocPunkty()
        {
            List<double>[] podzielone = new List<double>[liczbaCentroidów];
            for (int j = 0; j < podzielone.Length; j++)
            {
                podzielone[j] = new List<double>();
            }
            for (int i = 0; i < przynaleznosci.Length; i++)
            {
                for (int j = 0; j < podzielone.Length; j++)
                {
                    if (przynaleznosci[i] == j)
                        podzielone[j].Add(punkty[i]);
                }
            }
            return podzielone;
        }

    }
}
