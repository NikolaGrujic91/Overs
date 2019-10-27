using System;

namespace DataCollector
{
    public class Fixture
    {
        #region Constructors

        public Fixture(string homeTeam, string awayTeam, int homeScore, int awayScore, DateTime date, string league, double overOdd)
        {
            this.HomeTeam = homeTeam;
            this.AwayTeam = awayTeam;
            this.HomeScore = homeScore;
            this.AwayScore = awayScore;
            this.Date = date;
            this.League = league;
            this.OverOdd = overOdd;
            this.Finished = true;
        }

        public Fixture(string homeTeam, string awayTeam, DateTime date, string league, double overOdd)
        {
            this.HomeTeam = homeTeam;
            this.AwayTeam = awayTeam;
            this.Date = date;
            this.League = league;
            this.OverOdd = overOdd;
            this.Finished = false;
        }

        #endregion

        #region Properties

        public string HomeTeam { get; private set; }

        public string AwayTeam { get; private set; }

        public int HomeScore { get; private set; }

        public int AwayScore { get; private set; }

        public DateTime Date { get; private set; }

        public double OverOdd { get; private set; }

        public bool Finished { get; private set; }

        public string League { get; private set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return this.Finished ? 
                   $"{this.League} {this.Date.ToShortDateString()} {this.HomeTeam} - {this.AwayTeam} {this.HomeScore} : {this.AwayScore} Over odd: {this.OverOdd}" :
                   $"{this.League} {this.Date.ToShortDateString()} {this.HomeTeam} - {this.AwayTeam} Over odd: {this.OverOdd}";
        }

        #endregion
    }
}
