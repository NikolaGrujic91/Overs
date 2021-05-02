using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;

namespace DataCollector
{
    public class Collector
    {
        #region Constructors

        public Collector()
        {
            this.Fixtures = new List<Fixture>();
            this.Results = new Dictionary<string, List<Fixture>>();
        }

        #endregion

        #region Properties

        public List<Fixture> Fixtures { get; private set; }

        public Dictionary<string, List<Fixture>> Results { get; private set; }

        #endregion

        #region Methods

        public void Collect()
        {
            MemoryStream memoryStream = this.Download(Links.Fixtures);
            this.StoreFixtures(memoryStream);

            memoryStream = this.Download(Links.England1);
            this.StoreResults(memoryStream);

            memoryStream = this.Download(Links.England2);
            this.StoreResults(memoryStream);

            memoryStream = this.Download(Links.Scotland1);
            this.StoreResults(memoryStream);

            memoryStream = this.Download(Links.Germany1);
            this.StoreResults(memoryStream);

            memoryStream = this.Download(Links.Germany2);
            this.StoreResults(memoryStream);

            memoryStream = this.Download(Links.Italy1);
            this.StoreResults(memoryStream);

            memoryStream = this.Download(Links.Italy2);
            this.StoreResults(memoryStream);

            memoryStream = this.Download(Links.Spain1);
            this.StoreResults(memoryStream);

            memoryStream = this.Download(Links.France1);
            this.StoreResults(memoryStream);

            memoryStream = this.Download(Links.France2);
            this.StoreResults(memoryStream);

            memoryStream = this.Download(Links.Netherlands1);
            this.StoreResults(memoryStream);

            memoryStream = this.Download(Links.Belgium1);
            this.StoreResults(memoryStream);

            memoryStream = this.Download(Links.Portugal1);
            this.StoreResults(memoryStream);

            memoryStream = this.Download(Links.Turkey1);
            this.StoreResults(memoryStream);

            memoryStream = this.Download(Links.Greece1);
            this.StoreResults(memoryStream);
        }

        private MemoryStream Download(string link)
        {
            var webClient = new WebClient();
            return new MemoryStream(webClient.DownloadData(link));
        }

        private void StoreFixtures(MemoryStream memoryStream)
        {
            var streamReader = new StreamReader(memoryStream);

            string headerLine = streamReader.ReadLine();
            string[] headerValues = headerLine?.Split(',');

            int dateIndex     = this.FindColumnIndex(headerValues, Constants.DateColumnName);
            int homeTeamIndex = this.FindColumnIndex(headerValues, Constants.HomeTeamColumnName);
            int awayTeamIndex = this.FindColumnIndex(headerValues, Constants.AwayTeamColumnName);
            int leagueIndex   = this.FindColumnIndex(headerValues, Constants.LeagueName);
            int overOddIndex  = this.FindColumnIndex(headerValues, Constants.OverOddColumnName);

            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();

                if (line != null)
                {
                    string[] values = line.Split(',');

                    DateTime date = this.CreateDate(values[dateIndex]);

                    var fixture = new Fixture(values[homeTeamIndex],
                                              values[awayTeamIndex],
                                              date,
                                              values[leagueIndex],
                                              double.Parse(values[overOddIndex], CultureInfo.InvariantCulture));

                    this.Fixtures.Add(fixture);
                }
            }
        }

        private void StoreResults(MemoryStream memoryStream)
        {
            var streamReader = new StreamReader(memoryStream);

            string headerLine = streamReader.ReadLine();
            string[] headerValues = headerLine?.Split(',');

            int dateIndex      = this.FindColumnIndex(headerValues, Constants.DateColumnName);
            int homeTeamIndex  = this.FindColumnIndex(headerValues, Constants.HomeTeamColumnName);
            int awayTeamIndex  = this.FindColumnIndex(headerValues, Constants.AwayTeamColumnName);
            int homeScoreIndex = this.FindColumnIndex(headerValues, Constants.HomeScoreColumnName);
            int awayScoreIndex = this.FindColumnIndex(headerValues, Constants.AwayScoreColumnName);
            int leagueIndex    = this.FindColumnIndex(headerValues, Constants.LeagueName);
            int overOddIndex   = this.FindColumnIndex(headerValues, Constants.OverOddColumnName);

            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();

                if (line != null)
                {
                    string[] values = line.Split(',');

                    DateTime date = this.CreateDate(values[dateIndex]);
                    string league = values[leagueIndex];

                    double overOdd = overOdd = values[overOddIndex] == string.Empty ?
                                               0.0 : 
                                               double.Parse(values[overOddIndex], CultureInfo.InvariantCulture);

                    var fixture = new Fixture(values[homeTeamIndex],
                                              values[awayTeamIndex],
                                              Convert.ToInt32(values[homeScoreIndex]),
                                              Convert.ToInt32(values[awayScoreIndex]),
                                              date,
                                              league,
                                              overOdd);

                    if (!this.Results.ContainsKey(league))
                    {
                        this.Results.Add(league, new List<Fixture>());
                    }

                    this.Results[league].Add(fixture);
                }
            }
        }

        private int FindColumnIndex(string[] values, string columnName)
        {
            int count = values.Length;

            for (int i = 0; i < count; i++)
            {
                if (values[i] == columnName)
                {
                    return i;
                }
            }

            return -1;
        }

        private DateTime CreateDate(string value)
        {
            const int dayIndex = 0;
            const int monthIndex = 1;
            const int yearIndex = 2;

            string[] values = value.Split('/');

            return new DateTime(Convert.ToInt32(values[yearIndex]), 
                                Convert.ToInt32(values[monthIndex]), 
                                Convert.ToInt32(values[dayIndex]));
        }

        #endregion

    }
}
