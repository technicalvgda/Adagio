using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    //Used to disable the CameraController.
    public bool follow = true;

    //Object to follow.
    public GameObject player;
    //Smoothness of the Camera when following the player.
    public float smoothTime = 0.5f;
    //Smoothness of the Camera when zooming out to fit the room or zooming in to follow the player.
    public float zoomSmooth = 0.1f;
    //Extra Screen Size
    public float zoomExtra = 1;
    //Game Manager Object in Scene - Need this to get RoomChecker Component
    public GameObject gameManager;
    //Set relative position to player object
    private Vector3 offset;
    //Velocity of the camera
    private Vector3 currentVelocity; 
    //Orthographic Size of Camera when following player.
    private float originalOrthoSize = 5;

     // Use this for initialization
     void Start()
    {
        //Calculates offset by subtracting player position from camera object position
        offset = transform.position;
        originalOrthoSize = Camera.main.orthographicSize;
        follow = true;
    }

    // Update is called once per frame
    // Changed from LateUpdate to FixedUpdate so that Camera follows character more smoothly.
    void FixedUpdate()
    {
        if (follow)
        {
            if (player == null)
            {
                player = GameObject.Find("PlayerPlaceholder(Clone)");
                if (player != null)
                {
                    transform.position = new Vector3(player.transform.position.x, player.transform.position.y, offset.z);
                }
            }
            else
            {
                Room tr = gameManager.GetComponent<RoomChecker>().getRoomIn(player.transform.position);
                //Camera's new position is the player's position offsetted
                if (tr == null)
                {
                    Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originalOrthoSize, zoomSmooth);
                    transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, offset.z), ref currentVelocity, smoothTime);
                }
                else
                {
                    float screenDist = tr.roomWidth > tr.roomHeight ? tr.roomWidth : tr.roomHeight;
                    Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, screenDist / 2 + zoomExtra, zoomSmooth);
                    transform.position = Vector3.SmoothDamp(transform.position, new Vector3(tr.xPos + tr.roomWidth / 2 - 0.5f, tr.yPos + tr.roomHeight / 2 - 0.5f, offset.z), ref currentVelocity, smoothTime);
                }
            }
        }
        else
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originalOrthoSize, zoomSmooth);
        }
    }
}
