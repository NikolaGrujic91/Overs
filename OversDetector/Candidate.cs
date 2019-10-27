using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollector;

namespace OversDetector
{
    public class Candidate
    {
        #region Constructors

        public Candidate(Fixture candidateFixture,
                         List<Fixture> homeTeamPreviousFixtures,
                         List<Fixture> awayTeamPreviousFixtures)
        {
            this.CandidateFixture = candidateFixture;
            this.HomeTeamPreviousFixtures = homeTeamPreviousFixtures;
            this.AwayTeamPreviousFixtures = awayTeamPreviousFixtures;
        }

        #endregion

        #region Properties

        public Fixture CandidateFixture { get; private set; }

        public List<Fixture> HomeTeamPreviousFixtures { get; private set; }

        public List<Fixture> AwayTeamPreviousFixtures { get; private set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(this.CandidateFixture.ToString());

            stringBuilder.AppendLine("Home team recent results:");
            foreach (Fixture fixture in this.HomeTeamPreviousFixtures)
            {
                stringBuilder.AppendLine($"    {fixture}");
            }

            stringBuilder.AppendLine("Away team recent results:");
            foreach (Fixture fixture in this.AwayTeamPreviousFixtures)
            {
                stringBuilder.AppendLine($"    {fixture}");
            }

            stringBuilder.AppendLine();

            return stringBuilder.ToString();
        }

        #endregion

    }
}
