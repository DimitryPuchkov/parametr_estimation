using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ConsoleApp1
{
    public class InverseProblem : ForwardProblem
    {
        private double[] _actualReceiversPotentialDiff;
        public double[] ActualReceiversPotentialDiff
        {
            get => _actualReceiversPotentialDiff;
            init
            {
                if (!DimensionGuarantor.IsDimensionsCoincideArrOfPairCoorsOutside(ReceiversСoor, value))
                {
                    throw new Exception("Размерность вектора разности потенциалов не совпадает с " +
                                        "размерностью вектора координат приемников");
                }
                _actualReceiversPotentialDiff = value;
            }

        }

        //public double Sig

        public InverseProblem((double[], double[])[] sourcesСoor, (double[], double[])[] receiversСoor, double[] sourcesCurrentStrength,
                              double[] receiversPotentialDiff, double sigma, int problemDimension = 3) :
                              base(sourcesСoor, receiversСoor, sourcesCurrentStrength, sigma, problemDimension)
        {
            ActualReceiversPotentialDiff = receiversPotentialDiff;
        }

        public InverseProblem(ForwardProblem forwardProblem, double sigma, double[] receiversPotentialDiff) :
                              base(forwardProblem.SourcesСoor, forwardProblem.ReceiversСoor,
                              forwardProblem.SourcesCurrentStrength, sigma, forwardProblem.ProblemDimension)
        {
            ActualReceiversPotentialDiff = receiversPotentialDiff;
        }

        /// <summary>
        /// The difference between the theoretical output from the assumed data and 
        /// the practical output from the real data
        /// </summary>
        protected double DeltaEP(int i)
        {
            var hypotheticalValues = EP(ReceiversСoor[i]);
            var realValues = ActualReceiversPotentialDiff[i];

            return hypotheticalValues - realValues;
        }

        /// <summary>
        /// Calculates the analytical derivative with respect to sigma of the potential dif-ference for one receiver
        /// </summary>
        /// <param name="q"> For the index of the source by which we differentiate</param>
        /// <param name="k"> For the receiver index</param>
        protected double DEP(int q, int k)
        {
            double forPotential1 = 1 / R(SourcesСoor[q].Item1, ReceiversСoor[k].Item1) - 1 / R(SourcesСoor[q].Item2, ReceiversСoor[k].Item1);
            double forPotential2 = 1 / R(SourcesСoor[q].Item1, ReceiversСoor[k].Item2) - 1 / R(SourcesСoor[q].Item2, ReceiversСoor[k].Item2);
            return -SourcesCurrentStrength[q] * (forPotential1 - forPotential2) / (2 * Math.PI * Sigma * Sigma);
        }

        /// <summary>
        /// Calculates coefficients for a weighted sum
        /// </summary>
        protected double W(int k)
        {
            return 1 / ActualReceiversPotentialDiff[k];
        }

        /// <summary>
        /// Calculates an element of the matrix to solve the inverse problem
        /// </summary>
        public double AQS(int q, int s)
        {
            double sum = 0;
            for (int i = 0; i < ActualReceiversPotentialDiff.Length; i++)
            {
                sum += W(i) * W(i) * DEP(q, i) * DEP(s, i);
            }
            return sum;
        }

        /// <summary>
        /// Calculates an element of the right vector to solve the inverse problem
        /// </summary>
        public double BQ(int q)
        {
            double sum = 0;
            for (int i = 0; i < ActualReceiversPotentialDiff.Length; i++)
            {
                sum -= W(i) * W(i) * DeltaEP(i) * DEP(q, i);
            }
            return sum;
        }

        /// <summary>
        /// Does one iteration to solve the inverse problem. Return delta sigma for new sigma
        /// </summary>
        public double[] Iter(DenseSLAE SLAE)
        {
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    SLAE.A[i][j] = AQS(i, j);
                }
            }

            for (int i = 0; i < 1; i++)
            {
                SLAE.B[i] = BQ(i);
            }

            return SLAE.SolveSLAEGauss();
        }

        /// <summary>
        /// Without regularization, analytical derivatives, the matrix is solved by the Gauss method
        /// </summary>
        /// <param name="acc"> absolute accuracy of the solution </param>
        /// <returns></returns>
        public double SolveInverseProblem(double acc, int maxIter)
        {
            double[][] A = new double[1][];
            for (int i = 0; i < 1; i++)
            {
                A[i] = new double[1];
            }
            double[] B = new double[1];
            DenseSLAE SLAE = new(A, B, 1);

            double[] errorVector = CalcErrorVector();

            for (int i = 0; i < maxIter && VectorAlgebra.Norm(errorVector) > acc; i++)
            {
                Sigma += Iter(SLAE)[0];
                errorVector = CalcErrorVector();
            }

            return Sigma;
        }

        protected double[] CalcErrorVector()
        {
            double[] errorVector = new double[ActualReceiversPotentialDiff.Length];
            for (int i = 0; i < ActualReceiversPotentialDiff.Length; i++)
            {
                errorVector[i] = EP(ReceiversСoor[i]);
            }
            VectorAlgebra.SubtrFromLeft(errorVector, ActualReceiversPotentialDiff);
            return errorVector;
        }

        /// <summary>
        /// Without regularization, analytical derivatives, the matrix is solved by the Gauss method, with log
        /// </summary>
        /// <param name="acc"> absolute accuracy of the solution </param>
        /// <returns></returns>
        public double SolveInverseProblemWithLog(double acc, int maxIter)
        {
            double[][] A = new double[1][];
            for (int i = 0; i < 1; i++)
            {
                A[i] = new double[1];
            }
            double[] B = new double[1];
            DenseSLAE SLAE = new(A, B, 1);

            double[] errorVector = CalcErrorVector();

            Console.Write("Изначальный: ");
            WriteLineSigma();

            for (int i = 0; i < maxIter && VectorAlgebra.Norm(errorVector) > acc; i++)
            {
                Sigma += Iter(SLAE)[0];
                Console.Write($"{i + 1} итерация: ");
                WriteLineSigma();
                errorVector = CalcErrorVector();
            }
            Console.WriteLine();
            Console.Write("Вывод: ");
            WriteLineSigma();


            return Sigma;
        }

        public void WriteLineSigma()
        {
            Console.WriteLine($"{Sigma.ToString("E")}  ");
        }

    }
}
