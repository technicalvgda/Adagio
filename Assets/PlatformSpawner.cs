using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour {

    public int maxNumberPlatforms = 4;
    public GameObject platform;         //A platform prefab
    public GameObject platformSwitch = GameObject.Find("platformSwitch");   //A platform swtich prefab


    public GameObject[] platformArray;          //Stores each platform so we can keep track of them
    public GameObject[] platformSwitchArray;    //Stores each switch

    public float horizontalMin = 1f;
    public float horizontalMax = -1f;
    public float verticalMin = 1f;
    public float verticalMax = 2f;

    private Vector2 originPosition;

	// Use this for initialization
	void Start () {
        platformArray = new GameObject[maxNumberPlatforms + 1];
        platformSwitchArray = new GameObject[maxNumberPlatforms + 1];

        //platform = GameObject.Find("platform");
        //platformSwitch = GameObject.Find("platformSwitch");
        PlatformSwitchDetection detect = platformSwitch.GetComponent<PlatformSwitchDetection>();


        for (int i = 0; i < maxNumberPlatforms + 1; i++) {
            platformArray[i] = platform;
            platformSwitchArray[i] = platformSwitch;
        }
        
        originPosition = transform.position;
        Instantiate(platformArray[0], originPosition, Quaternion.identity);
        Vector2 switchPosition = new Vector2(originPosition.x, originPosition.y - 1);
        Instantiate(platformSwitch, switchPosition, Quaternion.identity);
        Spawn();
	}
	
	// Update is called once per frame
	void Update () {
        int count = 0;
        for (int i = 0; i < maxNumberPlatforms + 1; i++)
        {
            PlatformSwitchDetection detect = platformSwitchArray[i].GetComponent<PlatformSwitchDetection>();
            if (detect) {
                count++;
            }
        }
        if (count == 5) {
            Instantiate(platform, new Vector2(0, 0), Quaternion.identity);
        }
    }

    void Spawn() {
        for (int i = 1; i < maxNumberPlatforms + 1; i++) {
            //Creates a new position based on the origin position using the ranges defined
            Vector2 randomPosition = originPosition + new Vector2(Random.Range(horizontalMin, horizontalMax), Random.Range(verticalMin, verticalMax)); 
            Instantiate(platformArray[i], randomPosition, Quaternion.identity); //Spawns the platform at the randomPosition
            Vector2 switchPosition = new Vector2(randomPosition.x, randomPosition.y - 1);
            Instantiate(platformSwitch, switchPosition, Quaternion.identity);
            originPosition = randomPosition;                            //Sets the originPosition to the randomPosition as reference for the next platform to spawn
        }
    }
}
