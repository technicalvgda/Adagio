using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenAudio
{
    public AudioControl audioHandler;
    public Slider slrMusic, slrSfx;
    public Button btnBack;
    //public FxStreak streak;

    //RectTransform rtMusic, rtSfx;

    //WaitForSeconds waitShort = new WaitForSeconds(.03f);
    //WaitForSeconds wait = new WaitForSeconds(.1f);

    public void InitSettings()
    {
        if (!PlayerPrefs.HasKey("Music"))
            PlayerPrefs.SetFloat("Music", slrMusic.value);
        if (!PlayerPrefs.HasKey("SFX"))
            PlayerPrefs.SetFloat("SFX", slrSfx.value);

        // Set sliders to either their own values (in the editor) or the existing values in PlayerPrefs
        slrMusic.value = PlayerPrefs.GetFloat("Music");
        slrSfx.value = PlayerPrefs.GetFloat("SFX");

        audioHandler.music.volume = slrMusic.value * .1f;
        audioHandler.sfx.volume = slrSfx.value * .1f;

        //rtMusic = slrMusic.GetComponent<RectTransform>();
        //rtSfx = slrSfx.GetComponent<RectTransform>();
    }

    void SaveSettings()
    {
        PlayerPrefs.SetFloat("Music", slrMusic.value);
        PlayerPrefs.SetFloat("SFX", slrSfx.value);
    }

    public void UpdateMusic() { audioHandler.music.volume = slrMusic.value * .1f; }
    public void UpdateSfx() { audioHandler.sfx.volume = slrSfx.value * .1f; }
}
