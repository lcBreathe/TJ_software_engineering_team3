For TJU software engineering work by team3.\
Team member： Zhang Xiang,Haowen Zhang,Shenghan Cheng,Yiran Nie.\
Creat time: 2024/3/27\

软件使用说明：\
1.\
本软件的“震后建筑物三维重建”功能使用了英伟达开源软件Instant-NGP，该软件基于英伟达显卡的深度学习功能实现，因此本功能要求用户使用英伟达显卡才能实现建模功能。\
本项目自带的Instant-NGP版本适用于30系和40系显卡，若您的电脑不属于该系列显卡，请点击下列链接下载对应版本Instant-NGP并按步骤替换：\
30系和40系显卡：\
https://github.com/NVlabs/instant-ngp/releases/download/continuous/Instant-NGP-for-RTX-3000-and-4000.zip \
20系显卡：\
https://github.com/NVlabs/instant-ngp/releases/download/continuous/Instant-NGP-for-RTX-2000.zip \
10系显卡:\
https://github.com/NVlabs/instant-ngp/releases/download/continuous/Instant-NGP-for-GTX-1000.zip\
\
若电脑显卡品牌不是英伟达，则无法使用Instant-NGP以及本软件的“震后建筑物三维重建”功能。\
\
下载完成后，请解压到“项目文件”文件夹中，并替换Instant-NGP文件夹，如图所示：\
 ![image](https://github.com/lcBreathe/team3_work/assets/165003424/cf33bd2a-a618-4b9c-be51-c8c4774dd7ca)
打开Instant-NGP文件夹后，内容应和如图所示一样。\
 ![image](https://github.com/lcBreathe/team3_work/assets/165003424/2d70032e-c631-49ad-803e-08a56171f59c)
\
其余有关Instant-NGP内容，请查阅Instant-NGP官方GitHub网页：\
NVlabs/instant-ngp: Instant neural graphics primitives: lightning fast NeRF and more (github.com)\
