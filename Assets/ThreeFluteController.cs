using UnityEngine;
using System.Collections;

public class ThreeFluteController : MonoBehaviour
{
    public GameObject button1, button2, button3, door;
    int[] sequence = { 2, 1, 3 };
    bool stepped = false;

    void Update()
    {
        if (stepped && Input.GetKeyDown(KeyCode.E)) {
            if (sequence[0] == button1.GetComponent<ThreeFluteButton>().value && sequence[1] == button2.GetComponent<ThreeFluteButton>().value
                   && sequence[2] == button3.GetComponent<ThreeFluteButton>().value)
            {
                GameObject.Destroy(door);
            }
            
            button1.GetComponent<ThreeFluteButton>().value = 0;
            button2.GetComponent<ThreeFluteButton>().value = 0;
            button3.GetComponent<ThreeFluteButton>().value = 0;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" )
        {
            stepped = true;
            
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            stepped = false;

        }
    }


}
