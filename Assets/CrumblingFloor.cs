using UnityEngine;
using System.Collections;

public class CrumblingFloor : MonoBehaviour {

    public float fallDelay = 0.5f;
    public float destroyDelay = 3f;
    public Screenshake shakeScript;

    private Rigidbody2D rb2d;

    void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (shakeScript.shaking) {
            Invoke("Fall", fallDelay);
            Destroy(gameObject, destroyDelay);
        }
    }

    void Fall() {
        rb2d.isKinematic = false;
    }
}
