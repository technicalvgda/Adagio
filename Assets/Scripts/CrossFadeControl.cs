using UnityEngine;
using System.Collections;

public class CrossFadeControl : MonoBehaviour {

    public AudioSource audio1, audio2;
    public AudioClip[] musicList;
    public AudioClip startingMusic;
    
    
	// Use this for initialization
	void Start ()
    {
        
        audio1.volume = 1;
        audio2.volume = 0;
        

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    //pass a clip to fade in to
    public void CrossFade(AudioClip newClip)
    {
        //if audio 1 is the source playing currently, fade into 2
        if (audio1.volume == 1)
        {
            //if audio 1 is playing, reassign audio 2 to the new clip
            //audio2.clip = newClip;
            StartCoroutine("ChangeMusic1");
        }
        else
        {
            //if audio 2 is playing, reassign audio 1 to the new clip
            //audio1.clip = newClip;
            StartCoroutine("ChangeMusic2");
        }
    }
    // one fades out, two fades in
    private IEnumerator ChangeMusic1()
    {
        float fTimeCounter = 0f;

        while (!(Mathf.Approximately(fTimeCounter, 1f)))
        {
            fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
            audio1.volume = 1f - fTimeCounter;
            audio2.volume = fTimeCounter;
            yield return new WaitForSeconds(0.02f);
        }

        StopCoroutine("ChangeMusic");
    }
    // two fades out, one fades in
    private IEnumerator ChangeMusic2()
    {
        float fTimeCounter = 0f;

        while (!(Mathf.Approximately(fTimeCounter, 1f)))
        {
            fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
            audio2.volume = 1f - fTimeCounter;
            audio1.volume = fTimeCounter;
            yield return new WaitForSeconds(0.02f);
        }

        StopCoroutine("ChangeMusic");
    }
}
