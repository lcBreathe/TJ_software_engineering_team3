import requests
import csv
from bs4 import BeautifulSoup
import pandas as pd
from requests import RequestException
import os

provinces = ["河北", "山西", "辽宁", "吉林", "黑龙江", "江苏", "浙江", "安徽", "福建", "江西", "山东", "河南", "湖北", "湖南", "广东", "海南", "四川", "贵州", "云南", "陕西", "甘肃", "青海", "台湾"]
autonomous_regions = {"西藏":"自治区","新疆": "维吾尔自治区", "广西": "壮族自治区", "内蒙古": "自治区", "宁夏": "回族自治区"}
municipalities = ["北京", "天津", "上海", "重庆"]
special_administrative_regions = ["香港", "澳门"]

def get_one_page(page):
    try:
        url = "http://www.ceic.ac.cn/speedsearch?time=6&&page=%s" % (str(page))
        head = {
            'User-Agent': "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/17.17134"}
        data = requests.get(url, headers=head)
        return data
    except RequestException:
        print('爬取失败')

def parse_one_page(html, ulist):
    soup = BeautifulSoup(html.text, 'lxml')
    trs = soup.find_all('tr')
    i = 0
    for tr in trs:
        if i == 0:
            i += 1
        else:
            ui = []
            for td in tr:
                ui.append(td.string)
            ulist.append(ui)
            i = i + 1

def save_contents(ulist):
    with open(r'最近一年全球地震情况.csv', 'w', encoding='utf-8-sig', newline='') as f:
        csv.writer(f).writerow(['发震时刻', '震级', '纬度(°)', '经度(°)', '深度（千米）', '参考位置'])
        for i in range(len(ulist)):
            csv.writer(f).writerow([ulist[i][3], ulist[i][1], ulist[i][5], ulist[i][7], ulist[i][9], ulist[i][11]])

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
    pass

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

def main(page):
    ulist = []
    for i in range(page):
        html = get_one_page(i + 1)
        parse_one_page(html, ulist)
    save_contents(ulist)

    # 读取CSV文件
    df = pd.read_csv('最近一年全球地震情况.csv', encoding='utf-8-sig')

    # 应用函数并创建新列
    df['省份'] = df['参考位置'].apply(get_province)
    df['省份'] = df['省份'].apply(append_suffix)

    # 保存结果到新的CSV文件
    df.to_csv('近一年全球地震情况汇总.csv', index=False, encoding='utf-8-sig')
    os.remove('最近一年全球地震情况.csv')

if __name__ == '__main__':
    main(62)  # 需爬取的页数
