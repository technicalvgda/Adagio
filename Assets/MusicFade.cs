using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class MusicFade : MonoBehaviour
{

    public AudioSource track1;
    public AudioSource track2;
    public float audio1Volume = 1.0f;
    public float audio2Volume = 0.0f;
    public bool track2Playing  = false;

    void Start() {
        track1.Play();

       
    }
 
 void Update()
    {
        fadeOut();

        if (audio1Volume <= 0.1)
        {
            if (track2Playing == false)
            {
                track2Playing = true;
                
                track2.Play();
            }

            fadeIn();
        }
    }


    void fadeIn()
    {
        if (audio2Volume < 1)
        {
            audio2Volume += 0.1f * Time.deltaTime;
            track2.volume = audio2Volume;
        }
    }

    void fadeOut()
    {
        if (audio1Volume > 0.1)
        {
            audio1Volume -= 0.1f * Time.deltaTime;
            track1.volume = audio1Volume;
        }
    }
}