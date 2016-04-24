using UnityEngine;
using System.Collections;

public class ThreeFluteController : MonoBehaviour
{
    public GameObject button1, button2, button3, door;
    int[] sequence = { 2, 1, 3 };
 

    void Update()
    {
        
            if (sequence[0] == button1.GetComponent<ThreeFluteButton>().value && sequence[1] == button2.GetComponent<ThreeFluteButton>().value
                   && sequence[2] == button3.GetComponent<ThreeFluteButton>().value)
            {
                GameObject.Destroy(door);
            }
            
          
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" )
        {
           
            
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            

        }
    }


}
