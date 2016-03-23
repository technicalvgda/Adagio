using UnityEngine;
using System.Collections;

public class CodexHandlerScript : MonoBehaviour {

    public GameObject[] codexList;
	// Use this for initialization
	void Start ()
    {
	    foreach(GameObject codex in codexList)
        {
            string codexPref = codex.gameObject.name;
            //if the playerpref for this codex doesnt exist, create it
            if(!PlayerPrefs.HasKey(codexPref))
            {
                PlayerPrefs.SetInt(codexPref, 0);
            }
            else
            {
                if(PlayerPrefs.GetInt(codexPref) == 1)
                {
                    codex.SetActive(true);
                }
            }
           
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
