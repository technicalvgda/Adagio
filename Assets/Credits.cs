﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

    
    public GameObject FinalCredit;
    float timer = 300; //136
    public float speed = 3;

    void Start()
    {
        StartCoroutine(Timer());
    }

    void Update()
    {
        if (FinalCredit.transform.position.y < 0)
        {
            transform.Translate(new Vector3(0, Time.deltaTime * speed, 0));
        }
        else
        {
            StartCoroutine(Restart());
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        
    }
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);
        //load first scene
        SceneManager.LoadScene(0);
    }

   

}

