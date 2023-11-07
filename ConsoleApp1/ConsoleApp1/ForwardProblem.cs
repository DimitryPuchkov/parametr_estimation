using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ConsoleApp1
{
    public class ForwardProblem
    {
        public int ProblemDimension { get; init; }

        private (double[], double[])[] _receiversСoor;
        public (double[], double[])[] ReceiversСoor
        {
            get => _receiversСoor;
            init
            {
                if (!DimensionGuarantor.IsDimensionsCoincideArrOfPairCoorsInside(value, ProblemDimension))
                {
                    throw new Exception("Размерность некоторого вектора приемника не совпадает с размерностью задачи");
                }
                _receiversСoor = value;
            }
        }

        private (double[], double[])[] _sourcesСoor;
        public (double[], double[])[] SourcesСoor
        {
            get => _sourcesСoor;
            init
            {
                if (!DimensionGuarantor.IsDimensionsCoincideArrOfPairCoorsInside(value, ProblemDimension))
                {
                    throw new Exception("Размерность некоторого исходного вектора не совпадает с размерностью задачи");
                }
                _sourcesСoor = value;
            }
        }

        public double Sigma { get; set; }

        private double[] _sourcesCurrentStrength;
        public double[] SourcesCurrentStrength
        {
            get => _sourcesCurrentStrength;
            set
            {
                if (!DimensionGuarantor.IsDimensionsCoincideArrOfPairCoorsOutside(SourcesСoor, value))
                {
                    throw new Exception("Размерность вектора текущей мощности не совпадает с " +
                                        "размерностью вектора координат источников");
                }
                _sourcesCurrentStrength = value;
            }

        }

        public ForwardProblem((double[], double[])[] sourcesСoor, (double[], double[])[] receiversСoor,
                              double[] sourcesCurrentStrength, double sigma, int problemDimension = 3)
        {
            ProblemDimension = problemDimension;
            Sigma = sigma;
            SourcesСoor = sourcesСoor;
            ReceiversСoor = receiversСoor;
            SourcesCurrentStrength = sourcesCurrentStrength;
        }

        public double[] SolveForwardProblem()
        {
            double[] receiversPotentialDifference = new double[ReceiversСoor.Length];
            for (int i = 0; i < ProblemDimension; i++)
            {
                receiversPotentialDifference[i] = EP(ReceiversСoor[i]);
            }
            return receiversPotentialDifference;
        }

        /// <summary>
        /// Distance between two points in Cartesian coordinates
        /// </summary>
        protected double R(double[] a, double[] b)
        {
            double sum = 0;
            for (int i = 0; i < ProblemDimension; i++)
            {
                sum += Math.Pow(Math.Abs(a[i] - b[i]), 2);
            }
            return Math.Sqrt(sum);
        }

        /// <summary>
        /// Calculates the potential difference if there were one receiver and one source
        /// </summary>
        protected double OneToOneEP((double[], double[]) sourceCoor, (double[], double[]) receiverCoor, double currentStrength)
        {
            double forPotential1 = 1 / R(sourceCoor.Item1, receiverCoor.Item1) - 1 / R(sourceCoor.Item2, receiverCoor.Item1);
            double forPotential2 = 1 / R(sourceCoor.Item1, receiverCoor.Item2) - 1 / R(sourceCoor.Item2, receiverCoor.Item2);
            return (forPotential1 - forPotential2) * currentStrength / (2 * Math.PI * Sigma);
        }

        /// <summary>
        /// Calculates the potential difference for one receiver
        /// </summary>
        protected double EP((double[], double[]) receiverCoor)
        {
            double sum = 0;
            for (int i = 0; i < SourcesСoor.Length; i++)
            {
                sum += OneToOneEP(SourcesСoor[i], receiverCoor, SourcesCurrentStrength[i]);
            }
            return sum;
        }

    }
}
