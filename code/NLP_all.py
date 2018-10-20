from nltk.sentiment.vader import SentimentIntensityAnalyzer
import pandas as pd
import numpy as np

path = 'C:/dorahacktradegeneration/data'

store = []

files =  ['bitcoin_201709.csv', 'bitcoin_201710.csv', 'bitcoin_201711.csv', 'bitcoin_201801.csv', 'bitcoin_201802.csv',
          'bitcoin_201803.csv', 'bitcoin_201804.csv', 'bitcoin_201805.csv', 'bitcoin_201806.csv', 'bitcoin_201807.csv',
          'bitcoin_201808.csv', 'bitcoin_201809.csv', 'bitcoin_201810.csv']

analyzer = SentimentIntensityAnalyzer(lexicon_file='vader_crypto.txt')

for file in files:
    data = pd.read_csv(path + """/""" + file)
    sentences = data['title'].tolist()
    timestamp = data['CreatedUtc'].tolist()
    for i in np.arange(len(sentences)):
        vs = analyzer.polarity_scores(sentences[i])
        store.append([sentences[i], str(vs), timestamp[i]])

pd.DataFrame(store).to_csv('reddit_sentiment_clean.csv')