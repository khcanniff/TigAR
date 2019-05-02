# TigAR 
##### is an interactive augmented reality map of University of the Pacific, which can be accessed by any compatible smart phone mobile device. Users can use this application to help locate specific buildings on the Pacific campus, along with various other information like historical facts.

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

# GPS Guidance: Mapbox [WIP]
##### We originally planned on created a script to handle the GPS detection. We created the script GPS.cs [Jamie, please add script here]. When we tested the script in real world settings we found the GPS to be inaccurate and issues with the objects appearing. The issues with the objects appearing is that the objects overplayed on the screen but not in relation to the path. If the user moved their phone, then the object would move with them. 
##### We investigated google's GPS API but we ran into a roadblock. The roadblock was that we did not have a credit card to enter into the webpage. Looking into other options we found Mapbox. Mapbox leverages the power of Unity to be able to easily incorporate AR Foundation. AR Foundation blends both ARCore and ARKit plugins for unity. With AR Foundation, Unity has AR features and prefabs for use in projects. Investigating into Mapbox was difficult at first because of the outdated resources online. While GPS is still not 100% accurate in Mapbox, the accuracy is enough after some manual adjustments within the application. After some trails and errors, a project leveraging Mapbox was created. Below we outline the main steps to implement Mapbox into Unity.
##### The first step was learning the pipeline for Mapbox.
![logo](https://github.com/khcanniff/TigAR/blob/master/Documentation/Mapbox%20Pipeline%20(1).png)
##### Creating the custom map in Mapbox involved removing extra layers, and ensure a valid path for the map. Since the map data was preexisting we checked to make sure that there was a valid path through campus. After ensuring valid paths, we add in a custom data set. The custom data set consisted of GPS coordinate spots to place the spheres at. In Unity we integrated the custom map through a secure URL. Below is a screenshot for the custom map. ![logo](https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/Mapbox%20custom%20map.JPG) 
##### Due to the lack of updated resources online, the existing layers in the map was researched in order to figure out how the layers worked. We found out that a custom dataset was required to add in for the map layer. Below you can see a screenshot showing the interface to create the custom dataset. 
![logo](https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/Mapbox%20custom%20dataset.JPG)

##### Now that the Mapbox custom was created the next step was implementing Mapbox into Unity. After importing the Mapbox package into Unity, there was a various prefab in the asset folders. Below is a screenshot of the hierarchy where each game object will be discussed. ![logo](https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/Unity%20Mb%20prefabs.JPG)

##### The AR Root game object handles the AR camera from AR Foundation. This object allows for cross platform AR development. MapCamera game object handles the view of the debug camera to use in the editor, and the red arrow which represents the user's location on the map.
#### Debug Canvas game object displays the text on the map and the UI features like the repositioning button. 
#### Location Provider handles the phone integration. Handles obtaining the phone's GPS to accurately update the map. This game object also provided in scene testing.
#### Info Canvas displays the initial pop-up warning the user to pay attention to their surroundings
#### MapHolder game object handles the placement of the map on the screen and where Mapbox data can be accessed. The game object AR Aligned Map is where the Mapbox data is location is interfaced through. Three components are on the AR Aligned game object: Update Map Pos By AR Plane Y, Initialize Map With Location Provider, and Abstract Map. Update Map Pos By AR Plane Y handles the update of the map with the AR plane. Initialize Map With Location Provider places the users location on the map. Abstract Map handles the main Mapbox integration into Unity. The four main options under Abstract Map are General, Image, Terrain, and Map Layers. General handles the starting location in play mode within Unity. It also handles the basic design of the map layout. Image sources from Mapbox to stylize the look of the displayed map. Terrain provides the ability to adjust the terrain from flat or add in elevation. Map Layers connect in the Mapbox URL to allow us to access the custom map data. Through the Map Layer POI can be placed on the map to appear to the user while they use the application. For each POI we placed a prefab into the object reference. For the POI on the building we have a prefab with 3D text, and for the guidance POI we placed orange guidance spheres. This game object also handles the features of the map. The path was added with a blue transparent material to create a visually pleasing path. After placing the POI on the maps, we did tests on the campus. Some adjustments had to be made to ensure correct location to reflect real life conditions. 
![logo](https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/Screenshot%20of%20IRL%20test.JPG)


# Future Workings
##### There are other features we hope to include in the future:
* Include more buildings from UOP
* Add class schedules personalized by the user
* Include room numbers/classes held per building held in the informational text boxes for image recognition
* Display additional information for viewing the traditional 2D map

# Miscellaneous


