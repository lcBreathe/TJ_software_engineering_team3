# -*- coding: cp936 -*-
import cv2
import numpy as np
import glob

# ����Ѱ�������ؽǵ�Ĳ��������õ�ֹͣ׼�������ѭ������30������������0.001
criteria = (cv2.TERM_CRITERIA_EPS + cv2.TERM_CRITERIA_MAX_ITER, 30, 0.001)

# ��ȡ�궨��ǵ��λ��
objp = np.zeros((4*6,3), np.float32)
objp[:,:2] = np.mgrid[0:6,0:4].T.reshape(-1,2)  # ����������ϵ���ڱ궨���ϣ����е��Z����ȫ��Ϊ0������ֻ��Ҫ��ֵx��y

obj_points = []  # �洢3D��
img_points = []  # �洢2D��

images = glob.glob("images4/*.jpg")
i=0;
for fname in images:
    img = cv2.imread(fname)
    gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    size = gray.shape[::-1]
    ret, corners = cv2.findChessboardCorners(gray, (6, 4), None)
    #print(corners)

    if ret:

        obj_points.append(objp)

        corners2 = cv2.cornerSubPix(gray, corners, (5, 5), (-1, -1), criteria)  # ��ԭ�ǵ�Ļ�����Ѱ�������ؽǵ�
        #print(corners2)
        if [corners2]:
            img_points.append(corners2)
        else:
            img_points.append(corners)

        cv2.drawChessboardCorners(img, (6, 4), corners, ret)  # ��ס��OpenCV�Ļ��ƺ���һ���޷���ֵ
        i+=1;
        cv2.imwrite('conimg'+str(i)+'.jpg', img)
        cv2.waitKey(4000)

print(len(img_points))
cv2.destroyAllWindows()

# �궨
ret, mtx, dist, rvecs, tvecs = cv2.calibrateCamera(obj_points, img_points, size, None, None)

print("ret:", ret)
print("mtx:\n", mtx) # �ڲ�������
print("dist:\n", dist)  # ����ϵ��   distortion cofficients = (k_1,k_2,p_1,p_2,k_3)
print("rvecs:\n", rvecs)  # ��ת����  # �����
print("tvecs:\n", tvecs ) # ƽ������  # �����

print("-----------------------------------------------------")

img = cv2.imread(images[2])
h, w = img.shape[:2]
newcameramtx, roi = cv2.getOptimalNewCameraMatrix(mtx,dist,(w,h),1,(w,h))#��ʾ����Χ��ͼƬ��������ӳ��֮���ɾ��һ����ͼ��
print (newcameramtx)
print("------------------ʹ��undistort����-------------------")
dst = cv2.undistort(img,mtx,dist,None,newcameramtx)
x,y,w,h = roi
dst1 = dst[y:y+h,x:x+w]
cv2.imwrite('calibresult3.jpg', dst1)
print ("����һ:dst�Ĵ�СΪ:", dst1.shape)
