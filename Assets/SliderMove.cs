using UnityEngine;
using System.Collections;

public class SliderMove : MonoBehaviour {
    public Transform point;
    Vector3 end;
    Vector3 start;
    private float speed = .05f;
    int counter = 0;
    public GameObject fluteSwitch;
    bool finish;
    public GameObject pp1, pp2, pp3;
    public GameObject door;
	// Use this for initialization
	void Start () {
        finish = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fluteSwitch.GetComponent<FluteSwitch>().pressed == true) 
        {
            if (counter == 0)
            {
                start = new Vector3(point.position.x, point.position.y, point.position.z);
                end = new Vector3(point.position.x + 20.0f, point.position.y, point.position.z);
            }
            if (point.position != end)
            {
                transform.position = Vector3.MoveTowards(point.position, end, speed);
                counter = 1;
            }
            if(point.position == end)
            {
                finish = true;
            }
        }
        if (finish == true && (pp1.GetComponent<PressurePlate>().collide == false ||
               pp2.GetComponent<PressurePlate>().collide == false || pp3.GetComponent<PressurePlate>().collide == false))
        {
            transform.position = start;
            fluteSwitch.GetComponent<FluteSwitch>().pressed = false;
            finish = false;
        }
        if(pp1.GetComponent<PressurePlate>().collide == true &&
               pp2.GetComponent<PressurePlate>().collide == true && pp3.GetComponent<PressurePlate>().collide == true)
        {
            GameObject.Destroy(door);
        }
    }
}
