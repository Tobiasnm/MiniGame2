import numpy as np


def Initialize(filename, combine):
	data = np.genfromtxt (filename, delimiter=",")
	
	OverallPos = data[:,:3]

	jointRot = data[:,3:]
	jointVel = []
	overallVel = []
	#take hipe rotation as the overall direction
	overallDir = data[:,3:6]
	#set the y-rotation of the hip rotation as 0 to ignore the up-down influcence of the direction
	overallDir[:,1:2]=0

	length = len(jointRot)-1

	#get joint velocity from joint rotation
	#get overall velocity from overall position
	for i in range(0,length):
		jointVel.append(jointRot[i+1]-jointRot[i])
		overallVel.append(OverallPos[i+1]-OverallPos[i])
		combine.append([overallDir[i],overallVel[i],jointRot[i],jointVel[i]])
	jointVel.append(jointRot[length]-jointRot[1])
	overallVel.append(OverallPos[length]-OverallPos[1])
	combine.append([overallDir[length],overallVel[length],jointRot[length],jointVel[length]])
	#jointVel = np.array(jointVel)
	#overallVel = np.array(overallVel)
	#combine = np.array(combine)
	
	#print("jointRot")
	#print(jointRot.shape)
	
	return combine
	
	
array = []
	
array = Initialize('walking.csv', array)

array = np.array(array)

# TODO:
# figure out if the array is correctly configured, too many dimesions..:(
# should be 3,3,57,57
#np.savetxt('array.csv', array, delimiter=",")

#print("array")
print(array[2])