using UnityEngine;
using System.Collections;

public class VolSlider : MonoBehaviour
{
    float s = 1.0F;
    AudioListener main;

    void Start()
    {
        main = Camera.main.GetComponent<AudioListener>();
    }

    void Update()
    {
        main.GetComponent<AudioSource>().volume = s;
    }

    void OnGUI()
    {
        s = GUI.HorizontalSlider(new Rect(0, 0, 256, 32), s, 0.0F, 1.0F);
    }
}