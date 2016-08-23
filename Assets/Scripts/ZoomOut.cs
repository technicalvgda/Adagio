using UnityEngine;
using System.Collections;

public class ZoomOut : MonoBehaviour
{
    Camera playerCamera;
    CameraController playerCamControl;
    Vector3 cameraLocation;
    float originalZoomAmount;
	CrossFadeControl audioHandler;
	float vol1, vol2, vol3;
    //custom variables
    public float zoomAmount;

    public GameObject leftXBorder, rightXBorder, topYBorder, bottomYBorder;
    Transform player;
	public bool isZoomedIn= false;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerCamera = Camera.main;
        playerCamControl =playerCamera.GetComponent<CameraController>();
        cameraLocation = this.transform.position;
        originalZoomAmount = playerCamera.orthographicSize;
        zoomAmount += playerCamera.orthographicSize;
        cameraLocation.z = playerCamera.transform.position.z;
		audioHandler = GameObject.Find ("AudioHandler").GetComponent<CrossFadeControl> ();
    }

    void Update()
    {
        if(player != null)
        {
            if (ZoneCheck())
            {
                
                if(isZoomedIn == false)
                {
                    EnterZone();
                }
                else
                {
                    MoveCam();
                }
               
            }
            else
            {
                if (isZoomedIn == true)
                {
                    ExitZone();
                }
            }
        }
       
    }
	
	bool ZoneCheck()
    {
        if(player.position.x < rightXBorder.transform.position.x && player.position.x > leftXBorder.transform.position.x
            && player.position.y < topYBorder.transform.position.y && player.position.y > bottomYBorder.transform.position.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void EnterZone()
    {
		isZoomedIn = true;
        vol1 = audioHandler.audio1.volume;
        if (audioHandler.audio1.volume > 0.5f)
        {
            audioHandler.audio1.volume = 0.2f;
        }
        vol2 = audioHandler.audio3.volume;
        if (audioHandler.audio2.volume > 0.5f)
        {
            audioHandler.audio2.volume = 0.2f;
        }
        vol3 = audioHandler.audio3.volume;
        if (audioHandler.audio3.volume > 0.5f)
        {
            audioHandler.audio3.volume = 0.2f;
        }
        playerCamControl.enabled = false;


    }

    void ExitZone()
    {
		isZoomedIn = false;
        audioHandler.audio1.volume = vol1;
        audioHandler.audio2.volume = vol2;
        audioHandler.audio3.volume = vol3;

        playerCamControl.enabled = true;
        //playerCamera.orthographicSize = originalZoomAmount;
        StartCoroutine(ReturnZoom());
    }

    void MoveCam()
    {
        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, cameraLocation, Time.deltaTime);
        if (playerCamera.orthographicSize < zoomAmount)
        {

            playerCamera.orthographicSize += 0.1f;
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

    /*
    void OnTriggerStay2D(Collider2D col)
    {
       
        if (col.tag == "Player")
        {
			vol1 = audioHandler.audio1.volume;
			if (audioHandler.audio1.volume > 0.5f) 
			{
				audioHandler.audio1.volume = 0.2f;
			}
			vol2 = audioHandler.audio3.volume;
			if (audioHandler.audio2.volume > 0.5f) 
			{
				audioHandler.audio2.volume = 0.2f;
			}
			vol3 = audioHandler.audio3.volume;
			if (audioHandler.audio3.volume > 0.5f) 
			{
				audioHandler.audio3.volume = 0.2f;
			}
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
			audioHandler.audio1.volume = vol1;
			audioHandler.audio2.volume = vol2;
			audioHandler.audio3.volume = vol3;

            playerCamControl.enabled = true;
            //playerCamera.orthographicSize = originalZoomAmount;
            StartCoroutine(ReturnZoom());
        }
    }
    */

}
