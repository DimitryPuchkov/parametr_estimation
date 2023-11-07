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
    public class DenseSLAE
        
    {
        public dVector[] A;

        public dVector B;

        public int MatrixDimension;
        public double NearZero = 1e-14;

        public DenseSLAE(dVector[] a, dVector b, int matrixDimension)
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

        public dVector SolveSLAEGauss()
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
            dVector result = new dVector(new double[MatrixDimension]);
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
