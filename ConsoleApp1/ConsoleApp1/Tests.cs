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
    internal class Tests
    {
        /// <summary>
        /// 1 источник, 
        /// 3 приёмник, sigma = 0.1, 
        /// начальная апроксимация = 0.01,
        /// точность = 1e-10, 
        /// максимальное кол-во итераций = 1000
        /// </summary>
        public static void CheckFirstTestFromTrainingManual()
        {
            (Forward problemFromTrainingManual, dVector ReceiversPotentialDiff) =
                                                        Data.FirstTestFromTrainingManual();

            double new_sigma = 0.01;

            Inverse inverseProblem = new(problemFromTrainingManual, new_sigma, ReceiversPotentialDiff);
            inverseProblem.SolveInverseProblemWithLog(1e-10, 1000);
        }

        public static void CheckFirstTestFromTrainingManualPlus10Percent()
        {
            (Forward problemFromTrainingManual, dVector ReceiversPotentialDiff) =
                                                        Data.FirstTestFromTrainingManual();

            double new_sigma = 0.01;

            for (int i = 0; i < ReceiversPotentialDiff.Count; i++)
            {
                ReceiversPotentialDiff[i] *= 1.1;
            }

            Inverse inverseProblem = new(problemFromTrainingManual, new_sigma, ReceiversPotentialDiff);
            inverseProblem.SolveInverseProblemWithLog(1e-10, 1000);
        }

        public static void CheckFirstTestFromTrainingManualMinus10Percent()
        {
            (Forward problemFromTrainingManual, dVector ReceiversPotentialDiff) =
                                                        Data.FirstTestFromTrainingManual();

            double new_sigma = 0.01;

            for (int i = 0; i < ReceiversPotentialDiff.Count; i++)
            {
                ReceiversPotentialDiff[i] *= 0.9;
            }

            Inverse inverseProblem = new(problemFromTrainingManual, new_sigma, ReceiversPotentialDiff);
            inverseProblem.SolveInverseProblemWithLog(1e-10, 1000);
        }

        public static void CheckFirstTestFromTrainingManualBreaker()
        {
            (Forward problemFromTrainingManual, dVector ReceiversPotentialDiff) =
                                                        Data.FirstTestFromTrainingManual();

            double new_sigma = 0.01;

            ReceiversPotentialDiff[0] *= 1.01;
            ReceiversPotentialDiff[1] *= 1;
            ReceiversPotentialDiff[2] *= 1;

            Inverse inverseProblem = new(problemFromTrainingManual, new_sigma, ReceiversPotentialDiff);
            inverseProblem.SolveInverseProblemWithLog(1e-10, 1000);
        }

    }
}
