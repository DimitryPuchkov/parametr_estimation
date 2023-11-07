using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ConsoleApp1
{
    internal class ForTest
    {
        /// <summary>
        /// 1 source, 3 receivers, sigma = 0.1, initial approximation = 0.01, accuracy = 1e-14, maxIter = 1000
        /// </summary>
        public static void CheckFirstTestFromTrainingManual()
        {
            (ForwardProblem problemFromTrainingManual, double[] ReceiversPotentialDiff) =
                                                        SyntheticDataGenerator.FirstTestFromTrainingManual();

            double new_sigma = 0.01;

            InverseProblem inverseProblem = new(problemFromTrainingManual, new_sigma, ReceiversPotentialDiff);
            inverseProblem.SolveInverseProblemWithLog(1e-10, 1000);
        }

        public static void CheckFirstTestFromTrainingManualPlus10Percent()
        {
            (ForwardProblem problemFromTrainingManual, double[] ReceiversPotentialDiff) =
                                                        SyntheticDataGenerator.FirstTestFromTrainingManual();

            double new_sigma = 0.01;

            for (int i = 0; i < ReceiversPotentialDiff.Length; i++)
            {
                ReceiversPotentialDiff[i] *= 1.1;
            }

            InverseProblem inverseProblem = new(problemFromTrainingManual, new_sigma, ReceiversPotentialDiff);
            inverseProblem.SolveInverseProblemWithLog(1e-10, 1000);
        }

        public static void CheckFirstTestFromTrainingManualMinus10Percent()
        {
            (ForwardProblem problemFromTrainingManual, double[] ReceiversPotentialDiff) =
                                                        SyntheticDataGenerator.FirstTestFromTrainingManual();

            double new_sigma = 0.01;

            for (int i = 0; i < ReceiversPotentialDiff.Length; i++)
            {
                ReceiversPotentialDiff[i] *= 0.9;
            }

            InverseProblem inverseProblem = new(problemFromTrainingManual, new_sigma, ReceiversPotentialDiff);
            inverseProblem.SolveInverseProblemWithLog(1e-10, 1000);
        }

        public static void CheckFirstTestFromTrainingManualBreaker()
        {
            (ForwardProblem problemFromTrainingManual, double[] ReceiversPotentialDiff) =
                                                        SyntheticDataGenerator.FirstTestFromTrainingManual();

            double new_sigma = 0.01;

            ReceiversPotentialDiff[0] *= 1.01;
            ReceiversPotentialDiff[1] *= 1;
            ReceiversPotentialDiff[2] *= 1;

            InverseProblem inverseProblem = new(problemFromTrainingManual, new_sigma, ReceiversPotentialDiff);
            inverseProblem.SolveInverseProblemWithLog(1e-10, 1000);
        }

    }
}
