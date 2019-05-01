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