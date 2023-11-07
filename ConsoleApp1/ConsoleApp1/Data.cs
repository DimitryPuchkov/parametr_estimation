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
    internal class Data
    {
        public static (Forward, dVector) FirstTestFromTrainingManual()
        {
            // Источники
            (dVector, dVector)[] sourcesCoor = new (dVector, dVector)[1];
            dVector A = new dVector { 0, 0, 0 };
            dVector B = new dVector { 100, 0, 0 };
            sourcesCoor[0] = (A, B);

            // Приёмники
            (dVector, dVector)[] receiversCoor = new (dVector, dVector)[3];
            dVector M1 = new dVector{ 200, 0, 0 };
            dVector N1 = new dVector { 300, 0, 0 };
            dVector M2 = new dVector { 500, 0, 0 };
            dVector N2 = new dVector { 600, 0, 0 };
            dVector M3 = new dVector { 1000, 0, 0 };
            dVector N3 = new dVector { 1100, 0, 0 };
            receiversCoor[0] = (N1, M1);
            receiversCoor[1] = (N2, M2);
            receiversCoor[2] = (N3, M3);

            dVector I = new dVector { 1 }; // Сила тока
            double sigma = 0.1;

            Forward syntheticDataGenerator = new(sourcesCoor, receiversCoor, I, sigma, 3);

            return (syntheticDataGenerator, syntheticDataGenerator.SolveForwardProblem());
        }

    }
}
