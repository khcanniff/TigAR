# TigAR 
##### An interactive augmented reality map of University of the Pacific, which can be accessed by Android version 8 or above smart phones. Users can use this application to help locate specific buildings on the Pacific campus, along with various other information like historical facts.

###  In our project we have three main features to focus on: 
* Image Recognition
* GPS Guidance
* 2D map. 
### Below are links that refer to each one respectfully.
1) [Vuforia Scene](https://github.com/khcanniff/TigAR/tree/master/TigarApp/Assets/Vuforia) (These are a list of folders that are handled by Vuforia)
2) [Mapbox](https://github.com/khcanniff/TigAR/tree/master/TigAR_GPS-master)
3) [2D Map](https://github.com/khcanniff/TigAR/blob/master/TigarApp/Assets/Scripts/CameraHandler.cs)

### How to build and run the project
##### The program was implemented in the Unity game engine and was built for the Android platform. Here are [folders](https://github.com/khcanniff/TigAR/tree/master/Apks) to the Android executable. Once you build it, you can then download the APK, and install on an Android mobile device. 

# Image Recognition: Vuforia
##### For TigAR to recognize the stickers we provided, we decided to use the Vuforia engine that partners with Unity. The biggest feature of Vuforia that we used were target images, which scan flat objects such as photographs. With these target images, Unity would connect with a Vuforia database that we set up (we had to place in what the target images look like). While in the app whenever the camera detects an intended image, Vuforia would tell Unity that this is the correct image to use. In our case, we would display text boxes showing information when shown a particular sticker.
<img src =https://github.com/khcanniff/TigAR/blob/master/Documentation/ImgRecog.jpg width="500"/>

##### Above is a photo of the image recognition in process. The sticker used for Chambers is a paw print.

```C#
// Check periodically if model target is tracked and in view
float elapsed = Time.realtimeSinceStartup - mLastStatusCheckTime;
if (!StopSearchWhenModelFound && StopSearchWhileTracking && elapsed > 0.5f)
{
    mLastStatusCheckTime = Time.realtimeSinceStartup;

    if (mSearching)
    {
        if (IsModelTrackedInView(mLastRecoModelTarget))
        {
            // Switch Model Reco OFF when model is being tracked/in-view
            mModelRecoBehaviour.ModelRecoEnabled = false;
            mSearching = false;
        }
    }
    else
    {
        if (!IsModelTrackedInView(mLastRecoModelTarget))
        {
            // Switch Mode Reco ON when no model is tracked/in-view
            mModelRecoBehaviour.ModelRecoEnabled = true;
            mSearching = true;
        }
    }
}
```
##### The code above comes from Vuforia and is looped in an Update function. It describes the way Vuforia looks for a current model or image to scan. If a model/image has been scanned successfully, then it tells Vuforia to stop looking for more objects to scan unless there is another model/image that is more predominant in the camera view.

<img src =https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/Vuforia%20target.JPG width="500"/>

##### The image above is one example of the target image as it sits in the Vuforia database. Vuforia places markers (the yellow + signs) on each image to show how it recognizes images. We needed to provide compatible images that would be easy to scan, so stencil-like drawings were the best types for our case.

### Building Vuforia with Unity
##### First download Vuforia and [follow their steps](https://library.vuforia.com/articles/Training/getting-started-with-vuforia-in-unity.html) to install it in the project. Go to Window -> Vuforia Configuration and then add the Vuforia key. This key is located on the Final Submission assignment within README.txt, so copy that key and place it into the "App License Key" section in the inspector. Use this key only since it connects directly to the correct database we used. Once this is done, Vuforia will be ready for development.

# GPS Guidance:
### GPS script (no third party engine)
##### We originally planned on creating a script to handle the GPS detection and guidance. The first plan was to locate where the user was (finding the device GPS) and then place objects in the world at certain target locations. These set locations would leave a path for the user to follow and it would change depending on which building the user wanted to go to. We created the scripts GPS.cs, which parts of it will be shown below. 

```C#
private bool CloseEnoughForMe(double value1, double value2, double acceptableDifference)
{
    distanceFrom3dModel.text = "Distance (Lan,Lon): " + Math.Abs(value1 - value2).ToString();
    return Math.Abs(value1 - value2) <= acceptableDifference;
}

private double distanceBetweenTwoPoints(double value1, double value2)
{
    return Math.Abs(value1 - value2);
}
```
##### The code above descibes the main function for GPS.cs, which determined if a user was in an acceptable range within a target location. If they were, then TigAR would signal to them that they are on the right path (this will be shown in the next code section).

```C#
if (CloseEnoughForMe(Instance.latitude, referenceLatitude, referenceDistance) && CloseEnoughForMe(Instance.longitude,referenceLongitude, referenceDistance))
{
    // Other functionality to tell user they are on right path
}
else
{
    // Other functionality to tell user they are on the wrong path
}
```
##### In this code above, we would keep looping and see if the previous code snippet returned true. If the user was near a target location, the app would display an overlayed object on the screen telling them they are on the right path. If the user is not near an acceptable range, it was suppose to check (not shown above) if the user was falling further away from the check and then send a message to the user that they are on the wrong path.
##### When we tested the script in real life we found the GPS way too inaccurate and had issues with objects we tested to appear. Ultimately we decided to try something new after trying different iterations of the script. We left the code in there to prove that we tried working on it. Right now the script will not work as intended since we left it in an unfinished state.

### Mapbox:
##### We investigated google's GPS API but we ran into a roadblock. The roadblock was that we did not have a credit card to enter into the webpage. Looking into other options we found Mapbox. Mapbox leverages the power of Unity to be able to easily incorporate AR Foundation. AR Foundation blends both ARCore and ARKit plugins for unity. With AR Foundation, Unity has AR features and prefabs for use in projects. Investigating into Mapbox was difficult at first because of the outdated resources online. While GPS is still not 100% accurate in Mapbox, the accuracy is enough after some manual adjustments within the application. After some trails and errors, a project leveraging Mapbox was created. Below we outline the main steps to implement Mapbox into Unity.
##### The first step was learning the pipeline for Mapbox. Below is an image showing the basic flow between Mapbox and Unity.
![logo](https://github.com/khcanniff/TigAR/blob/master/Documentation/Mapbox%20Pipeline%20(1).png)
##### Creating the custom map in Mapbox involved removing extra layers, and ensured a valid path for the map. Since the map data was preexisting we checked to make sure that there was a valid path through campus. After we confirmed valid paths, we added in a custom data set. The custom data set consisted of GPS coordinate spots to place the spheres at. In Unity we integrated the custom map through a secure URL. Below is a screenshot for the custom map. ![logo](https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/Mapbox%20custom%20map.JPG) 
##### Due to the lack of updated resources online, the existing layers in the map was researched in order to figure out how the layers worked. We found out that a custom dataset was required to add in for the map layer. Below you can see a screenshot showing the interface to create the custom dataset. 
![logo](https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/Mapbox%20custom%20dataset.JPG)

##### Now that the Mapbox custom was created the next step was implementing Mapbox into Unity. After importing the Mapbox package into Unity, there was a various prefab in the asset folders. Below is a screenshot of the hierarchy where each game object will be discussed. ![logo](https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/Unity%20Mb%20prefabs.JPG)

##### The Mapbox hierarchy explained
* AR Root Game Object
    - ##### Handles the AR camera from AR Foundation  
    - ##### Cross platform AR development
    - ##### Handles the view of the debug camera to use in the editor
    - ##### Red arrow which represents the user's location on the map

* MapHolder game object explained
    - ##### Handles the placement of the map on the screen
    - ##### Where Mapbox data can be accessed 
    - ##### AR Aligned Map is where the main Mapbox interface is located. There are three components on this game object
        - ##### 1) Update Map Pos By AR Plane Y handles the update of the map with the AR plane 
        - ##### 2) Initialize Map With Location Provider places the users location on the map
        - ##### 3) Abstract Map handles the main Mapbox integration into Unity. Therea are four main settings under Abstract Map
            - ##### 1) General handles the starting location in play mode within Unity. It also handles the basic design of the map layout.
            - ##### 2) Image sources from Mapbox to stylize the look of the displayed map
            - ##### 3) Terrain provides the ability to adjust the terrain from flat or add in elevation
            - ##### 4) Map Layers connect in the Mapbox URL to allow us to access the custom map data. Through the Map Layer, POI's can be placed on the map to appear to the user while they use the application. For each POI we placed a prefab into the object reference. For the POI on the building we have a prefab with 3D text, and for the guidance POI we placed orange guidance spheres. This game object also handles the features of the map. The path was added with a blue transparent material to create a visually pleasing path. After placing the POI on the maps, we did tests on the campus. Some adjustments had to be made to ensure correct location to reflect real life conditions. 

* ##### Debug Canvas game object displays the text on the map and the UI features like the repositioning button. 
* ##### Location Provider handles the phone integration. Handles obtaining the phone's GPS to accurately update the map. This game object also provided in scene testing.
* ##### Info Canvas displays the initial pop-up warning the user to pay attention to their surroundings
#### The image below is the final product.

![logo](https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/Screenshot%20of%20IRL%20test.JPG)

### Building Mapbox with Unity
##### Download Mapbox and [follow their steps](https://www.mapbox.com/unity/) to install it in the project. Go to Mapbox -> Setup and then add the Mapbox key. This key is located on the Final Submission assignment within README.txt, so copy that key and place it into the "Access Token" section in the popup. Use this key only since it connects directly to the correct map we customized on the Mapbox studio site online. Once this is done, Mapbox will be ready for development.

# 2D Map
##### We thought it would be helpful for users of the application to have access to a static version of the map. We downloaded an online PDF of the map online and imported it into Unity. Once the UOP map was in Unity we wanted the user to be able to pan zoom into the map for ease of visualization. There was existing tutorial online for how to pan and zoom on a mobile phone game. We adapted the code to fit our project and it is located in the CameraHandler.cs script. The user can pinch the map to zoom in and out, while panning around the map with the ease of a finger touching the screen. 
```C#
void PanCamera(Vector3 newPanPosition) //update the camera's position and checks the bounds to prevent the user from zooming out into "space" or out of the scene
{
    // Determine how much to move the camera
    Vector3 offset = theCamera.ScreenToViewportPoint(lastPanPosition - newPanPosition);
    Vector3 move = new Vector3(offset.x * speedToPan, offset.y * speedToPan, 0); // changed the value to make the map move up and not zoom (x value)

    // move the camera
    transform.Translate(move, Space.World);

    // check the bounds of the camera to prevent the user from going out of the scene or into "space" we made an adjustment here to ensure that all axis were being checked
    Vector3 pos = transform.position;
    pos.x = Mathf.Clamp(transform.position.x, boundsX[0], boundsX[1]);
    pos.z = Mathf.Clamp(transform.position.z, boundsZ[0], boundsZ[1]);
    pos.y = Mathf.Clamp(transform.position.y, boundsY[0], boundsY[1]);
    transform.position = pos;

    // Cache the position
    lastPanPosition = newPanPosition;
}

void ZoomCamera(float offset, float speed) //update the camera's FOV
{
    if (offset == 0)
    {
        return;
    }
    theCamera.fieldOfView = Mathf.Clamp(theCamera.fieldOfView - (offset * speed), zoomBounds[0], zoomBounds[1]);
}
```
##### The code above describes the main feature for panning in and out of the map. The first function handles moving the map whenever the user tries "swiping" in a certain direction. The second function describes the zooming effect that can zoom in or out of the map. With these two functions it made handling the map image much easier for the user to adjust for visuals.

# Design Process
##### Since the beginning we have planned For how TigAR should look to the user. We started with an initial draft design document so that the team can be on the same page for how the app was going to look like. Below is a table to show the process from our inital design to the new design:
**Name** | **Draft** | **Final**
--- | --- | ---
Main Menu | <img src =https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/DraftMenu.PNG width="500"/> | <img src =https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/FinalMenu.jpg width="500"/>
Building Info Page | <img src =https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/DraftBuildingInfo.PNG width="500"/> | <img src =https://github.com/khcanniff/TigAR/blob/master/Documentation/ImgRecog.jpg width="500"/>
Locate Form| <img src =https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/DraftForm.PNG width="500"/> | <img src =https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/FinalForm.jpg width="500"/> 
GPS | <img src =https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/DraftGPS.PNG width="500"/>  | <p align="center"><img src =https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/FinalGPS.jpg height="300"/> </p>
About Page | <img src =https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/DraftAbout.PNG width="500"/> | <img src =https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/FinalAbout.jpg width="500"/> 
##### Going down the table each screenshot shows the main menu, building information page, locate building page form, GPS design, and the about page, in respective order. The 2D map is not included since it is just an image and does not show a significant difference.
##### The new designs show more vibrant colors and is much more pleasing to the eye than the ones we had in the draft/planning phases. The UI interface is also more realistic in terms of what the user should be seeing. This can be in the building information screenshots. The draft on the left side does not fully capture what we want the user to see, unlike the one on the right which is a better depiction of the feature at work. Everything else has been changed to improve the user experience.

# Future Workings
##### There are other features we hope to include in the future:
* Include more buildings from UOP
* Add class schedules personalized by the user
* Include room numbers/classes held per building held in the informational text boxes for image recognition
* Display additional information for viewing the traditional 2D map
* Make the application iOS compatible/friendly

