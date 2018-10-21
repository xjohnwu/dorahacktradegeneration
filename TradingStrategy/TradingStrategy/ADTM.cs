using System;
using System.Collections.Generic;
using System.Linq;
using Bitfinex.Net.Objects;

namespace TradingStrategy
{   
    class ADTM
    {
        public decimal Fit(IList<BitfinexCandle> series, int lookbackLength)
        {
            var input = series.Skip(series.Count - lookbackLength - 1).ToList();

            var dtm = Dtm(input);
            var dbm = Dbm(input);

            var stm = dtm.Sum();
            var sbm = dbm.Sum();

            if (Math.Max(stm, sbm) == 0)
            {
                return 1;
            }
            else
            {
                return (stm - sbm) / Math.Max(stm, sbm);
            }
        }

        private decimal[] Dtm(IList<BitfinexCandle> series)
        {
            var x = series;
            decimal[] dtm = new decimal[x.Count - 1];

            for (int i = 1; i < x.Count; i++)
            {
                if (x[i].Open <= x[i - 1].Open)
                {
                    dtm[i - 1] = 0;
                }
                else
                {
                    dtm[i - 1] = Math.Max(x[i].High - x[i].Open, x[i].Open - x[i - 1].Open);
                }
            }

            return dtm;
        }

        private decimal[] Dbm(IList<BitfinexCandle> series)
        {
            var x = series;
            decimal[] dbm = new decimal[x.Count - 1];

            for (int i = 1; i < x.Count; i++)
            {
                if (x[i].Open >= x[i - 1].Open)
                {
                    dbm[i - 1] = 0;
                }
                else
                {
                    dbm[i - 1] = x[i].Open - x[i].Low;
                }
            }

            return dbm;
        }
    }
}
