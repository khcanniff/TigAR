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

# GPS Guidence: Mapbox
##### We orignally planned on created a script to handle the GPS detection. We created the script GPS.cs. When we tested the script in real world settings we found the GPS to be inaccurate and issues with the objects appearing. The issues with the objects appearing is that the objects overlayed on the scren but not in relation to the path. If the user moved their phone, then the object would move with them. 
##### We investigated google's GPS API but we ran into a roadblock. The roadblock was that we did not have a credit card to enter into the webpage. Looking into other options we found Mapbox. Mapbox levegares the power of Unity to be able to easlity incorporate AR Foundation. AR Foundation blends both ARCore and ARKit plugins for unity. With AR Foundation, Unity has AR features and prefabs for use in projects. Investigating into Mapbox was difficult at first because of the outdated resources online. While GPS is still not 100% accruate in Mapbox, the accuracy is enough after some manual adjustments within the application. After some trails and errors, a project leveraging Mapbox was created. Below we outline the main steps to implement Mapbox into Unity.
##### The first step was learning the pipeline for Mapbox.
![logo](https://github.com/khcanniff/TigAR/blob/master/Documentation/Mapbox%20Pipeline%20(1).png)
##### Creating the custom map in Mapbox involved removing extra layers, and ensure a valid path for the map. Since the map data was pre existing we checked to make sure that there was a valid path through campus. After ensureing valid paths, we add in a custom data set. The custom data set consisted of GPS coordinate spots to place the spheres at. In Unity we integrated the custom map through a secure URL. 

# Future Workings

# Miscellaneous

