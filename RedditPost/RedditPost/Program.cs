using System;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;

namespace RedditPost
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RedditClient();

            var startMonth = new DateTime(2017, 9, 1, 0, 0, 0, DateTimeKind.Utc);
            var endMonth = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var start = startMonth;
            while(start < endMonth)
            {
                var dataData = client.Get(start, start.AddMonths(1).AddSeconds(-1));
                using (var sw =
                    new StreamWriter($@"C:\Code\GitHub\dorahacktradegeneration\data\bitcoin_{start:yyyyMM}.csv"))
                using (var csvWriter = new CsvWriter(sw))
                {
                    csvWriter.WriteRecords(dataData);
                }

                start = start.AddMonths(1);
            }
        }
    }
}
