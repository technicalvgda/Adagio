using UnityEngine;
using System.Collections;

public class ZoomOut : MonoBehaviour
{
    Camera playerCamera;
    CameraController playerCamControl;
    Vector3 cameraLocation;
    public float zoomAmount;
    float originalZoomAmount;

	// Use this for initialization
	void Start ()
    {

        playerCamera = Camera.main;
        playerCamControl =playerCamera.GetComponent<CameraController>();
        cameraLocation = this.transform.position;
        originalZoomAmount = playerCamera.orthographicSize;
        zoomAmount += playerCamera.orthographicSize;
        cameraLocation.z = playerCamera.transform.position.z;
    }
	
	
    void OnTriggerStay2D(Collider2D col)
    {
       
        if (col.tag == "Player")
        {
            StopCoroutine(ReturnZoom());
            playerCamControl.enabled = false;
            playerCamera.transform.position =Vector3.Lerp(playerCamera.transform.position, cameraLocation, Time.deltaTime);
            if (playerCamera.orthographicSize < zoomAmount)
            {
                playerCamera.orthographicSize += 0.1f;
                if(playerCamera.orthographicSize > zoomAmount + 0.01f )
                {
                    playerCamera.orthographicSize = zoomAmount;
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerCamControl.enabled = true;
            //playerCamera.orthographicSize = originalZoomAmount;
            StartCoroutine(ReturnZoom());
        }
    }

    IEnumerator ReturnZoom()
    {
        
        while (playerCamera.orthographicSize > originalZoomAmount)
        {
            playerCamera.orthographicSize -= 0.1f;
            yield return new WaitForFixedUpdate();    
        }
        playerCamera.orthographicSize = originalZoomAmount;
        yield return null;
    }
}
