using UnityEngine;
using System.Collections;

public class PlatformRotation : MonoBehaviour {

    public float rotationDelay = 2f;
    public float speed = 1;
    public GameObject platform;
    private Rigidbody2D rb2d;
    private HingeJoint2D hinge;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
       
    }

    void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.isKinematic = true;
        hinge = GetComponent<HingeJoint2D>();
        JointAngleLimits2D limits = hinge.limits;
        limits.min = -90;
        limits.max = 90;
        hinge.limits = limits;
        hinge.useLimits = true;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Invoke("Rotate", rotationDelay);
        }
    }

    void Rotate() {
        rb2d.isKinematic = false;
    }
}

