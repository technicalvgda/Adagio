using UnityEngine;
using System.Collections;

public class ItemCounter : MonoBehaviour {
    public int itemCount = 0;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("You have " + itemCount + " items.");
        }
    }
}
