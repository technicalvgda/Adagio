using UnityEngine;
using System.Collections;

public class AdagioAudio : MonoBehaviour
{
    public AudioSource audioBgm;
    public AudioSource[] audioSe;
    public AudioClip[] clipBgm;
    public AudioClip[] clipSe;

    public void SetBgmVolume(float value)
    {
        audioBgm.volume = value;
    }

    public void SetSeVolume(float value)
    {
        for (int i = 0; i < audioSe.Length; i++)
        {
            audioSe[i].volume = value;
        }
    }

    public void PlayBgm(int index, bool loop)
    {
        if (index < 0 || clipBgm.Length == 0 || index >= clipBgm.Length) return;
        if (audioBgm.isPlaying)
        {
            audioBgm.Stop();
        }
        audioBgm.clip = clipBgm[index];
        audioBgm.loop = loop;
        audioBgm.Play();
    }

    public void PlaySe(int index)
    {
        if (index < 0 || clipSe.Length == 0 || index >= clipSe.Length) return;
        for (int i = 0; i < audioSe.Length; i++)
        {
            if (!audioSe[i].isPlaying)
            {
                //audioSe[i].Stop();
                audioSe[i].clip = clipSe[index];
                audioSe[i].Play();
                break;
            }
        }
    }

    public void PlaySe(int index, int source)
    {
        if (index < 0 || clipSe.Length == 0 || index >= clipSe.Length) return;
        audioSe[source].Stop();
        audioSe[source].clip = clipSe[index];
        audioSe[source].Play();
    }

    public void StopBgm()
    {
        audioBgm.Stop();
    }

    public void StopSe()
    {
        for (int i = 0; i < audioSe.Length; i++)
        {
            audioSe[i].Stop();
        }
    }
}
