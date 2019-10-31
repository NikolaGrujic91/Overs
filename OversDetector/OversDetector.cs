using System;
using System.Collections.Generic;
using DataCollector;

namespace OversDetector
{
    public class OversDetector
    {
        #region Constructors

        public OversDetector()
        {
            var collector = new Collector();

            collector.Collect();

            this.Fixtures = collector.Fixtures;
            this.Results = collector.Results;

            this.Candidates = new List<Candidate>();
        }

        #endregion

        #region Properties

        public List<Candidate> Candidates { get; private set; }

        private List<Fixture> Fixtures { get; }

        private Dictionary<string, List<Fixture>> Results { get; }

        #endregion

        #region Methods

        public void Run()
        {
            // Algorithm

            //Home team
            //The home team MUST have had 7 goals or MORE in their last 3 home games.
            //2 or all 3 of the 3 previous games must have ended over 2.5

            //Away Team
            //The away team MUST have had 7 goals or MORE in their last 3 away games.
            //The PREVIOUS game... must have had 2 or more goals in total for the entire game.
            //The away team MUST have scored in 2 or 3 of the last 3 games.
            //2 or 3 of the 3 previous games must have ended over 2.5.

            foreach (Fixture fixture in this.Fixtures)
            {
                if (!this.CheckOverOdd(fixture.OverOdd))
                {
                    continue;
                }

                List<Fixture> homeTeamPreviousFixtures = this.GetHomeTeamResults(fixture.HomeTeam, fixture.League);

                if (this.NumberOfGoals(homeTeamPreviousFixtures) < 7)
                {
                    continue;
                }

                if (this.NumberOfOverFixtures(homeTeamPreviousFixtures) < 2)
                {
                    continue;
                }

                List<Fixture> awayTeamPreviousFixtures = this.GetAwayTeamResults(fixture.AwayTeam, fixture.League);

                if (this.NumberOfGoals(awayTeamPreviousFixtures) < 7)
                {
                    continue;
                }

                if (this.NumberOfGoals(new List<Fixture>{awayTeamPreviousFixtures[0]}) < 2)
                {
                    continue;
                }

                if (this.NumberOfOverFixtures(awayTeamPreviousFixtures) < 2)
                {
                    continue;
                }

                if (this.TeamScored(awayTeamPreviousFixtures) < 2)
                {
                    continue;
                }

                var candidate = new Candidate(fixture, homeTeamPreviousFixtures, awayTeamPreviousFixtures);
                this.Candidates.Add(candidate);
            }
        }

        private int TeamScored(List<Fixture> awayTeamPreviousFixtures)
        {
            int scored = 0;

            foreach (Fixture fixture in awayTeamPreviousFixtures)
            {
                if (fixture.AwayScore > 0)
                {
                    scored++;
                }
            }

            return scored;
        }

        private bool CheckOverOdd(double fixtureOverOdd)
        {
            return 1.60 <= fixtureOverOdd && fixtureOverOdd <= 2.20;
        }

        private int NumberOfGoals(List<Fixture> previousFixtures)
        {
            int totalGoals = 0;

            foreach (Fixture fixture in previousFixtures)
            {
                totalGoals += fixture.HomeScore + fixture.AwayScore;
            }

            return totalGoals;
        }

        private int NumberOfOverFixtures(List<Fixture> previousFixtures)
        {
            int overFixtures = 0;

            foreach (Fixture fixture in previousFixtures)
            {
                if ((fixture.HomeScore + fixture.AwayScore) > 2)
                {
                    overFixtures++;
                }
            }

            return overFixtures;
        }

        private List<Fixture> GetHomeTeamResults(string teamName, string league)
        {
            var recentResults = new List<Fixture>();

            if (this.Results.ContainsKey(league))
            {
                List<Fixture> results = this.Results[league];

                int recentCount = 3;
                int count = results.Count;

                for (int i = count - 1; i >= 0; i--)
                {
                    if (results[i].HomeTeam == teamName)
                    {
                        recentResults.Add(results[i]);
                        recentCount--;

                        if (recentCount == 0)
                        {
                            break;
                        }
                    }
                }
            }

            return recentResults;
        }

        private List<Fixture> GetAwayTeamResults(string teamName, string league)
        {
            var recentResults = new List<Fixture>();

            if (this.Results.ContainsKey(league))
            {
                List<Fixture> results = this.Results[league];

                int recentCount = 3;
                int count = results.Count;

                for (int i = count - 1; i >= 0; i--)
                {
                    if (results[i].AwayTeam == teamName)
                    {
                        recentResults.Add(results[i]);
                        recentCount--;

                        if (recentCount == 0)
                        {
                            break;
                        }
                    }
                }
            }

            return recentResults;
        }

        #endregion
    }
}
