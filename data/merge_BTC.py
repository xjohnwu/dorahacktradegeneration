import pandas as pd
import numpy as np

path = 'C:/dorahacktradegeneration/data'

full_data = None

files = ['BTCUSD_201810.csv', 'BTCUSD_201809.csv', 'BTCUSD_201808.csv', 'BTCUSD_201807.csv', 'BTCUSD_201806.csv',
         'BTCUSD_201805.csv', 'BTCUSD_201804.csv', 'BTCUSD_201803.csv', 'BTCUSD_201802.csv', 'BTCUSD_201801.csv',
         'BTCUSD_201712.csv', 'BTCUSD_201711.csv', 'BTCUSD_201710.csv', 'BTCUSD_201709.csv']

full_data = pd.read_csv(path + """/""" + files[0])

for file in files[1:]:
    data = pd.read_csv(path + """/""" + file)
    print(data.head())
    full_data = full_data.append(data, ignore_index=True)
    print(file)

print(full_data.shape)
full_data.to_csv('BTCUSD_all.csv')