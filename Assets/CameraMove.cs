﻿using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{
    public enum state
    {
        pantoward,
        panback,
        panwait,
        notpanning
    }
    public float panTime = 0.75f;
    public float stayTime = 2f;
    public GameObject target;
	public GameObject CanvasObject;
	public GameObject TextBoxManager;
    state panState = state.notpanning;
    Transform player;
    Vector3 camPos;
    Vector3 currentVelocity = Vector3.zero;
    public bool finished = false;


    void OnTriggerStay2D(Collider2D c)
    {
      
        
            if (panState == state.notpanning)
            {
                if (target != null)
                {
                    panState = state.pantoward;
                    player = c.transform;
                    camPos = Camera.main.transform.position;
                    player.GetComponent<PlayerController>().enabled = false;
                    Camera.main.GetComponent<CameraController>().enabled = false;
                }
            }
        
    }
    void LateUpdate()
    {
        if (target != null)
        {
            if (panState == state.pantoward)
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                camPos = Vector3.SmoothDamp(camPos, new Vector3(target.transform.position.x, target.transform.position.y, camPos.z), ref currentVelocity, panTime);
                Camera.main.transform.position = camPos;
                Debug.Log(currentVelocity);
				Invoke ("startDialog", 1.5f);

                //if (Vector2.Distance(camPos, target.transform.position) < 0.1f && Input.GetKeyDown(KeyCode.E))
				if (Vector2.Distance(camPos, target.transform.position) < 0.1f && finished)
				{
                    
                    Invoke("startPanBack", stayTime);
                    panState = state.panwait;
                    finished = true;
				
                }

            }
            else if (panState == state.panback)
            {
                camPos = Vector3.SmoothDamp(camPos, new Vector3(player.position.x, player.position.y, camPos.z), ref currentVelocity, panTime);
                Camera.main.transform.position = camPos;
                if (Vector2.Distance(camPos, player.position) < 0.1f)
                {
                    Camera.main.GetComponent<CameraController>().enabled = true;
                    panState = state.notpanning;
                }
            }
        }
    }
    void startPanBack()
    {
        player.GetComponent<PlayerController>().enabled = true;
        panState = state.panback;
    }

	void startDialog() {
		CanvasObject.SetActive (true);
		TextBoxManager.SetActive (true);
	}
}