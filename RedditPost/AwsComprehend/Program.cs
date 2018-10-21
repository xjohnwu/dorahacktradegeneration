using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace AwsComprehend
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var awsSentiment = new AwsSentiment();
            using (var sw = new StreamWriter(@"C:\Code\GitHub\dorahacktradegeneration\data\reddit_sentiment_aws.csv"))
            using (var writer = new CsvWriter(sw, new Configuration()))
            {
                writer.WriteHeader<Sentiment>();
                writer.NextRecord();
                var sentiments = awsSentiment.GetFiles().SelectMany(s=>s);
                await awsSentiment.GetSentiment(writer, sentiments);
            }
        }
    }
}
