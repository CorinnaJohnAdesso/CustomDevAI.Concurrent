from sklearn import tree
from sklearn.model_selection import train_test_split
import pandas as pd
import pickle

data = pd.read_csv('it_jobs_automl_dataset.csv')

X = data.drop('got_job', axis=1)
X = X.drop('name', axis=1)
y = data['got_job']

X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.1)

classifier = tree.DecisionTreeClassifier()
classifier = classifier.fit(X, y)

with open('model.pkl', 'wb') as handle:
    pickle.dump(classifier, handle, protocol=pickle.HIGHEST_PROTOCOL)
