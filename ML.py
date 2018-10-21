import pandas as pd
from sklearn.linear_model import LinearRegression
from sklearn.ensemble import GradientBoostingClassifier
from sklearn.tree import DecisionTreeClassifier
from sklearn.metrics import r2_score
import numpy as np


from sklearn.metrics import confusion_matrix
from sklearn.metrics import recall_score
from sklearn.metrics import precision_score

# X = pd.read_csv('ratios.csv')
# X = X.loc[:, ['Positive', 'Negative', 'Count']]
#
#
# Y = np.loadtxt('price_diff.csv', dtype='float', delimiter=',')
# Y = np.reshape(Y, (415, 1))

#
# sampl = 100
# X_train = X[:-sampl]
# y_train = Y[:-sampl]
#
# X_test = X[-sampl:]
# y_test = Y[-sampl:]
#
# model = LinearRegression()
# model = model.fit(X_train, y_train)
# y_train_pred = model.predict(X_train)
# y_test_pred = model.predict(X_test)
#
# y_train_pred = np.reshape(y_train_pred, (len(y_train_pred),))
# y_test_pred = np.reshape(y_test_pred, (len(y_test_pred),))
#
#
# print(r2_score(y_train, y_train_pred))
#
# print(r2_score(y_test, y_test_pred))


# #model = LinearRegression()
# model = GradientBoostingRegressor()
# model = model.fit(X, Y)
# Y_pred = model.predict(X)
#
# print(r2_score(Y, Y_pred))


data = pd.read_csv('BTCUSDARMeanReversion.csv')


Y_num = data['Position']
Y = np.where(Y_num < 1, 1, 0)

print(Y)

X = data.drop(['Position', 'OpenTime', 'CloseTime'], axis=1)


sampl = 30
X_train = X[:-sampl]
y_train = Y[:-sampl]

X_test = X[-sampl:]
y_test = Y[-sampl:]

#model = DecisionTreeClassifier()
model = GradientBoostingClassifier()
model = model.fit(X_train, y_train)
y_train_pred = model.predict(X_train)
y_test_pred = model.predict(X_test)

y_train_pred = np.reshape(y_train_pred, (len(y_train_pred),))
y_test_pred = np.reshape(y_test_pred, (len(y_test_pred),))


# print(r2_score(y_train, y_train_pred))
#
# print(r2_score(y_test, y_test_pred))


print("TRAIN")
print(confusion_matrix(y_train, y_train_pred))
print(precision_score(y_train, y_train_pred))
print(recall_score(y_train, y_train_pred))

print("TEST")
print(confusion_matrix(y_test, y_test_pred))
print(precision_score(y_test, y_test_pred))
print(recall_score(y_test, y_test_pred))

print(y_test, y_test_pred, Y_num[-sampl:])



data['Prediction'] = model.predict(X)

data.to_csv('prediction.csv')
