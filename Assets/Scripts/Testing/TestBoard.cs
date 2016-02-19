using UnityEngine;
using System.Collections;

public class TestBoard : MonoBehaviour {
    public GameObject playerPrefab;
    public GameObject floorPrefab;
    public GameObject switchPrefab;
    
	void Start () {
	    for(int i=-10; i<10; i++)
        {
            Instantiate(floorPrefab, new Vector3(i, -1, 0), Quaternion.identity);
        }
        Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(switchPrefab, new Vector3(3, 0, 0), Quaternion.identity);
    }
}
