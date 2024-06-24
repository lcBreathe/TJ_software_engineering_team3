import requests
import csv
from bs4 import BeautifulSoup
from lxml import etree

# print(soup)
from requests import RequestException


def get_one_page(page):
    try:
        url = "http://www.ceic.ac.cn/speedsearch?time=6&&page=%s" % (str(page))
        head = {
            'User-Agent': "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/17.17134"}
        data = requests.get(url, headers=head)
        # print(data)
        return data
    except RequestException:
        print('爬取失败')


def parse_one_page(html, ulist):
    # 创建bs对象 bs是使用的python默认的解析器，lxml也是解析器
    soup = BeautifulSoup(html.text, 'lxml')
    # find_all():可以根据标签名、属性、内容查找文档
    trs = soup.find_all('tr')
    i = 0
    for tr in trs:
        # 去掉第一个tr
        if i == 0:
            i += 1
        else:
            ui = []
            for td in tr:
                # print(td.string)
                ui.append(td.string)
            ulist.append(ui)
            i = i + 1


def save_contents(ulist):
    # print(ulist)
    with open(r'最近一年全球地震情况.csv', 'w', encoding='utf-8-sig', newline='') as f:
        csv.writer(f).writerow(['发震时刻', '震级', '纬度(°)', '经度(°)', '深度（千米）', '参考位置'])
        for i in range(len(ulist)):
            csv.writer(f).writerow([ulist[i][3], ulist[i][1], ulist[i][5], ulist[i][7], ulist[i][9], ulist[i][11]])


def main(page):
    ulist = []
    for i in range(page):
        html = get_one_page(i + 1)
        parse_one_page(html, ulist)
    save_contents(ulist)


if __name__ == '__main__':
    main(62)  # 需爬取的页数
