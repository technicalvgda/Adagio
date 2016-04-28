using UnityEngine;
using System.Collections;

public class FinalPuzzleSphere : MonoBehaviour {
    public GameObject leftwall, rightwall;
    Vector3 start;
    Vector3 end;
    public float speed = .05f;
    bool x = true;
    int counter = 0;
    public bool collide = false;
    public bool isMoving = false;
    
    

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (counter == 0)
            {
                
                start = new Vector3(leftwall.transform.position.x - 3.5f, this.transform.position.y, this.transform.position.z);
                end = new Vector3(rightwall.transform.position.x + 3.5f, this.transform.position.y, this.transform.position.z);

            }
            if (this.transform.position == end)
            {
                x = false;
            }
            if (this.transform.position == start)
            {
                x = true;

            }
            if (this.transform.position == end || x == false)
            {
                transform.position = Vector3.MoveTowards(this.transform.position, start, speed);
                counter += 1;
            }
            else if (this.transform.position != end)
            {
                transform.position = Vector3.MoveTowards(this.transform.position, end, speed);
                counter += 1;
            }
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        collide = true;
        
        
    }
    void OnTriggerExit2D(Collider2D col)
    {
        collide = false;
    }
    
}
