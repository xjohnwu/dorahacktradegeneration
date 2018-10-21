using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bitfinex.Net.Objects;
using CsvHelper;

namespace TradingStrategy
{
    class Program
    {
        static void Main(string[] args)
        {
            var candles = GetData();

            var strategy = new BTCARMeanReversion();
            Trade trade = null;
            var date = new DateTime(2017, 9, 5, 0, 0, 0, DateTimeKind.Utc);
            var trades = new List<Trade>();
            while (date < DateTime.Today)
            {
                if (strategy.GenerateOpenTradeSignal(candles, date))
                {
                    trade = new Trade
                    {
                        OpenTime = date,
                        OpenPrice = candles.Single(c => c.Timestamp == date).Close
                    };
                    var nextDate = date.AddDays(1);
                    date = new DateTime(nextDate.Year, nextDate.Month, nextDate.Day, 0, 0, 0, DateTimeKind.Utc);
                }
                else if (trade != null && trade.CloseTime == default(DateTime) &&
                         strategy.GenerateCloseTradeSignal(candles, date, trade))
                {
                    trade.CloseTime = date;
                    trade.ClosePrice = candles.Single(c => c.Timestamp == date).Close;
                    trades.Add(trade);
                    trade = null;

                    var nextDate = date.AddDays(1);
                    date = new DateTime(nextDate.Year, nextDate.Month, nextDate.Day, 0, 0, 0, DateTimeKind.Utc);
                }
                else
                    date = date.AddHours(1);
            }

            using (var sw = new StreamWriter(@"C:\Code\GitHub\dorahacktradegeneration\data\BTCUSDARMeanReversion.csv"))
            using (var writer = new CsvWriter(sw))
            {
                writer.WriteRecords(trades);
            }
        }

        public static IList<BitfinexCandle> GetData()
        {
            var list = new List<BitfinexCandle>();
            foreach (var file in Directory.GetFiles(@"C:\Code\GitHub\dorahacktradegeneration\data", "BTCUSD_??????.csv"))
            {
                using (var sr = new StreamReader(file))
                using (var csvReader = new CsvReader(sr))
                {
                    list.AddRange(csvReader.GetRecords<BitfinexCandle>());
                }
            }

            list = list.Distinct(new TimeRelationalComparer()).ToList();
            list.Sort(new TimeRelationalComparer());
            return list;
        }
        private sealed class TimeRelationalComparer : IComparer<BitfinexCandle>, IEqualityComparer<BitfinexCandle>
        {
            public int Compare(BitfinexCandle x, BitfinexCandle y)
            {
                if (ReferenceEquals(x, y)) return 0;
                if (ReferenceEquals(null, y)) return 1;
                if (ReferenceEquals(null, x)) return -1;
                return x.Timestamp.CompareTo(y.Timestamp);
            }

            public bool Equals(BitfinexCandle x, BitfinexCandle y)
            {
                return x.Timestamp.Equals(y.Timestamp);
            }

            public int GetHashCode(BitfinexCandle obj)
            {
                return obj.Timestamp.GetHashCode();
            }
        }
    }
}
