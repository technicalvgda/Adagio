using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

// To change what happens when a slider knob moves, modify the Setting's Apply() method
public class AdagioSettings : MonoBehaviour
{
    [Tooltip("-3 .. 3")]
    public int defaultImage = 0;
    [Tooltip("0 .. 10")]
    public int defaultBgm = 7, defaultSe = 7;
    public Slider sliderImage, sliderBgm, sliderSe;
    public AdagioAudio audioCtrl;

    Setting settingImage, settingBgm, settingSe;

    public void Awake()
    {
        //Nuke();

        settingImage = new SettingImage("IMG", defaultImage, sliderImage);
        settingBgm = new SettingBgm("BGM", defaultBgm, sliderBgm, audioCtrl);
        settingSe = new SettingSe("SE", defaultSe, sliderSe, audioCtrl);

        settingImage.Init();
        settingBgm.Init();
        settingSe.Init();
    }

    void Nuke()
    {
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.DeleteKey("IMG");
        PlayerPrefs.DeleteKey("BGM");
        PlayerPrefs.DeleteKey("SE");
    }

    public void ResetImage() { settingImage.Reset(); }
    public void ResetBgm() { settingBgm.Reset(); }
    public void ResetSe() { settingSe.Reset(); }

    public void ApplyImage() { settingImage.Apply(); }
    public void ApplyBgm() { settingBgm.Apply(); }
    public void ApplySe() { settingSe.Apply(); }

    public void SaveImage() { settingImage.Save(); }
    public void SaveBgm() { settingBgm.Save(); }
    public void SaveSe() { settingSe.Save(); }

    abstract class Setting
    {
        protected string key;
        protected int initial;
        protected Slider slider;

        protected Setting(string key, int initial, Slider slider)
        {
            this.key = key;
            this.initial = initial;
            this.slider = slider;
        }

        public void Init()
        {
            // If value at key doesn't exist, set it
            if (!PlayerPrefs.HasKey(key))
                PlayerPrefs.SetFloat(key, initial);

            //print(key + ": " + PlayerPrefs.GetFloat(key));

            // Get the value at key, which is either the value in inspector or an existing value
            slider.value = PlayerPrefs.GetFloat(key);
            Apply();
        }
        public void Reset() { slider.value = initial; Apply(); }
        public abstract void Apply();
        public void Save() { PlayerPrefs.SetFloat(key, slider.value); }
    }

    class SettingImage : Setting
    {
        public SettingImage(string key, int initial, Slider slider)
            : base(key, initial, slider)
        { }

        public override void Apply()
        {
            float gamma = 1 + slider.value * .08f;
            RenderSettings.ambientLight = new Color(gamma, gamma, gamma, 1);
        }
    }

    class SettingBgm : Setting
    {
        AdagioAudio audioCtrl;
        public SettingBgm(string key, int initial, Slider slider, AdagioAudio audioCtrl)
            : base(key, initial, slider)
        { this.audioCtrl = audioCtrl; }

        public override void Apply() { audioCtrl.SetBgmVolume(slider.value * 0.1f); }
    }

    class SettingSe : Setting
    {
        AdagioAudio audioCtrl;
        public SettingSe(string key, int initial, Slider slider, AdagioAudio audioCtrl)
            : base(key, initial, slider)
        { this.audioCtrl = audioCtrl; }

        public override void Apply() { audioCtrl.SetSeVolume(slider.value * 0.1f); }
    }
}
