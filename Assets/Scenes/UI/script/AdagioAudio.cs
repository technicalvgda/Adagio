using UnityEngine;
using System.Collections;

public class AdagioAudio : MonoBehaviour
{
    public AudioSource audioBgm, audioSe;
    public AudioClip[] clipBgm;
    public AudioClip[] clipSe;

    public void SetBgmVolume(float value)
    {
        audioBgm.volume = value;
    }

    public void SetSeVolume(float value)
    {
        audioSe.volume = value;
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
        if (audioSe.isPlaying)
        {
            audioSe.Stop();
        }
        audioSe.clip = clipSe[index];
        audioSe.Play();
    }

    public void StopBgm()
    {
        audioBgm.Stop();
    }

    public void StopSe()
    {
        audioSe.Stop();
    }
}
