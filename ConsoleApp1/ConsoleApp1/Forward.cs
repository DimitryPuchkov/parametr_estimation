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
    public class Forward
    {
        public int ProblemDimension;

        public (dVector, dVector)[] ReceiversСoor;

        public (dVector, dVector)[] SourcesСoor;

        public double Sigma;

        public dVector SourcesCurrentStrength;

        public Forward((dVector, dVector)[] sourcesСoor, (dVector, dVector)[] receiversСoor,
                              dVector sourcesCurrentStrength, double sigma, int problemDimension = 3)
        {
            ProblemDimension = problemDimension;
            Sigma = sigma;
            SourcesСoor = sourcesСoor;
            ReceiversСoor = receiversСoor;
            SourcesCurrentStrength = sourcesCurrentStrength;
        }

        // прямая задача
        public dVector SolveForwardProblem()
        {
            dVector receiversPotentialDifference = new dVector(new double[ReceiversСoor.Length]);
            for (int i = 0; i < ProblemDimension; i++)
            {
                receiversPotentialDifference[i] = EP(ReceiversСoor[i]);
            }
            return receiversPotentialDifference;
        }

        // расстояние
        public double R(dVector a, dVector b)
        {
            double sum = 0;
            for (int i = 0; i < ProblemDimension; i++)
            {
                sum += Math.Pow(Math.Abs(a[i] - b[i]), 2);
            }
            return Math.Sqrt(sum);
        }
        // разность потенциалов
        public double OneToOneEP((dVector, dVector) sourceCoor, (dVector, dVector) receiverCoor, double currentStrength)
        {
            double forPotential1 = 1 / R(sourceCoor.Item1, receiverCoor.Item1) - 1 / R(sourceCoor.Item2, receiverCoor.Item1);
            double forPotential2 = 1 / R(sourceCoor.Item1, receiverCoor.Item2) - 1 / R(sourceCoor.Item2, receiverCoor.Item2);
            return (forPotential1 - forPotential2) * currentStrength / (2 * Math.PI * Sigma);
        }

        // сложение разности потенциалов
        public double EP((dVector, dVector) receiverCoor)
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
