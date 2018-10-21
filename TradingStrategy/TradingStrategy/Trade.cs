using System;

namespace TradingStrategy
{
    internal class Trade
    {
        public DateTime OpenTime { get; set; }
        public decimal OpenPrice { get; set; }
        public DateTime CloseTime { get; set; }
        public decimal ClosePrice { get; set; }
    }
}