import pandas as pd
import numpy as np

path = 'C:/dorahacktradegeneration/data'

full_data = None

files =  ['bitcoin_201709.csv', 'bitcoin_201710.csv', 'bitcoin_201711.csv', 'bitcoin_201801.csv', 'bitcoin_201802.csv',
          'bitcoin_201803.csv', 'bitcoin_201804.csv', 'bitcoin_201805.csv', 'bitcoin_201806.csv', 'bitcoin_201807.csv',
          'bitcoin_201808.csv', 'bitcoin_201809.csv', 'bitcoin_201810.csv']

for file in files:
    data = pd.read_csv(path + """/""" + file)
    if full_data is None:
        full_data = data
    else:
        full_data.append(data, ignore_index=True)

full_data.to_csv('reddit_all.csv')
