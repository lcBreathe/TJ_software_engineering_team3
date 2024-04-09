#!/usr/bin/env python
# coding: utf-8

# In[ ]:


import cv2
import numpy as np
import matplotlib.pyplot as plt
import open3d as o3d

# 读取两张不同角度的图片
img1 = cv2.imread('image1.jpg')
img2 = cv2.imread('image2.jpg')

# 使用 SIFT 特征检测器和匹配器
sift = cv2.SIFT_create()

# 在图像中找到关键点和描述符
kp1, des1 = sift.detectAndCompute(img1, None)
kp2, des2 = sift.detectAndCompute(img2, None)

# 使用 Brute-Force 匹配器
bf = cv2.BFMatcher()
matches = bf.knnMatch(des1, des2, k=2)

# 筛选匹配
good_matches = []
for m, n in matches:
    if m.distance < 0.75 * n.distance:
        good_matches.append(m)

# 提取匹配点的坐标
points1 = np.float32([kp1[m.queryIdx].pt for m in good_matches]).reshape(-1, 1, 2)
points2 = np.float32([kp2[m.trainIdx].pt for m in good_matches]).reshape(-1, 1, 2)


# 计算基础矩阵
F, mask = cv2.findFundamentalMat(points1, points2, cv2.FM_LMEDS)

# 从基础矩阵中恢复相机的姿态
retval, H1, H2 = cv2.stereoRectifyUncalibrated(points1, points2, F, img1.shape)

# 生成点云
points_3d = cv2.recoverPose(F, points1, points2)

# 创建 Open3D 点云对象
pcd = o3d.geometry.PointCloud()
pcd.points = o3d.utility.Vector3dVector(points_3d)

# 可视化点云
o3d.visualization.draw_geometries([pcd])

