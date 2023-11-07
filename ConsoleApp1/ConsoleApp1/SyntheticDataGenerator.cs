using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ConsoleApp1
{
    internal class SyntheticDataGenerator
    {
        /// <summary>
        /// sigma = 0.1
        /// </summary>
        public static (ForwardProblem, double[]) FirstTestFromTrainingManual()
        {
            (double[], double[])[] sourcesCoor = new (double[], double[])[1];
            double[] A = { 0, 0, 0 };
            double[] B = { 100, 0, 0 };
            sourcesCoor[0] = (A, B);

            (double[], double[])[] receiversCoor = new (double[], double[])[3];
            double[] M1 = { 200, 0, 0 };
            double[] N1 = { 300, 0, 0 };
            double[] M2 = { 500, 0, 0 };
            double[] N2 = { 600, 0, 0 };
            double[] M3 = { 1000, 0, 0 };
            double[] N3 = { 1100, 0, 0 };
            receiversCoor[0] = (N1, M1);
            receiversCoor[1] = (N2, M2);
            receiversCoor[2] = (N3, M3);

            double[] I = new double[] { 1 };
            double sigma = 0.1;

            ForwardProblem syntheticDataGenerator = new(sourcesCoor, receiversCoor, I, sigma, 3);

            return (syntheticDataGenerator, syntheticDataGenerator.SolveForwardProblem());
        }

    }
}
