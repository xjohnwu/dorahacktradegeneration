using System.Collections.Generic;
using System.Linq;
using Bitfinex.Net.Objects;

namespace TradingStrategy
{
    class RSI
    {
        public decimal Fit(IList<BitfinexCandle> series, int lookbackLength)
        {
            var x = series.Skip(series.Count - lookbackLength - 1).ToList();

            decimal[] delta = new decimal[x.Count - 1];

            for (int i = 0; i < x.Count - 1; i++)
            {
                delta[i] = x[i + 1].Open - x[i].Open;
            }

            decimal[] upval = new decimal[delta.Length];
            decimal[] downval = new decimal[delta.Length];

            for (int i = 0; i < delta.Length; i++)
            {
                if (delta[i] > 0)
                {
                    upval[i] = delta[i];
                    downval[i] = 0;

                }
                else
                {
                    upval[i] = 0;
                    downval[i] = -delta[i];
                }
            }

            var up = upval.Average();
            var down = downval.Average();

            if (up + down == 0)
            {
                return 1;
            }
            else
            {
                return up / (up + down);
            }
        }
    }
}