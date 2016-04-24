using UnityEngine;
using System.Collections;

public class ThreeFluteButton : MonoBehaviour
{
    public int value = 0;
    bool firstContact = true;

	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") == true && firstContact == true)
        {
            firstContact = false;
            value++;

            if (value > 3)
                value = 1;
            Debug.Log(value);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        firstContact = true;
    }


}
