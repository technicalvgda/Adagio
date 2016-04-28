using UnityEngine;
using System.Collections;

public class FinalMovingPlatform : MonoBehaviour {
    public GameObject sphere, leftwall, rightwall;
    Vector3 start;
    Vector3 end;
    public int value;
    public float speed = 0;
    bool x = true;
    int counter = 0;
    public bool collide = false;
    public bool firsthit = true;
    public bool sphase = false;
    public Vector3 startpoint;

    // Use this for initialization
    void Start () {
        sphere.GetComponent<Renderer>().enabled = false;
        sphere.gameObject.SetActive(false);
        
	}
	
	// Update is called once per frame
	void Update () {
       
            if (counter == 0)
            {
                startpoint = this.transform.position;
                start = new Vector3(leftwall.transform.position.x + 3.5f, this.transform.position.y, this.transform.position.z);
                end = new Vector3(rightwall.transform.position.x - 3.5f, this.transform.position.y, this.transform.position.z);

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
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player" )
        {
            if (col.gameObject.transform.position.y > this.transform.position.y && firsthit && speed > 0)
            {
                if (sphase == false)
                {
                    collide = true;
                }
                else if (sphase == true && sphere.GetComponent<FinalPuzzleSphere>().collide == true)
                {
                    collide = true;
                }
                speed = 0;
                firsthit = false;

            }
        }
    }
    public void activateSphere()
    {
        sphere.gameObject.SetActive(true);
        sphere.GetComponent<Renderer>().enabled = true;
    }
    public void movingSphere()
    {
        sphere.GetComponent<FinalPuzzleSphere>().isMoving = true;
    }
    public void resetPlatform()
    {
        this.transform.position = startpoint;
    }
}
