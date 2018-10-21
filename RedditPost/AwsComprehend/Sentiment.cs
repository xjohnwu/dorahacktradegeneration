using System;

namespace AwsComprehend
{
    public class Sentiment
    {
        public DateTime time { get; set; }
        public string title { get; set; }
        public float negative { get; set; }
        public float neutral { get; set; }
        public float positive { get; set; }
        public float mixed { get; set; }
    }
}