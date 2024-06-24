import pandas as pd
import re

# 读取CSV文件
df = pd.read_csv(r'output_file.csv',encoding='utf-8-sig')

# 定义要删除的后缀
suffixes = ['省', '市', '地区', '自治州']

# 删除“市”列中末尾的特定后缀
def remove_suffix(city_name):
    for suffix in suffixes:
        if city_name.endswith(suffix):
            return city_name[:-len(suffix)]
    return city_name

# 应用函数到“市”列
df['市'] = df['市'].apply(remove_suffix)

# 将结果保存到新的CSV文件
df.to_csv('处理后的数据.csv', index=False,encoding='utf-8-sig')

