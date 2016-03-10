using UnityEngine;
using System.Collections;

public class SimonSaysButton : MonoBehaviour
{
    public GameObject button;

    public bool pressed = false;
    public bool collide = false;
    //GameObject pressureplate1 = GameObject.FindGameObjectWithTag("pressureplate1");
    void Start()
    {
       

    }
    void Update()
    {
        if (collide && Input.GetKeyDown(KeyCode.E))
        {
            pressed = true;
            Debug.Log("ON");

        }
    }
   
    void OnTriggerEnter2D(Collider2D col)
    {
        
        Debug.Log("Enter");
        
        collide = true;
        Debug.Log(collide);
        

    }
    void OnTriggerExit2D(Collider2D col)
    {
        collide = false;
        pressed = false;
        Debug.Log("Exit");
    }
    
}