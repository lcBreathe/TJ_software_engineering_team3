# -*- coding: utf-8 -*-
"""
Created on Sat Jun 15 20:07:35 2024

@author: nyr
"""


import json
import pandas as pd
from pyecharts.charts import Map, Page
from pyecharts import options as opts



import cpca

# 设置列对齐
pd.set_option('display.unicode.ambiguous_as_wide', True)
pd.set_option('display.unicode.east_asian_width', True)
# 打开文件

df = pd.read_excel(r'C:\Users\nyr\Desktop\eqList2024_06_15.xlsx')
col = df["参考位置"]
ans = cpca.transform(col)

data = df[df['省'].notna()]
data.to_excel('地名.xlsx')