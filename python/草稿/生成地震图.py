# -*- coding: utf-8 -*-
"""
Created on Sat Jun 15 17:06:24 2024

@author: nyr
"""

import json
import pandas as pd
from pyecharts.charts import Map, Page
from pyecharts import options as opts




# 设置列对齐
pd.set_option('display.unicode.ambiguous_as_wide', True)
pd.set_option('display.unicode.east_asian_width', True)
# 打开文件
df = pd.read_csv(r'C:\Users\17376\Desktop\pythonProject1\处理后的数据.csv')
# 对省份进行统计
data2 = df['省份全称']
element_counts = data2.value_counts()
element_max = df.groupby(['省份全称'])['震级'].max()
data2_list = list(element_counts.index)
data3_list = list(element_counts)
data4_list = list(element_max.index)
data5_list = list(element_max)
a = (
    Map()
        .add("地震发生次数", [list(z) for z in zip(data2_list, data3_list)], "china")
        .set_global_opts(
        title_opts=opts.TitleOpts(),
        visualmap_opts=opts.VisualMapOpts(max_=500),
    )
)

b = (
    Map()
        .add("全省最大震级", [list(z) for z in zip(data4_list, data5_list)], "china")
        .set_global_opts(
        title_opts=opts.TitleOpts(),
        visualmap_opts=opts.VisualMapOpts(min_= 3, max_= 8),
    )
)


page1 = Page(layout=Page.DraggablePageLayout)
page1.add(
    a,
)
# 生成render.html文件
page1.render("1.html")
page2 = Page(layout=Page.DraggablePageLayout)
page2.add(
    b,
)
# 生成render.html文件
page2.render("2.html")

































































