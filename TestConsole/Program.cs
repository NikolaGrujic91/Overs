using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataCollector;
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
            CreateMarkdownTable(oversDetector.Candidates);
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

        private static void PrintCandidates(List<Candidate> candidates)
        {
            if (candidates.Count == 0)
            {
                Console.WriteLine("No candidate fixtures found.");
                return;
            }

            foreach (Candidate candidate in candidates)
            {
                Console.WriteLine(candidate.ToString());
            }
        }

        private static void CreateMarkdownTable(IEnumerable<Candidate> candidates)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"## {DateTime.Now:dd.MM.yyyy}");

            stringBuilder.AppendLine("|   | League | Date | Home team | Away team | Over odd | Result |   |");
            stringBuilder.AppendLine("|---|--------|------|-----------|-----------|----------|--------|---|");

            int rowNumber = 1;

            foreach (Candidate candidate in candidates)
            {
                Fixture fixture = candidate.CandidateFixture;
                stringBuilder.AppendLine($"| {rowNumber} |{fixture.League}|{fixture.Date:dd.MM.yyyy}|{fixture.HomeTeam}|{fixture.AwayTeam}|{fixture.OverOdd:#.##}|         |   |");
                rowNumber++;
            }

            stringBuilder.AppendLine("|   |        |      |           |           |          |        |   |");

            var file = new StreamWriter($"{DateTime.Now:dd.MM.yyyy}.md");
            file.Write(stringBuilder.ToString());
            file.Close();
        }
    }
}
