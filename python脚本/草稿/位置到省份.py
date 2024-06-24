import pandas as pd

# 读取CSV文件
df = pd.read_excel('地震数据集.xlsx')

# 定义一个函数来判断省份
def get_province(location):
    if '新疆' in location:
        return '新疆'
    elif '四川' in location:
        return '四川'
    elif '西藏' in location:
        return '西藏'
    elif '云南' in location:
        return '云南'
    elif '台湾' in location:
        return '台湾'
    elif '海南' in location:
        return '海南'
    elif '北京' in location:
        return '北京'
    elif '上海' in location:
        return '上海'
    elif '广东' in location:
        return '广东'
    elif '江苏' in location:
        return '江苏'
    elif '浙江' in location:
        return '浙江'
    elif '福建' in location:
        return '福建'
    elif '湖南' in location:
        return '湖南'
    elif '湖北' in location:
        return '湖北'
    elif '安徽' in location:
        return '安徽'
    elif '河南' in location:
        return '河南'
    elif '河北' in location:
        return '河北'
    elif '山东' in location:
        return '山东'
    elif '江西' in location:
        return '江西'
    elif '陕西' in location:
        return '陕西'
    elif '山西' in location:
        return '山西'
    elif '黑龙江' in location:
        return '黑龙江'
    elif '吉林' in location:
        return '吉林'
    elif '辽宁' in location:
        return '辽宁'
    elif '内蒙古' in location:
        return '内蒙古'
    elif '宁夏' in location:
        return '宁夏'
    elif '青海' in location:
        return '青海'
    elif '甘肃' in location:
        return '甘肃'
    elif '四川' in location:
        return '四川'
    # 如果不是国内，则返回"非国内"
    return '非国内'

# 应用函数并创建新列
df['省份缩写'] = df['参考位置'].apply(get_province)
df = df[(df['震级'] >= 5) & (df['震级'] <= 10)]
# 保存结果到新的CSV文件
df.to_csv('output_file.csv', index=False,encoding='utf-8-sig')
