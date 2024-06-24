# -*- coding: utf-8 -*-
"""
Created on Sat Jun 15 16:21:41 2024

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
data = df[df['省'].notna()]
data.to_excel('data.xlsx')

