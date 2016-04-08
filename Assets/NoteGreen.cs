using UnityEngine;
using System.Collections;

public class NoteGreen : MonoBehaviour {

    private bool goingUp = true;

    public float speed = 1;
    float accumulatedDistance = 0;
    public float travelDistance = 1;

    int count = 0;

    void Start()
    {


    }



    void Update()
    {
        Vector2 currentPos = gameObject.transform.position;


        float movement = speed * Time.deltaTime;

        if (!goingUp)
            transform.Translate(0, -movement, 0);
        else
            //Enemy facing and movement to the right
            transform.Translate(0, movement, 0);



        accumulatedDistance += movement;

        if (accumulatedDistance >= travelDistance)
        {
            if (goingUp)
            {
                goingUp = false;
            }
            else
            {
                goingUp = true;
            }

            accumulatedDistance = 0;
        }
    }
}
