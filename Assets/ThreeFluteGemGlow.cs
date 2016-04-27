using UnityEngine;
using System.Collections;

public class ThreeFluteGemGlow : MonoBehaviour
{
    Behaviour halo;         //Access the halo effect, AKA Glowing effect
    bool stepped = false;   //A boolean to know if we are in contact with the object
    public GameObject button;
    public int value = 0;

    // Use this for initialization
    void Start()
    {
        halo = (Behaviour)GetComponent("Halo");
    }

    // Update is called once per frame
    void Update()
    {
        if (value == button.GetComponent<ThreeFluteButton>().value) //If the player steps on the object and presses E, the glow will stop
        {
            //halo = (Behaviour)GetComponent("Halo");
            halo.enabled = true;
        }
        else
        {
            halo.enabled = false;
        }
    }

}
