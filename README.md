# GraphX3D
Software 3D Engine - (Dynamic Link Library For Visual Basic .NET)


This project's goal is to recreate a 3D Graphics Engine in order to learn how 3D Graphics work, and how to use 3D Engines in the most optimal way. The entire software runs on the CPU (we don't want the graphic card to take any of the work away since the goal is learning).  

IMPORTANT NOTICE -> This is still a work in progress. I usually prefer releasing finished and fully tested programs. Updates will follow soon.  

Upcoming Critical Updates : 

1)Fix Performance Issue Caused by "SetPixel" Function (Modify Bitmap Bytes Directly Instead)  
2)Depth Buffer (Fix Occlusion)  
3)Interpolation for Shader (Per-pixel Lighting Instead of Per-Triangle)  

Upcoming Secondary Updates :

1)Rework the code structure and naming, child-proof all public functions  
2)Improve Mesh Object importation routine (wider range of file types)  
3)Texturing  

The 6 main classes can be found in the GraphX3D folder :

PVector (3D Points & Vectors + Operations)  
MeshObject.TriFace (Faces of 3D objects)  
MeshObject (Mesh From Triangles - Can be imported from simple .obj files)  
Matrices (Easy Matrix Creation)  
FPCamera (Basic First Person Camera)  
Rasterizer (Handles Sorting, Clipping, Drawing)  

To see the results download the "Pre_Release" folder and run the .exe file (Windows may pop up a warning; the project is not signed)
I didn't plan for an early release, came up with this demo project in a day for demonstration purpose. The controls are quite whacky and need to be worked on but should still allow observation of the progress. Heavy performance (speed) issues currently come from the only external function call I'm using to draw on bitmaps, should run decently in the next update. 

Forward : W  
Backward:  S  
Left : A  
Right : D  
Up : Space  
Down : Left Shift  

Toggle WireMesh : Backspace  
Toggle MouseLock : Escape  
Set Light Source To Current Location : Enter  

