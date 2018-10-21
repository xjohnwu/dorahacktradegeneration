using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bitfinex.Net;
using Bitfinex.Net.Objects;
using CsvHelper;

namespace TradingStrategy
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new BitfinexClient();
            var candles = client.GetCandles(TimeFrame.OneDay, "tBTCUSD", startTime: new DateTime(2017, 8, 1),
                endTime: new DateTime(2018, 10, 21), sorting: Sorting.OldFirst, limit: 1000).Data;

            var strategy = new BTCARMeanReversion();
            Trade trade = null;
            var date = new DateTime(2017, 9, 1);
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
                }
                else if (trade != null && trade.CloseTime == default(DateTime) &&
                         strategy.GenerateCloseTradeSignal(candles, date))
                {
                    trade.CloseTime = date;
                    trade.ClosePrice = candles.Single(c => c.Timestamp == date).Close;
                    trades.Add(trade);
                    trade = null;
                }

                date = date.AddDays(1);
            }

            using (var sw = new StreamWriter(@"C:\Code\GitHub\dorahacktradegeneration\data\BTCUSDARMeanReversion.csv"))
            using (var writer = new CsvWriter(sw))
            {
                writer.WriteRecords(trades);
            }
        }

        public class Trade
        {
            public DateTime OpenTime { get; set; }
            public decimal OpenPrice { get; set; }
            public DateTime CloseTime { get; set; }
            public decimal ClosePrice { get; set; }
        }
    }
}
