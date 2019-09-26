README for the RenovateAR App
=============================

RenovateAR is a mobile application based on on augmented reality.
This application is built on unity3D platform using Google ARCore.
Main focus of this application is to adapt AR technology to enhance the 
newly renovated buildings, by showcasing ideas of different interior of 
office or home. User can renovate their offices or houses walls, floors,
place furniture or change the texture of the furnitures etc. Provide a 
new virtual reality and physical reality experience by merging them through 
a portal.

App Running Instructions
========================
Step 1:
1.Minimum API level to run this application is Android 7.1 Nougat/ API Level 25
2.Install the APK RenovateAR_V0.3.apk
3.Run the Application
Step 2:
4.Use the provided marker image (print it that image will work as controller) as controller
image name: marker.jpg
5.Use phone camera to get controller from the marker image.
6.After getting the controller move the image controller will move with the image.
Step 3:
7.Use the phone camera to get grid on the floor plane
8.When you get the visible grid on the floor, tap the phone screen to activate AR portal
9.After activating portal now you can get inside the virtual portal and play around that portal
Step 4:
10.You can see furnitures inside the portal(portal basically a virtual setup of house/office)
use your controller to touch furnitures.
11.When you touch a furniture the color of the furniture will be changed(turn into red)
12.When the furniture color got changed touch the phone screen to activate voice recognition
13.Before activating voice recognition the furniture color will change to Magenta
14.When the color got changed to Magenta use voice command to do few changes
15.Available Voice commands:
	a.Change Color (to change the color)
	b.Up(to move the object up)
	c.Down(to move the object down)
	d.Forward(to move the object forward)
	e.Back(to move the object back)
	f.Move Right(to move the object right)
	g.Move Left(to move the object Left)

These are the basic set of instructions now install and play around Augmented Portal and Enjoy!

Source Code Content:
===================
Assets			- All the assets of the application is inside that folder.
AUTHORS.txt		- Authors of RenovateAR application
Library			- Asset database, cahce, and other assets inside that folder.
obj			- Debug log inside that.
Packages		- manifest.jsno file keeps the packages list.
ProjectSetting		- all the manager files inside that.
marker.jpg		- marker image to activate and use controller.
README.txt		- Instructions are in there.
RenovateAR_V0.3.apk	- apk file to install and play.