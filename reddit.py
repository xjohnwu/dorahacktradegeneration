import praw

reddit = praw.Reddit(client_id='OePcJdcciT2aeA',
                     client_secret='ul07nU1AZydnS9UXSUOq4pekXtA',
                     user_agent='')

subreddit = reddit.subreddit("bitcoin")

# assume you have a Subreddit instance bound to variable `subreddit`
for submission in subreddit.hot(limit=10):
    print(submission.title)  # Output: the submission's title
    print(submission.score)  # Output: the submission's score
    print(submission.id)     # Output: the submission's ID
    print(submission.url)    # Output: the URL the submission points to
                             # or the submission's URL if it's a self post
