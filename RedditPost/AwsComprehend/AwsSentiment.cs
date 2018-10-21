using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using CsvHelper;
using RedditPost;

namespace AwsComprehend
{
    public class AwsSentiment
    {
        public IEnumerable<IEnumerable<Submission>> GetFiles()
        {
            foreach (var file in Directory.GetFiles(@"C:\Code\GitHub\dorahacktradegeneration\data", "bitcoin*.csv"))
            {
                using (var sr = new StreamReader(file))
                using (var reader = new CsvReader(sr))
                {
                    yield return reader.GetRecords<Submission>();
                }
            }
        }

        public async Task GetSentiment(CsvWriter writer, IEnumerable<Submission> sentiments)
        {
            var comprehendClient = new AmazonComprehendClient(Amazon.RegionEndpoint.EUWest1);

            foreach (var submission in sentiments)
            {
                // Call DetectKeyPhrases API
                var detectSentimentRequest = new DetectSentimentRequest
                {
                    Text = submission.title,
                    LanguageCode = "en"
                };
                var detectSentimentResponse = await comprehendClient.DetectSentimentAsync(detectSentimentRequest);
                var score = detectSentimentResponse.SentimentScore;
                var sentiment = new Sentiment
                {
                    time = submission.CreatedUtc,
                    title = submission.title,
                    positive = score.Positive,
                    neutral = score.Neutral,
                    negative = score.Negative,
                    mixed = score.Mixed,
                };
                Console.WriteLine($"Processing {submission.CreatedUtc} {submission.title}");
                Console.WriteLine($"Score +ve {sentiment.positive} -- {sentiment.neutral} -ve {sentiment.negative} mixed {sentiment.mixed}");
                writer.WriteRecord(sentiment);
                writer.NextRecord();
                writer.Flush();
            }
        }
    }
}