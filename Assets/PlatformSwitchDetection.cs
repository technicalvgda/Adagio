using UnityEngine;
using System.Collections;

public class PlatformSwitchDetection : MonoBehaviour {

    public bool switchActive = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (switchActive) {
            Debug.Log("WAHOOOOOOOOOOOOO");
        }
	}

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("platform")) {
            Activate();
        }
    }

    void Activate() {
        switchActive = true;
    }
}
