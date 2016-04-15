using UnityEngine;
using System.Collections;

public class ParticleColorChanger : MonoBehaviour {
    
	// Use this for initialization
	void Start ()
    {
        ///example call
        ///Function can be called with any color placed in arguments
        ChangeColor(Color.green);
	}
	
	// Update is called once per frame
	void Update ()
    {
	   
	}
    public void ChangeColor(Color color)
    {
        this.GetComponent<ParticleSystem>().startColor = color;
    }
}
