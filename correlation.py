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
        A.append(float(row[1]))
    return A

def csv_write(file, A): 
    csvfile = open(file, 'w')
    writer = csv.writer(csvfile)
    writer.writerow(A)
    csvfile.close()

def main():
    while True:
        file1 = input("Name of first csv file: ")
        file2 = input("Name of second csv file: ")
        data1 = csv_read(file1)
        data2 = csv_read(file2)
        corr = [data1, data2]
        print("correlation matrix: ", np.corrcoef(corr))
        print("chi-square p-value: ", stats.chi2_contingency(corr)[1])

if __name__ == '__main__':
    main()