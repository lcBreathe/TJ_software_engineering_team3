import copy
import requests
from bs4 import BeautifulSoup
import csv
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import random

def getinfo():
    '''爬取2022年武汉的天气（网页从1月4号开始），以列表的形式返回数据'''
    data=[]
    for i in range(1,13):
        url = 'https://www.tianqi24.com/shanghai/history2023'+f'{i:02d}'+'.html'
        r = requests.get(url)
        demo = r.text  # 服务器返回响应
        soup = BeautifulSoup(demo, "html.parser")
        html= soup.find_all(class_="col6")[0].text   #将制定位置的数据读取成字符串
        #print([html])
        lst=html.split('\n\n\n')         #对字符串进行切分操作
        datatemp=[]
        for x in lst:
            datatemp.append(x.split('\n'))
        for k in datatemp:
            while('' in k):               #去除空字符串
                k.remove('')
        for j in datatemp:
            if len(j)==8:
                j.remove(j[2])              #读取数据时就去除每天夜间的天气情况
            j[1] = j[1].strip()
        if i==1:
            datatemp[0][1]='天气'
            data.append(datatemp[0])     #保留1月的天气属性作为之后的列标签，其余的删除
        #print(data)
        datatemp.pop(0)
        data+=datatemp
    return data

def write_read_csv(data):
    '''接收一个列表，进行写文件,再读取所生成的文件，生成dateframe并返回'''
    with open('2023年上海天气情况.csv','w',encoding='utf-8-sig')as fw:
        writer=csv.writer(fw)
        for i in data:
            writer.writerow(i)
    data = pd.read_csv('2023年上海天气情况.csv')
    data['高温']=data['高温'].apply(lambda x:int(x[:-1]))
    data['低温'] = data['低温'].apply(lambda x: int(x[:-1]))
    return data

write_read_csv(getinfo())