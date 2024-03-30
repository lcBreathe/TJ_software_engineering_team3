# -*- coding: utf-8 -*-
"""
Created on Sat Mar 30 11:28:16 2024

@author: nyr
"""

import pandas as pd

df = pd.read_excel('.\抗震设防烈度.xlsx')
cols, rows = df.shape

for i in range(1, cols):
    data = df.iloc[i].values
    s = "insert into kangzhen values(\'" + str(data[0]) + "\',\'" + str(data[1]) + "\',\'" + str(data[2]) + "\',\'" + str(data[3]) + "\',\'" + str(data[4]) + "\')"
    excel = open("./kz_sql.csv", "a").write(s+"\n")
    
    
