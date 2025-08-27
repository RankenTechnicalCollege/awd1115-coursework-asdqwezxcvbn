using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] testScores = { 100, 90, 30, 88, 75, 93 };

            int maxScore = testScores.Max();
            int minScore = testScores.Min();
            double avgScore = testScores.Average();
            int sumScores = testScores.Sum();

            Console.WriteLine($"Max Score: {maxScore}");
            Console.WriteLine($"Min Score: {minScore}");
            Console.WriteLine($"Average Score: {avgScore}");
            Console.WriteLine($"Sum of Scores: {sumScores}");
        }
    }
}