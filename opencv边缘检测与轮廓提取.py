#!/usr/bin/env python
# coding: utf-8

# In[1]:


#边缘检测
import cv2 as cv
import numpy as np


def edge_demo(image):
    blurred = cv.GaussianBlur(image, (3, 3), 0)  # 高斯模糊
    gray = cv.cvtColor(blurred,cv.COLOR_BGR2GRAY)  # 灰路图像
    xgrad = cv.Sobel(gray, cv.CV_16SC1, 1, 0)  # xGrodient
    ygrad = cv.Sobel(gray, cv.CV_16SC1, 0, 1)  # yGrodient
    edge_output = cv.Canny(xgrad, ygrad, 100, 150)  # edge
    cv.imshow("Canny Edge",edge_output)

    # #  彩色边缘
    # dst = cv.bitwise_and(image, image, mask=edge_output)
    # cv.imshow("Color Edge", dst)


src=cv.imread("D:\\1111.png")
cv.namedWindow("input image",cv.WINDOW_AUTOSIZE)
cv.imshow("input image",src)
edge_demo(src)
cv.waitKey(0)


cv.destroyAllWindows()


# In[2]:
#非边缘检测轮廓提取
import cv2 as cv
def contours_demo(image):
      blurred = cv.GaussianBlur(image, (3, 3), 0)
      gray = cv.cvtColor(blurred, cv.COLOR_RGB2GRAY)  # 灰路图像
     # ret , binary = cv.threshold(gray, 0, 255,cv.THRESH_BINARY| cv.THRESH_OTSU) # 图像二值化
      ret, binary = cv.threshold(gray, 68, 255, cv.THRESH_BINARY )  # 图像二值化
      cv.imshow("binary image",binary)


      contours, heriachy = cv.findContours(binary,cv.RETR_LIST,cv.CHAIN_APPROX_SIMPLE)
      for i, contour in enumerate(contours):
            cv.drawContours(image,contours,i,(0,0,255),6)   #6的改为-1可以填充
            print(i)
      cv.imshow("detect contours", image)


src=cv.imread("D:\\1111.png")
cv.namedWindow("input image",cv.WINDOW_AUTOSIZE)
#cv.imshow("input image",src)
contours_demo(src)
cv.waitKey(0)

cv.destroyAllWindows()

# In[3]:
#边缘检测和轮廓调节
import cv2 as cv


def edge_demo(image):
    blurred = cv.GaussianBlur(image, (3, 3), 0)  # 高斯模糊
    gray = cv.cvtColor(blurred,cv.COLOR_BGR2GRAY)  # 灰路图像
    xgrad = cv.Sobel(gray, cv.CV_16SC1, 1, 0)  # xGrodient
    ygrad = cv.Sobel(gray, cv.CV_16SC1, 0, 1)  # yGrodient
    edge_output = cv.Canny(xgrad, ygrad, 100, 150)  # edge
    return edge_output
    def contours_demo(image):
     #  blurred = cv.GaussianBlur(image, (3, 3), 0)
     #  gray = cv.cvtColor(blurred, cv.COLOR_RGB2GRAY)  # 灰路图像
     # # ret , binary = cv.threshold(gray, 0, 255,cv.THRESH_BINARY| cv.THRESH_OTSU) # 图像二值化
     #  ret, binary = cv.threshold(gray, 68, 255, cv.THRESH_BINARY )  # 图像二值化
     #  cv.imshow("binary image",binary)

      binary = edge_demo(image)

      contours, heriachy = cv.findContours(binary,cv.RETR_LIST,cv.CHAIN_APPROX_SIMPLE)
      for i, contour in enumerate(contours):
            cv.drawContours(image,contours,i,(0,0,255),6)   #6的改为-1可以填充
            print(i)
      cv.imshow("detect contours", image)


src=cv.imread("D:\\1111.png")
cv.namedWindow("input image",cv.WINDOW_AUTOSIZE)
#cv.imshow("input image",src)
contours_demo(src)
cv.waitKey(0)

cv.destroyAllWindows()

