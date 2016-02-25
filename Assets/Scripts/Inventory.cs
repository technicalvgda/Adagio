using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
    int ItemCount=0;
    
    public void addItem(GameObject item)
    {
        ItemCount++;
        item.SetActive(false);
    }
	void Update () {
	    if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(ItemCount);
        }
	}
}
