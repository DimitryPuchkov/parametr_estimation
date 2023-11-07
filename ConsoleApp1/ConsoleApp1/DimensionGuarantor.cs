using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ConsoleApp1
{
    internal class DimensionGuarantor
    {
        public static bool IsDimensionsCoincideArrOfPairCoorsInside((double[], double[])[] coordinates, int dimension)
        {
            foreach (var vectorPair in coordinates)
            {
                if (vectorPair.Item1.Length != dimension || vectorPair.Item2.Length != dimension)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsDimensionsCoincideArrOfPairCoorsOutside((double[], double[])[] coordinates, double[] vector)
        {
            if (coordinates.Length != vector.Length)
            {
                return false;
            }
            return true;
        }

        public static bool IsDimensionsCoincide(double[] vector, int dimension)
        {
            if (vector.Length != dimension)
            {
                return false;
            }
            return true;
        }

        public static bool IsDimensionsCoincide(int[] vector, int dimension)
        {
            if (vector.Length != dimension)
            {
                return false;
            }
            return true;
        }

        public static bool IsDimensionsCoincideMatrixFull(double[][] matrix, int dimension)
        {
            if (matrix.Length != dimension)
            {
                return false;
            }
            foreach (var str in matrix)
            {
                if (str.Length != dimension)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
