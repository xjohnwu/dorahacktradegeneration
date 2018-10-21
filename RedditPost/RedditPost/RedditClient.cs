using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace RedditPost
{
    public class RedditClient
    {
        private readonly RestClient _restClient;

        public RedditClient()
        {
            _restClient = new RestClient("https://api.pushshift.io/reddit/search/submission");
        }

        public IEnumerable<Submission> DoGet(DateTime start, DateTime stop)
        {
            var request = new RestRequest(Method.GET);
            request.Parameters.Add(new Parameter("subreddit", "bitcoin", ParameterType.QueryString));
            request.Parameters.Add(new Parameter("size", 250, ParameterType.QueryString));
            request.Parameters.Add(new Parameter("before", Submission.ToUnixTime(stop), ParameterType.QueryString));
            request.Parameters.Add(new Parameter("after", Submission.ToUnixTime(start), ParameterType.QueryString));
            while (true)
            {
                var response = _restClient.Execute<RedditSubmissionData>(request);
                if (response.Data != null)
                {
                    return response.Data.data;
                }
            }
        }

        public IEnumerable<Submission> Get(DateTime before, DateTime after)
        {
            List<Submission> data = new List<Submission>();
            List<Submission> lastData = new List<Submission>();

            while(true)
            {
                lastData = DoGet(before, after).ToList();
                data.AddRange(lastData);
                if (!lastData.Any())
                    return data;
                before = lastData.Max(d => d.CreatedUtc).AddMinutes(1);
            }
        }
    }
}