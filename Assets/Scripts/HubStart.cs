using UnityEngine;
using System.Collections;

public class HubStart : MonoBehaviour {

    public GameObject audioHandler;
    CrossFadeControl audioScript;
    public GameObject hubDoor;
    TrapDoorOpenResponse doorScript;
    bool hasTouched = false;

    void Start()
    {
        audioScript = audioHandler.GetComponent<CrossFadeControl>();
        doorScript = hubDoor.GetComponent<TrapDoorOpenResponse>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && hasTouched == false)
        {
            hasTouched = true;
            audioScript.audio1.Play();
            audioScript.audio2.Play();
            audioScript.audio3.Play();
            doorScript.Activate();
        }
    }
}
