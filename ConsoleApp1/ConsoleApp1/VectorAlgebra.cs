using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ConsoleApp1
{
    public static class VectorAlgebra
    {
        public static double[] Add(double[] x, double[] y)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("При сложении векторов было обнаружено, что векторы разной размерности");
            }
            double[] result = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                result[i] = x[i] + y[i];
            }
            return result;
        }

        public static int[] Add(int[] x, int[] y)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("При сложении векторов было обнаружено, что векторы разной размерности");
            }
            int[] result = new int[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                result[i] = x[i] + y[i];
            }
            return result;
        }

        public static double Norm(int[] x)
        {
            double sum = 0;
            foreach (var el in x)
            {
                sum += el * el;
            }
            return Math.Sqrt(sum);
        }

        public static double Norm(double[] x)
        {
            double sum = 0;
            foreach (var el in x)
            {
                sum += el * el;
            }
            return Math.Sqrt(sum);
        }

        public static void AddToLeft(double[] x, double[] y)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("При сложении векторов было обнаружено, что векторы разной размерности");
            }
            for (int i = 0; i < x.Length; i++)
            {
                x[i] += y[i];
            }
        }

        public static void AddToLeft(int[] x, int[] y)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("При сложении векторов было обнаружено, что векторы разной размерности");
            }
            for (int i = 0; i < x.Length; i++)
            {
                x[i] += y[i];
            }
        }

        public static void AddToRight(double[] x, double[] y)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("При сложении векторов было обнаружено, что векторы разной размерности");
            }
            for (int i = 0; i < x.Length; i++)
            {
                y[i] += x[i];
            }
        }

        public static void AddToRight(int[] x, int[] y)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("При сложении векторов было обнаружено, что векторы разной размерности");
            }
            for (int i = 0; i < x.Length; i++)
            {
                y[i] += x[i];
            }
        }

        public static void Scale(double[] x, double k)
        {
            for (int i = 0; i < x.Length; i++)
            {
                x[i] *= k;
            }
        }

        public static void Scale(int[] x, int k)
        {
            for (int i = 0; i < x.Length; i++)
            {
                x[i] *= k;
            }
        }

        public static double[] CreateAndScale(double[] x, double k)
        {
            double[] result = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                result[i] = x[i] * k;
            }
            return result;
        }

        public static int[] CreateAndScale(int[] x, int k)
        {
            int[] result = new int[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                result[i] = x[i] * k;
            }
            return result;
        }

        public static void SubtrFromLeft(double[] x, double[] y)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("При вычитании векторов было обнаружено, что векторы разной размерности");
            }
            for (int i = 0; i < x.Length; i++)
            {
                x[i] -= y[i];
            }
        }

        public static void SubtrFromLeft(int[] x, int[] y)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("При вычитании векторов было обнаружено, что векторы разной размерности");
            }
            for (int i = 0; i < x.Length; i++)
            {
                x[i] -= y[i];
            }
        }

        public static void SubtrFromRight(double[] x, double[] y)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("При вычитании векторов было обнаружено, что векторы разной размерности");
            }
            for (int i = 0; i < x.Length; i++)
            {
                y[i] -= x[i];
            }
        }

        public static void SubtrFromRight(int[] x, int[] y)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("При вычитании векторов было обнаружено, что векторы разной размерности");
            }
            for (int i = 0; i < x.Length; i++)
            {
                y[i] -= x[i];
            }
        }

        public static void Swap(double[] x, double[] y)
        {
            var tmp = x;
            x = y;
            y = tmp;
        }

        public static void Swap(int[] x, int[] y)
        {
            var tmp = x;
            x = y;
            y = tmp;
        }

    }
}
