using System;

namespace Program
{
    public class Program
    {
        private static readonly Random _random = new Random();

        public static void Main()
        {
            double[][] state = new double[4][];
            
            state[0] = new[] { 0.3, 0.5, 0.8, double.NaN };
            state[1] = new[] { 0, 0.2, 0.5, 0.7 };
            state[2] = new[] { double.NaN, 0, 0.4, 0.6 };
            state[3] = new[] { double.NaN, double.NaN, 0, 0.1 };

            double leadingMin = 0.6, leadingMax = 0.8, leadingIntensity = 4;
            
            double coordMin1 = 0.3, coordMax1 = 0.4, coordIntensity1 = 8;
            double coordMin2 = 0.2, coordMax2 = 0.3, coordIntensity2 = 10;

            int currentIndex = 0;
            double currentState = 0.0;

            for (int i = 1; i <= 30; i++)
            {
                Console.Write($"Day {i} ");
                
                ProcessInfluence(state, ref currentIndex, ref currentState, ref leadingIntensity, leadingMin, leadingMax, "Leading");
                ProcessInfluence(state, ref currentIndex, ref currentState, ref coordIntensity1, coordMin1, coordMax1, "First coordination");
                ProcessInfluence(state, ref currentIndex, ref currentState, ref coordIntensity2, coordMin2, coordMax2, "Second coordination");
                
                Console.WriteLine();
            }
        }

        private static void ProcessInfluence(double[][] state, ref int currentIndex, ref double currentState, ref double intensity, double min, double max, string influenceType)
        {
            if (currentIndex <= 3 && IsInfluenceEvent(intensity))
            {
                double power = RandomInfluence(min, max);
                
                currentState += power;
                intensity--;

                for (int j = 3; j >= 0; j--)
                {
                    if (state[currentIndex][j] <= currentState && state[currentIndex][j] != 0)
                    {
                        currentState -= state[currentIndex][j];
                        currentIndex = j + 1;
                        break;
                    }
                }

                Console.Write($"{influenceType} influence has occurred with the power of {power} with the overall power of {currentState} and current level of {currentIndex}");
            }
        }

        private static bool IsInfluenceEvent(double intensity)
        {
            return _random.NextDouble() < intensity / 30;
        }

        private static double RandomInfluence(double minValue, double maxValue)
        {
            return minValue + (maxValue - minValue) * _random.NextDouble();
        }
    }
}
