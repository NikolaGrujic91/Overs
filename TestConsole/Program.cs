using System;
using System.Collections.Generic;
using OversDetector;

namespace TestConsole
{
    internal static class Program
    {
        private static OversDetector.OversDetector oversDetector;

        private static void Main()
        {
            Run();
            PrintCandidates(oversDetector.Candidates);
            Console.ReadLine();
        }

        private static void Run()
        {
            try
            {
                TryRun();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void TryRun()
        {
            oversDetector = new OversDetector.OversDetector();
            oversDetector.Run();
        }

        private static void PrintCandidates(IEnumerable<Candidate> candidates)
        {
            foreach (Candidate candidate in candidates)
            {
                Console.WriteLine(candidate.ToString());
            }
        }
    }
}
