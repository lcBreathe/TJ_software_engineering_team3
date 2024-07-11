import pandas as pd

# 读取CSV文件
df = pd.read_csv(r'C:\Users\17376\Desktop\Earthquake_spirit\地震数据集.csv',encoding='utf-8-sig')

# 定义省份、直辖市、自治区的列表
provinces = ["河北", "山西", "辽宁", "吉林", "黑龙江", "江苏", "浙江", "安徽", "福建", "江西", "山东", "河南", "湖北", "湖南", "广东", "海南", "四川", "贵州", "云南", "陕西", "甘肃", "青海", "台湾"]
autonomous_regions = {"西藏":"自治区","新疆": "维吾尔自治区", "广西": "壮族自治区", "内蒙古": "自治区", "宁夏": "回族自治区"}
municipalities = ["北京", "天津", "上海", "重庆"]
special_administrative_regions = ["香港", "澳门"]

# 定义一个函数来处理区域名称
def append_suffix(region):
    if region in provinces:
        return region + "省"
    elif region in autonomous_regions:
        return region + autonomous_regions[region]
    elif region in municipalities:
        return region + "市"
    elif region in special_administrative_regions:
        return region + "特别行政区"
    else:
        return region


# 应用函数，跳过第一行
df['省份'] = df['省份'].apply(append_suffix)

# 将处理后的数据写回CSV文件
df.to_csv('data_processed.csv', index=False,encoding='utf-8-sig')

