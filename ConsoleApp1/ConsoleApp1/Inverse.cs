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
    public class Inverse : Forward
    {
        //private dVector _actualReceiversPotentialDiff;
        public dVector ActualReceiversPotentialDiff;

        public Inverse((dVector, dVector)[] sourcesСoor, (dVector, dVector)[] receiversСoor, dVector sourcesCurrentStrength,
                              dVector receiversPotentialDiff, double sigma, int problemDimension = 3) :
                              base(sourcesСoor, receiversСoor, sourcesCurrentStrength, sigma, problemDimension)
        {
            ActualReceiversPotentialDiff = receiversPotentialDiff;
        }

        public Inverse(Forward forwardProblem, double sigma, dVector receiversPotentialDiff) :
                              base(forwardProblem.SourcesСoor, forwardProblem.ReceiversСoor,
                              forwardProblem.SourcesCurrentStrength, sigma, forwardProblem.ProblemDimension)
        {
            ActualReceiversPotentialDiff = receiversPotentialDiff;
        }

        public double DeltaEP(int i)
        {
            var hypotheticalValues = EP(ReceiversСoor[i]);
            var realValues = ActualReceiversPotentialDiff[i];

            return hypotheticalValues - realValues;
        }

        public double DEP(int q, int k)
        {
            double forPotential1 = 1 / R(SourcesСoor[q].Item1, ReceiversСoor[k].Item1) - 1 / R(SourcesСoor[q].Item2, ReceiversСoor[k].Item1);
            double forPotential2 = 1 / R(SourcesСoor[q].Item1, ReceiversСoor[k].Item2) - 1 / R(SourcesСoor[q].Item2, ReceiversСoor[k].Item2);
            return -SourcesCurrentStrength[q] * (forPotential1 - forPotential2) / (2 * Math.PI * Sigma * Sigma);
        }

        public double W(int k)
        {
            return 1 / ActualReceiversPotentialDiff[k];
        }

        public double AQS(int q, int s)
        {
            double sum = 0;
            for (int i = 0; i < ActualReceiversPotentialDiff.Count; i++)
            {
                sum += W(i) * W(i) * DEP(q, i) * DEP(s, i);
            }
            return sum;
        }

        public double BQ(int q)
        {
            double sum = 0;
            for (int i = 0; i < ActualReceiversPotentialDiff.Count; i++)
            {
                sum -= W(i) * W(i) * DeltaEP(i) * DEP(q, i);
            }
            return sum;
        }

        public dVector Iter(SLAU SLAE)
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

        public double SolveInverseProblem(double acc, int maxIter)
        {
            //dVector a_ = new dVector(1);
            dVector[] A = new dVector[100];
            A[1] = new dVector(new double[1]);
            for (int i = 0; i < 1; i++)
            {
                A[i] = new dVector(new double[1]);
            }
            dVector B = new dVector(new double[1]);
            SLAU SLAE = new(A, B, 1);

            dVector errorVector = CalcErrorVector();

            for (int i = 0; i < maxIter && Algebra.Norm(errorVector) > acc; i++)
            {
                Sigma += Iter(SLAE)[0];
                errorVector = CalcErrorVector();
            }

            return Sigma;
        }

        public dVector CalcErrorVector()
        {
            dVector errorVector = new dVector(new double[ActualReceiversPotentialDiff.Count]);
            for (int i = 0; i < ActualReceiversPotentialDiff.Count; i++)
            {
                errorVector[i] = EP(ReceiversСoor[i]);
            }
            Algebra.SubtrFromLeft(errorVector, ActualReceiversPotentialDiff);
            return errorVector;
        }

        public double SolveInverseProblemWithLog(double acc, int maxIter)
        {
            dVector[] A = new dVector[100];
            A[1] = new dVector(new double[1]);
            for (int i = 0; i < 1; i++)
            {
                A[i] = new dVector(new double[1]);
            }
            dVector B = new dVector(new double[1]);
            SLAU SLAE = new(A, B, 1);

            dVector errorVector = CalcErrorVector();

            Console.Write("Изначальный: ");
            WriteLineSigma();

            for (int i = 0; i < maxIter && Algebra.Norm(errorVector) > acc; i++)
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
