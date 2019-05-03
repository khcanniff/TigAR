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

##### [Please talk about the why Vuforia targets image.] 

<img src =https://github.com/khcanniff/TigAR/blob/master/Documentation/Images/Vuforia%20target.JPG/>

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
## 2D Map
#### We thought it would be helpful for users of the application to have access to the online version of the map. We downloaded the online PDF of the map online and imported it into Unity. Once the UOP map was in Unity we wanted the user to be able to pan zoom the map for ease of visualization. There was existing tutorial online for how to pan and zoom on a mobile phone game. Adapting the code to fit our project the CameraHandler.cs code is shown below with appropriate comments. The user can pinch the map to zoom in and out, while panning around the map with the ease of a finger touching the screen. 
```C#
using UnityEngine;
using System.Collections;
//code from https://kylewbanks.com/blog/unity3d-panning-and-pinch-to-zoom-camera-with-touch-and-mouse-input
// adjusted to fit our application needs
public class CameraHandler : MonoBehaviour
{
    private static readonly float[] boundsX = new float[] { -5f, 5f }; //upper and lower bounds on the x axis
    private static readonly float[] boundsZ = new float[] { -9f, -2f }; //upper and lower bounds on the Z axis
    private static readonly float[] boundsY = new float[] { -10f, 10f }; //upper and lower bounds on the Y axis
    private static readonly float[] zoomBounds = new float[] { 10f, 85f }; //bounds to prevent the user from panning/zooming out of the picture/scene view
    private static readonly float speedToPan = 17f; //the speed for the camera movement when looking around on the map
    private static readonly float zoomSpeedTouch = 0.1f; //the speed for when the user is touching the screen
    private static readonly float zoomSpeedMouse = 0.5f;

    private Camera theCamera;

    private Vector3 lastPanPosition; //location of the finger from the last frame

    //below are 3 lines for touch mode
    private int panFingerId; //tracks the ID of which finger is being used to pan
    private bool wasZoomingLastFrame; //was camera zoomed in last frame?
    private Vector2[] lastZoomPositions; //the screen cordinatios for where the finger was in last frame 

    void Awake()
    {
        theCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer) //if the user touches the phone this fires off
        {
            HandleTouch();
        }

    }

    void HandleTouch()
    {
        switch (Input.touchCount) //swtch based on the number of touches
        {

            case 1: //handles the panning because only one finger is being used
                wasZoomingLastFrame = false; //since we are panning now, we are not zooming
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) //If the touch is true the we need the position and which finger is touching
                {
                    lastPanPosition = touch.position; //move the position to where the finger is touching
                    panFingerId = touch.fingerId; //sets the pan fingure ID to the touching finger
                }
                else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved) //we know that the finger moved/panned the camera so update the camera's postions
                {
                    PanCamera(touch.position);
                }
                break;

            case 2: //Handles the zooming because two fingers is being used
                Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position }; //store the position of the two fingers. we use this for later to know the offset for the pinch and zoom interaction
                if (!wasZoomingLastFrame) //not zooming in the last frame, and since there are two fingers touching, then we are not startng the zoom
                {
                    lastZoomPositions = newPositions;
                    wasZoomingLastFrame = true;
                }
                else //we are now zooming, so we need to get the distance between the two fingers. this provides the functionality for the pinch and zoom interaction. 
                {
                    float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                    float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                    float offset = newDistance - oldDistance;

                    ZoomCamera(offset, zoomSpeedTouch); //update the camera zoom

                    lastZoomPositions = newPositions;
                }
                break;

            default:
                wasZoomingLastFrame = false; //done zooming or wasnt zooming
                break;
        }
    }


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
}
```
## [Mention other scripts here please my peeps]
## [Please talk about our design process of the UI interface, include screenshots of design please]
