using System;
using System.Collections.Generic;
using System.Text;

namespace SieciNeuronowe
{
    internal class PNN
    {
        static double sigma = 0.8;
        List<double>[] radial;

        public PNN(int iloscKlas)
        {
            radial = new List<double>[iloscKlas];
            for(int i=0;i<iloscKlas;i++)
            {
                radial[i] = new List<double>();
            }
        }

        internal void ucz(double wartosc, int klasa)
        {
            radial[klasa].Add(wartosc);
        }

        internal double[] prawdopodobienstwa (double wartosc)
        {
            double[] wynik = new double[radial.Length];
            double norm = 0;
            for(int i = 0; i < radial.Length; i++)
            {
                wynik[i] = gauss(radial[i], wartosc);
                norm += wynik[i];
            }
            for(int i = 0; i < wynik.Length; i++)
            {
                wynik[i] /= norm;
            }
            return wynik;
        }
        
        double gauss(List<double> x, double wartosc)
        {
            double wynik = 0;
            for(int i = 0; i < x.Count; i++)
            {
                wynik += Math.Pow(Math.E,(-Math.Pow((Math.Abs(wartosc - x[i])*sigma),2)));
            }
            return wynik;
        }

    }
}
