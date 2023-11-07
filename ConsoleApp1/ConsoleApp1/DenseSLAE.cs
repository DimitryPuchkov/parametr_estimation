using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ConsoleApp1
{
    public class DenseSLAE
    {
        private double[][] _a;
        public double[][] A
        {
            get => _a;
            set
            {
                if (!DimensionGuarantor.IsDimensionsCoincideMatrixFull(value, MatrixDimension))
                {
                    throw new Exception("Размерность матрицы A не совпадает с размерностью СЛАУ");
                }
                _a = value;
            }
        }

        private double[] _b;
        public double[] B
        {
            get => _b;
            set
            {
                if (!DimensionGuarantor.IsDimensionsCoincide(value, MatrixDimension))
                {
                    throw new Exception("Размерность вектора b не совпадает с размерностью СЛАУ");
                }
                _b = value;
            }
        }
        public int MatrixDimension { get; init; }

        /// <summary>
        /// We will consider the number x as zero (for example, when solving SLAE by the Gauss method) if: -Near Zero < x < NearZero
        /// </summary>
        public double NearZero { get; set; } = 1e-14;

        public DenseSLAE(double[][] a, double[] b, int matrixDimension)
        {
            MatrixDimension = matrixDimension;
            A = a;
            B = b;
        }

        private bool IsNearZero(double x)
        {
            if (x < NearZero && x > -NearZero)
            {
                return true;
            }
            return false;
        }

        public double[] SolveSLAEGauss()
        {
            for (int i = 0; i < MatrixDimension; i++)
            {
                if (IsNearZero(A[i][i]))
                {
                    bool foundStringForSwap = false;
                    for (int j = i + 1; j < MatrixDimension; j++)
                    {
                        if (IsNearZero(A[j][i]))
                        {
                            continue;
                        }
                        VectorAlgebra.Swap(A[j], A[i]);
                        foundStringForSwap = true;
                        break;
                    }
                    if (foundStringForSwap == false)
                    {
                        throw new UnsolvableMatrixException("СЛАУ не решается");
                    }
                }
                for (int j = i + 1; j < MatrixDimension; j++)
                {
                    double coef = A[j][i] / A[i][i];
                    VectorAlgebra.SubtrFromLeft(A[j], VectorAlgebra.CreateAndScale(A[i], coef));
                    B[j] -= B[i] * coef;
                }
            }
            double[] result = new double[MatrixDimension];
            for (int i = MatrixDimension - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < MatrixDimension; j++)
                {
                    sum += result[j] * A[i][j];
                }
                result[i] = (B[i] - sum) / A[i][i];
            }
            return result;
        }

    }
}
