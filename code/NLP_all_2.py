from nltk.sentiment.vader import SentimentIntensityAnalyzer
import pandas as pd
import numpy as np

# path = 'C:/dorahacktradegeneration/data'
#
# store = []

# files =  ['bitcoin_201709.csv', 'bitcoin_201710.csv', 'bitcoin_201711.csv', 'bitcoin_201712.csv', 'bitcoin_201801.csv', 'bitcoin_201802.csv',
#           'bitcoin_201803.csv', 'bitcoin_201804.csv', 'bitcoin_201805.csv', 'bitcoin_201806.csv', 'bitcoin_201807.csv',
#           'bitcoin_201808.csv', 'bitcoin_201809.csv', 'bitcoin_201810.csv']
#
# analyzer = SentimentIntensityAnalyzer(lexicon_file='vader_crypto.txt')
#
# for file in files:
#     data = pd.read_csv(path + """/""" + file)
#     sentences = data['title'].tolist()
#     timestamp = data['CreatedUtc'].tolist()
#     for i in np.arange(len(sentences)):
#         vs = analyzer.polarity_scores(sentences[i])
#         vals = list(vs.values())
#
#         store.append([sentences[i], vals[0], vals[1], vals[2], vals[3], timestamp[i]])
#
# out = pd.DataFrame(store)

# print(out.head())
# out = out.rename({0: 'Title', 1: 'Negative', 2: 'Neutral', 3: 'Positive', 4: 'Compound', 5: 'DateTime'})
#
# print(out.head())

# out['IndicatorPositive'] = np.where(out.iloc[:, 4] > 0.2, 1, 0)
# out['IndicatorNegative'] = np.where(out.iloc[:, 4] < -0.2, -1, 0)
#
# out['Indicator'] = out['IndicatorPositive'] + out['IndicatorNegative']
#
# out.to_csv('reddit_sentiment_clean_3.csv')

data = pd.read_csv('reddit_sentiment_clean_3.csv')

timeline = data.iloc[:, 6].tolist()

dates = []

for time in timeline:
    print(time[0:10])
    dates.append(str(time[0:10]))

data['Date'] = dates

calendar = np.unique(dates)

pos_store = []
neg_store = []
num_store = []

for d in calendar:
    sub = data.loc[data['Date'] == str(d), :]

    num_store.append(len(sub['Date']))

    positive_count = np.sum(sub['IndicatorPositive'])
    negative_count = -np.sum(sub['IndicatorNegative'])

    positive_fraction = positive_count/(positive_count + negative_count)
    print(positive_fraction)
    pos_store.append(positive_fraction)

    negative_fraction = negative_count/(positive_count + negative_count)
    print(negative_fraction)
    neg_store.append(negative_fraction)

out = pd.DataFrame({'Date': calendar, 'Positive': pos_store, 'Negative': neg_store, 'Count': num_store })



out.to_csv('ratios.csv')