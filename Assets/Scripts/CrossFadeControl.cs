using UnityEngine;
using System.Collections;

public class CrossFadeControl : MonoBehaviour {

    public AudioSource audio1, audio2, audio3;
    public AudioClip level2p1, level2p2, level2p3, level3p1, level3p2, level3p3;


    int levelsComplete = 0;
    public int levelToActivate;
    // Use this for initialization
    void Start ()
    {
        
        audio1.volume = 1;
        audio2.volume = 0;
        audio3.volume = 0;


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
        if(levelsComplete == 1)
        {
            StartLevelTwo();
        }
        if (levelsComplete == 2)
        {
            StartLevelThree();
        }
    }
    public void StartLevelTwo()
    {

        audio1.clip = level2p1;
        StartCoroutine(ChangeMusicNextLevel(level2p2, level2p3));
       

    }
    public void StartLevelThree()
    {

        audio1.clip = level3p1;
        StartCoroutine(ChangeMusicNextLevel(level3p2, level3p3));
        
    }

    public void CrossFadeOneTwo()
    {
       
        StartCoroutine("ChangeMusicOneAndTwo");
       
    }

    public void CrossFadeTwoThree()
    {

        StartCoroutine("ChangeMusicTwoAndThree");

    }
    // one fades out, two fades in
    private IEnumerator ChangeMusicOneAndTwo()
    {
        AudioSource fadeOut = null, fadeIn = null;
        if (audio1.volume > 0)
        {
            fadeOut = audio1;
            fadeIn = audio2;
        }
        else if (audio2.volume > 0)
        {
            fadeOut = audio2;
            fadeIn = audio1;
        }
        else//if both sources are silent (this shouldnt happen)
        { StopCoroutine("ChangeMusicOneAndTwo"); }

        float fTimeCounter = 0f;
       
        while (!(Mathf.Approximately(fTimeCounter, 1f)))
        {
            fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
            fadeOut.volume = 1f - fTimeCounter;
            fadeIn.volume = fTimeCounter;
            yield return new WaitForSeconds(0.02f);
        }

        StopCoroutine("ChangeMusicOneAndTwo");
    }
    // one fades out, two fades in
    private IEnumerator ChangeMusicTwoAndThree()
    {
        AudioSource fadeOut = null,fadeIn = null;
        if (audio2.volume > 0 )
        {
            fadeOut = audio2;
            fadeIn = audio3;
        }
        else if (audio3.volume > 0)
        {
            fadeOut = audio3;
            fadeIn = audio2;
        }
        else//if both sources are silent (this shouldnt happen)
        {StopCoroutine("ChangeMusicTwoAndThree");}

        float fTimeCounter = 0f;

        while (!(Mathf.Approximately(fTimeCounter, 1f)))
        {
            fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
            fadeOut.volume = 1f - fTimeCounter;
            fadeIn.volume = fTimeCounter;
            yield return new WaitForSeconds(0.02f);
        }

        StopCoroutine("ChangeMusicTwoAndThree");
    }
    ///fades three to one
    private IEnumerator ChangeMusicNextLevel(AudioClip clip2, AudioClip clip3)
    {
        AudioSource fadeOut = null, fadeIn = null;
       //set audio 1 to new music
        fadeOut = audio3;
        fadeIn = audio1;
      
        //do fade
        float fTimeCounter = 0f;

        while (!(Mathf.Approximately(fTimeCounter, 1f)))
        {
            fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
            fadeOut.volume = 1f - fTimeCounter;
            fadeIn.volume = fTimeCounter;
            yield return new WaitForSeconds(0.02f);
        }
        audio2.clip = clip2;
        audio3.clip = clip3;
        StopCoroutine("ChangeMusicNextLevel");
    }

}
