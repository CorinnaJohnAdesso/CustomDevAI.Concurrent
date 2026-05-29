import pandas as pd
import pickle

with open('model.pkl', 'rb') as handle:
    restored_classifier = pickle.load(handle)

unknown_rows = pd.read_csv('unknown.csv')
unknown_rows = unknown_rows.drop('name', axis=1)

p = restored_classifier.predict(unknown_rows)
print(p)
