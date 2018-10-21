using System;

namespace RedditPost
{
    public class Submission
    {
        public string title { get; set; }
        public string selftext { get; set; }
        public long created_utc { get; set; }

        public DateTime CreatedUtc => FromUnixTime(created_utc);

        public string url { get; set; }
        public int score { get; set; }

        public static DateTime FromUnixTime(long unixTime)
        {
            return epoch.AddSeconds(unixTime);
        }
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ToUnixTime(DateTime time)
        {
            return Convert.ToInt64(time.ToUniversalTime().Subtract(epoch).TotalSeconds);
        }
    }
}