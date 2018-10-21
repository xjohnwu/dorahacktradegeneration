import numpy as np
import matplotlib.pyplot as plt
import multiprocessing as mp
import datetime as dt
import matplotlib.dates as mdates
from scipy import stats
import pandas as pd
import requests
import json
import math
import csv

def csv_read(file):
    csvfile = open(file, 'r')
    reader = csv.reader(csvfile)
    A = []
    for row in reader:
        try:
            A.append(float(row[2]))
        except:
            pass
    return A

def csv_read2(file):
    csvfile = open(file, 'r')
    reader = csv.reader(csvfile)
    A = []
    for row in reader:
        try:
            A.append(float(row[5]))
        except:
            pass
    return A

def csv_write(file, A): 
    csvfile = open(file, 'w')
    writer = csv.writer(csvfile)
    writer.writerow(A)
    csvfile.close()

def main():
    price = csv_read('BTCUSD_daily.csv')
    ratio = csv_read('ratios.csv')
    aws_ratio = csv_read('ratios_aws.csv')
    write_array_diff = []
    write_array_pm = []
    for i in range(len(price)-1):
        diff = price[i+1] / price[i]
        write_array_diff.append(diff)
        if diff > 1:
            write_array_pm.append(1)
        elif diff < 1:
            write_array_pm.append(0)
        else:
            write_array_pm.append(0)
    aws_ratio.pop(-1)
    aws_length = len(aws_ratio)
    csv_write('price_diff.csv', write_array_diff)
    csv_write('price_pm.csv', write_array_pm)
    corr = [ratio, write_array_diff]
    corr2 = [aws_ratio, write_array_diff[:aws_length]]
    print(len(ratio), len(write_array_diff))
    print("correlation matrix: ", np.corrcoef(corr), np.corrcoef(corr2))
    print("chi-square p-value: ", stats.chi2_contingency(corr)[1], stats.chi2_contingency(corr2)[1])
    
    
if __name__ == '__main__':
    main()