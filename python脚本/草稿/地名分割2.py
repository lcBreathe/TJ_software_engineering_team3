import pandas as pd
import cpca

# 打开文件
df = pd.read_csv(r'C:\Users\17376\Desktop\近十年中国地震数据.csv')

# 提取参考位置列
col = df["参考位置"]

# 使用cpca转换地名信息
ans = cpca.transform(col)

# 将转换后的数据添加到原始数据框中
df[['省', '市', '区']] = ans[['省', '市', '区']]

# 处理特殊情况：
df.loc[(df['省'] == '台湾省') & (df['市'].isna()), '市'] = '台湾省'
df.loc[(df['省'] == '四川省') & (df['市'].isna()) & (df['参考位置'].str.contains('凉山州')), '市'] = '凉山州'
df.loc[(df['省'] == '青海省') & (df['市'].isna()) & (df['参考位置'].str.contains('海西州')), '市'] = '海西州'
df.loc[(df['省'] == '青海省') & (df['市'].isna()) & (df['参考位置'].str.contains('海北州')), '市'] = '海北州'
df.loc[(df['省'] == '新疆维吾尔自治区') & (df['市'].isna()) & (df['参考位置'].str.contains('新星市')), '市'] = '新星市'
df.loc[df['参考位置'].str.contains('重庆'), '市'] = '重庆市'

# 过滤出省信息不为空的数据
data = df[df['省'].notna()]
data = df[df['市'].notna()]

# 将处理后的数据保存到新的Excel文件
data.to_excel('地名.xlsx')
