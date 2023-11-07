using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using dVector = System.Collections.Generic.List<double>;
using iVector = System.Collections.Generic.List<int>;


namespace ConsoleApp1
{
    internal class DimensionGuarantor
    {
        public static bool IsDimensionsCoincideArrOfPairCoorsInside((dVector, dVector)[] coordinates, int dimension)
        {
            foreach (var vectorPair in coordinates)
            {
                if (vectorPair.Item1.Count != dimension || vectorPair.Item2.Count != dimension)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsDimensionsCoincideArrOfPairCoorsOutside((dVector, dVector)[] coordinates, dVector vector)
        {
            if (coordinates.Length != vector.Count)
            {
                return false;
            }
            return true;
        }

        public static bool IsDimensionsCoincide(dVector vector, int dimension)
        {
            if (vector.Count != dimension)
            {
                return false;
            }
            return true;
        }

        public static bool IsDimensionsCoincide(iVector vector, int dimension)
        {
            if (vector.Count != dimension)
            {
                return false;
            }
            return true;
        }

        public static bool IsDimensionsCoincideMatrixFull(dVector[] matrix, int dimension)
        {
            if (matrix.Length != dimension)
            {
                return false;
            }
            foreach (var str in matrix)
            {
                if (str.Count != dimension)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
