# Facial landmarks with dlib, OpenCV, and PythonPython

# import the necessary packages
from imutils import face_utils
import numpy as np
import argparse
import imutils
import dlib
import cv2
import xlsxwriter
import glob
import os
def main():
    # construct the argument parser and parse the arguments
    ap = argparse.ArgumentParser()
    ap.add_argument("-p", "--shape-predictor", required=True, help="path to facial landmark predictor")
    ap.add_argument("-i", "--image", required=True, help="path to input image")
    ap.add_argument('--draw', nargs='?', const=True, type=bool, default=False, help="Fill landmarks")

    args = vars(ap.parse_args())

    # initialize dlib's face detector (HOG-based) and then create
    # the facial landmark predictor
    detector = dlib.get_frontal_face_detector()
    predictor = dlib.shape_predictor(args["shape_predictor"])
    str=args["image"]
    # load the input image, resize it, and convert it to grayscale
    image = cv2.imread(args["image"])
    # image = imutils.resize(image, width=500)

    if args['draw']:
        draw_individual_detections(image, detector, predictor)
    else:  
        show_raw_detection(image, detector, predictor, str)

def show_raw_detection(image, detector, predictor, str):
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
    # detect faces in the grayscale image
    rects = detector(gray, 1)
    str=str.rsplit('.',1)[0]
    print(str)
    workbook = xlsxwriter.Workbook(str + '.xlsx')
    worksheetData = workbook.add_worksheet('Data')
    worksheet = workbook.add_worksheet(str)
    worksheet.write('A1','Fatin')
    listFiles=glob.glob(str[0:2]+"//Results/*.jpg")
    listFileNm= os.listdir(str[0:2]+"//Results".rsplit('.', 1)[0])
    count=0

    print(listFileNm)
    print(listFiles)
    # loop over the face detections

        # determine the facial landmarks for the face region, then
        # convert the facial landmark (x, y)-coordinates to a NumPy
        # array

    rect=rects[0]
    shape = predictor(gray, rect)
    shape = face_utils.shape_to_np(shape)

    # convert dlib's rectangle to a OpenCV-style bounding box
    # [i.e., (x, y, w, h)], then draw the face bounding box
    (x, y, w, h) = face_utils.rect_to_bb(rect)
    cv2.rectangle(image, (x, y), (x + w, y + h), (0, 255, 0), 2)

    # show the face number
    cv2.putText(image, "Face".format(1), (x - 10, y - 10),
                cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 255, 0), 2)
    row=0
    col=0



    # loop over the (x, y)-coordinates for the facial landmarks
    # and draw them on the image
    #worksheet.write(shape)
    for (x, y) in shape:

        cv2.circle(image, (x, y), 2, (255, 0, 0), -1)
        worksheet.write(row,col, x)
        worksheet.write(row,col+1, y)
        row = row+1
    count=0
    for x in listFileNm:

        print(x)
        worksheet = workbook.add_worksheet(x.rsplit('.', 1)[0])
        worksheetData.write(count,0, x.rsplit('.', 1)[0])
        image2 = cv2.imread(listFiles[count])
        gray2 = cv2.cvtColor(image2, cv2.COLOR_BGR2GRAY)
        # detect faces in the grayscale image
        rects2 = detector(gray2, 1)
        shape2 = predictor(gray2, rects2[0])
        shape2 = face_utils.shape_to_np(shape2)
        row=0
        col=0
        count2=0
        max=0
        av=0
        av2=0
        for (x, y) in shape2:

            worksheet.write(row, col, x)
            #print(shape[row][col],' ',shape[row][col+1],'***',shape2[row][col],' ',shape2[row][col+1],'*****',shape[row][col]-shape2[row][col],' ',shape[row][col+1]-shape2[row][col+1])
            worksheet.write(row, col + 1, y)
            worksheet.write(row,col+3,shape[row][col])
            worksheet.write(row, col + 4, shape[row][col+1])
            val=abs(shape[row][col]-shape2[row][col])+abs(shape[row][col+1]-shape2[row][col+1])
            worksheet.write(row,col+6,val )
            if val>max:
                max=val
            count2=count2+1
            av=av+val
            if val<0.00005*h*w:
                av2+=1

            row = row + 1
            #print(shape[row][col+1])
        worksheetData.write(count,1,max)
        av=av/68
        av2=av2/68
        print ('av=',av,' ','count2=',count2)
        worksheetData.write(count,col+2,av)
        worksheetData.write(count, col + 4, av2)
        if count+1 ==len(listFiles):

            print(len(listFiles)-1)
            break
        else:
            count=count+1


    workbook.close()

    # show the output image with the face detections + facial landmarks
    cv2.imshow("Output", image)
    cv2.waitKey(0)


def draw_individual_detections(image, detector, predictor):
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)

    # detect faces in the grayscale image
    rects = detector(gray, 1)

    # loop over the face detections
    for (i, rect) in enumerate(rects):
        # determine the facial landmarks for the face region, then
        # convert the landmark (x, y)-coordinates to a NumPy array
        shape = predictor(gray, rect)
        shape = face_utils.shape_to_np(shape)

        # loop over the face parts individually
        for (name, (i, j)) in face_utils.FACIAL_LANDMARKS_IDXS.items():
            # clone the original image so we can draw on it, then
            # display the name of the face part on the image
            clone = image.copy()
            cv2.putText(clone, name, (10, 30), cv2.FONT_HERSHEY_SIMPLEX,
                        0.7, (0, 0, 255), 2)

            # loop over the subset of facial landmarks, drawing the
            # specific face part
            for (x, y) in shape[i:j]:
                cv2.circle(clone, (x, y), 1, (0, 0, 255), -1)

            # extract the ROI of the face region as a separate image
            (x, y, w, h) = cv2.boundingRect(np.array([shape[i:j]]))
            roi = image[y:y + h, x:x + w]
            roi = imutils.resize(roi, width=250, inter=cv2.INTER_CUBIC)

            # show the particular face part
            cv2.imshow("ROI", roi)
            cv2.imshow("Image", clone)
            cv2.waitKey(0)

        # visualize all facial landmarks with a transparent overlay
        output = face_utils.visualize_facial_landmarks(image, shape)
        cv2.imshow("Image", output)
        cv2.waitKey(0)


if __name__ == '__main__':
    main()

