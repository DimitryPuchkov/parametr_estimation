using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows;

using dVector = System.Collections.Generic.List<double>;
using iVector = System.Collections.Generic.List<int>;


namespace ConsoleApp1
{
    public static class Algebra
    {
        public static dVector Add(dVector x, dVector y)
        {
            if (x.Count != y.Count)
            {
                throw new Exception("При сложении векторов было обнаружено, что векторы разной размерности");
            }
            dVector result = new dVector(new double[x.Count]);
            for (int i = 0; i < x.Count; i++)
            {
                result[i] = x[i] + y[i];
            }
            return result;
        }

        public static iVector Add(iVector x, iVector y)
        {
            if (x.Count != y.Count)
            {
                throw new Exception("При сложении векторов было обнаружено, что векторы разной размерности");
            }
            iVector result = new iVector(new int[x.Count]);
            for (int i = 0; i < x.Count; i++)
            {
                result[i] = x[i] + y[i];
            }
            return result;
        }

        public static double Norm(iVector x)
        {
            double sum = 0;
            foreach (var el in x)
            {
                sum += el * el;
            }
            return Math.Sqrt(sum);
        }

        public static double Norm(dVector x)
        {
            double sum = 0;
            foreach (var el in x)
            {
                sum += el * el;
            }
            return Math.Sqrt(sum);
        }

        public static void AddToLeft(dVector x, dVector y)
        {
            if (x.Count != y.Count)
            {
                throw new Exception("При сложении векторов было обнаружено, что векторы разной размерности");
            }
            for (int i = 0; i < x.Count; i++)
            {
                x[i] += y[i];
            }
        }

        public static void AddToLeft(iVector x, iVector y)
        {
            if (x.Count != y.Count)
            {
                throw new Exception("При сложении векторов было обнаружено, что векторы разной размерности");
            }
            for (int i = 0; i < x.Count; i++)
            {
                x[i] += y[i];
            }
        }

        public static void AddToRight(dVector x, dVector y)
        {
            if (x.Count != y.Count)
            {
                throw new Exception("При сложении векторов было обнаружено, что векторы разной размерности");
            }
            for (int i = 0; i < x.Count; i++)
            {
                y[i] += x[i];
            }
        }

        public static void AddToRight(iVector x, iVector y)
        {
            if (x.Count != y.Count)
            {
                throw new Exception("При сложении векторов было обнаружено, что векторы разной размерности");
            }
            for (int i = 0; i < x.Count; i++)
            {
                y[i] += x[i];
            }
        }

        public static void Scale(dVector x, double k)
        {
            for (int i = 0; i < x.Count; i++)
            {
                x[i] *= k;
            }
        }

        public static void Scale(iVector x, int k)
        {
            for (int i = 0; i < x.Count; i++)
            {
                x[i] *= k;
            }
        }

        public static dVector CreateAndScale(dVector x, double k)
        {
            dVector result = new dVector(x.Count);
            for (int i = 0; i < x.Count; i++)
            {
                result[i] = x[i] * k;
            }
            return result;
        }

        public static iVector CreateAndScale(iVector x, int k)
        {
            iVector result = new iVector(new int[x.Count]);
            for (int i = 0; i < x.Count; i++)
            {
                result[i] = x[i] * k;
            }
            return result;
        }

        public static void SubtrFromLeft(dVector x, dVector y)
        {
            if (x.Count != y.Count)
            {
                throw new Exception("При вычитании векторов было обнаружено, что векторы разной размерности");
            }
            for (int i = 0; i < x.Count; i++)
            {
                x[i] -= y[i];
            }
        }

        public static void SubtrFromLeft(iVector x, iVector y)
        {
            if (x.Count != y.Count)
            {
                throw new Exception("При вычитании векторов было обнаружено, что векторы разной размерности");
            }
            for (int i = 0; i < x.Count; i++)
            {
                x[i] -= y[i];
            }
        }

        public static void SubtrFromRight(dVector x, dVector y)
        {
            if (x.Count != y.Count)
            {
                throw new Exception("При вычитании векторов было обнаружено, что векторы разной размерности");
            }
            for (int i = 0; i < x.Count; i++)
            {
                y[i] -= x[i];
            }
        }

        public static void SubtrFromRight(iVector x, iVector y)
        {
            if (x.Count != y.Count)
            {
                throw new Exception("При вычитании векторов было обнаружено, что векторы разной размерности");
            }
            for (int i = 0; i < x.Count; i++)
            {
                y[i] -= x[i];
            }
        }

        public static void Swap(dVector x, dVector y)
        {
            var tmp = x;
            x = y;
            y = tmp;
        }

        public static void Swap(iVector x, iVector y)
        {
            var tmp = x;
            x = y;
            y = tmp;
        }

    }
}
