using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {
    public Light flashingLight;
    private float randNum;

	// Use this for initialization
	void Start () {
        flashingLight.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        randNum = Random.value;
        if (randNum <= 0.95f)
        {
            flashingLight.enabled = true;
        }
        else
        {
            flashingLight.enabled = false;
        }
	}
}
