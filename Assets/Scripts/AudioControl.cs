using UnityEngine;
using System.Collections;

public class AudioControl : MonoBehaviour {

    public AudioSource music, sfx;
    public float musicVolume, sfxVolume;

    public AudioClip[] musicClips;
    public AudioClip[] soundClips;
    
    // Use this for initialization
    void Start () {
        musicVolume = PlayerPrefs.GetFloat("Music");
        sfxVolume = PlayerPrefs.GetFloat("SFX");
        music.volume = musicVolume;
        sfx.volume = sfxVolume;
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void PlayMusic(int i) {
        if (music.isPlaying) {
            music.Stop();
        }
        music.clip = musicClips[i];
        music.Play();

    }

    public void PlaySound(int i) {
        if (sfx.isPlaying) {
            sfx.Stop();
        }
        sfx.clip = soundClips[i];
        sfx.Play();
    }

    public void LoopMusic(bool b) {
        music.loop = b;
    }

    public void StopMusic() {
        music.Stop();
    }

}
