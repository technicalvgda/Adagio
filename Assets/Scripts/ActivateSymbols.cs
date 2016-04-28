using UnityEngine;
using System.Collections;

public class ActivateSymbols : MonoBehaviour {

    int levelsComplete = 0;
    public int levelToActivate;
    SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start ()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
	}
    void Update()
    {
        if(levelsComplete >= levelToActivate)
        {
            spriteRenderer.enabled = true;
        }
    }
	
	void OnDisable()
    {
        Teleporter.OnTeleport -= IncreaseLevels;
    }
    void OnEnable()
    {
        Teleporter.OnTeleport += IncreaseLevels;
    }
    void IncreaseLevels()
    {
        levelsComplete++;
    }
    
}
