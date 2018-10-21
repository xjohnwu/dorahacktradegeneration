using System;
using System.Collections.Generic;
using System.Linq;
using Bitfinex.Net.Objects;

namespace TradingStrategy
{
    internal class BTCARMeanReversion
    {
        public bool GenerateOpenTradeSignal(IList<BitfinexCandle> data, DateTime now)
        {
            var indicator1 = new ADTM();
            var indicator2 = new RSI();

            var appliedData = data.TakeWhile(d => d.Timestamp <= now).ToList();

            var adtm = indicator1.Fit(appliedData, 14);
            var rsi = indicator2.Fit(appliedData,  5);

            var tradeSignal = adtm > -0.7m && adtm < -0.4m && rsi < 0.32m;

            return tradeSignal;
        }

        public bool GenerateCloseTradeSignal(IList<BitfinexCandle> data, DateTime now, Trade trade)
        {
            var indicator1 = new ADTM();
            var indicator2 = new RSI();

            var appliedData = data.TakeWhile(d => d.Timestamp <= now).ToList();

            var adtm = indicator1.Fit(appliedData, 14);
            var rsi = indicator2.Fit(appliedData, 5);

            var loss = CalculateLoss(data.Last().Close, trade.OpenPrice);

            if (loss >= 0.03)
            {
                return true;
            }

            return adtm > -0.4m || adtm < -0.7m || rsi > 0.32m;
        }

        public static double CalculateLoss(decimal marketPrice, decimal longSignalPrice)
        {
            return -Math.Log(Convert.ToDouble(marketPrice / longSignalPrice));
        }

        public static double CalculateProfit(decimal marketPrice, decimal longSignalPrice)
        {
            return Math.Log(Convert.ToDouble(marketPrice / longSignalPrice));
        }

    }
}