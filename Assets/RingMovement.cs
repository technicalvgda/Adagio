using UnityEngine;
using System.Collections;

public class RingMovement : MonoBehaviour {
    private bool facingRight = true;
    public float speed = 8;
    public float distance = 10;
    float accumulatedDistance = 0;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float movement = speed * Time.deltaTime;
        if (!facingRight)
            transform.Translate(-movement, 0, 0);
        else
            //Enemy facing and movement to the right
            transform.Translate(movement, 0, 0);

        accumulatedDistance += movement;

        if (accumulatedDistance > distance || accumulatedDistance < -distance) {
            facingRight = !facingRight;
            accumulatedDistance = 0;
        }
    }
}
