using UnityEngine;
using System.Collections;

public class PanSwitch : Switch {
    public enum state
    {
        pantoward,
        panback,
        panwait,
        notpanning
    }
    public float panTime = 0.75f;
    public float stayTime = 5f;
    GameObject target;
    state panState = state.notpanning;
    Transform player;
    Vector3 camPos;
    Vector3 currentVelocity = Vector3.zero;
    
	void OnCollisionEnter2D(Collision2D c) {
	    if (panState == state.notpanning)
        {
            target = GameObject.Find("Item");
            if (target != null)
            {
                panState = state.pantoward;
                player = c.transform;
                camPos = Camera.main.transform.position;
                player.GetComponent<PlayerController>().paused = true;
                Camera.main.GetComponent<CameraController>().enabled = false;
            }
        }
	}
    void Update()
    {
        if (target != null)
        {
            if (panState == state.pantoward)
            {
                camPos = Vector3.SmoothDamp(camPos, new Vector3(target.transform.position.x, target.transform.position.y, camPos.z), ref currentVelocity, panTime);
                Camera.main.transform.position = camPos;
                Debug.Log(currentVelocity);
                if (Vector2.Distance(camPos, target.transform.position) < 0.1f)
                {
                    Invoke("startPanBack", stayTime);
                    panState = state.panwait;
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
        player.GetComponent<PlayerController>().paused = false;
        panState = state.panback;
    }
}
