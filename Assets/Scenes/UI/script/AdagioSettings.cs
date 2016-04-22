using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdagioSettings : MonoBehaviour
{
    public enum SettingsType { IMG, BGM, SE };
    float[] initialValues;
    public Slider[] sliders;
    public AdagioAudio audioCtrl;

    public void Start()
    {
        initialValues = new float[sliders.Length];
        for (int i = 0; i < sliders.Length; i++)
        {
            initialValues[i] = sliders[i].value;
        }
        Init(SettingsType.IMG);
        Init(SettingsType.BGM);
        Init(SettingsType.SE);
    }

    void Init(SettingsType settings)
    {
        string key = settings.ToString();
        int index = (int)settings;

        // If value at key doesn't exist, set it
        //if (!PlayerPrefs.HasKey(key))
            //PlayerPrefs.SetFloat(key, sliders[index].value);
            //print("Saved " + sliders[index].value + " to key "+ key);

        // Get the value at key, which is either the value in editor or an existing value
        //sliders[index].value = PlayerPrefs.GetFloat(key);
    }

    public void Reset(SettingsType settings)
    {
        int index = (int)settings;
        sliders[index].value = initialValues[index];
    }

    public void Save(SettingsType settings)
    {
        string key = settings.ToString();
        int index = (int)settings;

        print("Saved " + sliders[index].value + " to key " + key);
    }

    public void SetImage()
    {
        float gamma = 1 + sliders[(int)SettingsType.IMG].value * .2f;
        RenderSettings.ambientLight = new Color(gamma, gamma, gamma, 1);
    }

    public void SetBgm()
    {
        audioCtrl.SetBgmVolume(sliders[(int)SettingsType.BGM].value);
    }

    public void SetSe()
    {
        audioCtrl.SetSeVolume(sliders[(int)SettingsType.SE].value);
    }
}
