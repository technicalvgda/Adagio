using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

using UIButton = UnityEngine.UI.Button;

public class AdagioPauseCtrl : MonoBehaviour
{
    [Header("Graphics")]
    public Image imgFade;

    [Header("References")]
    public GameObject btnPause;
    public GameObject objButtonBar;

    [Space]
    public AdagioMenu menuPause;
    AdagioMenu menuCurrent;

    [Space]
    [Tooltip("Build index of the title scene")]
    public int titleIndex = 0;
    [Tooltip("Build index of the loading screen")]
    public int loaderIndex = -1;

    int cd = 0;
    bool paused = false;
    void Update()
    {
        cd--;
        if (cd < 0 && Input.GetKeyDown(KeyCode.Escape) )
        {
            paused = !paused;
            if (paused) Pause();
            else Unpause();
        }
    }

    public void Pause()
    {
        cd = 60;
        StartCoroutine(_Pause() );
    }

    IEnumerator _Pause()
    {
        Time.timeScale = 0;
        yield return null;
        btnPause.SetActive(false);

        objButtonBar.SetActive(true);

        yield return AdagioHelper.WaitForRealSeconds(0.5f);

        menuPause.Enter();
        menuCurrent = menuPause;
    }

    public void Unpause()
    {
        cd = 60;
        StartCoroutine(_Unpause() );
    }

    IEnumerator _Unpause()
    {
        objButtonBar.SetActive(false);
        menuCurrent.Exit();
        menuCurrent = null;

        Time.timeScale = 1;
        yield return null;
        btnPause.SetActive(true);
    }

    public void TransitionToMenu(AdagioMenu menu)
    {
        menuCurrent.Exit();
        menu.Enter();
        menuCurrent = menu;
    }

    public void ReturnToTitle() { if (loaderIndex > 0) StartCoroutine(_ReturnToTitle() ); }
    IEnumerator _ReturnToTitle()
    {
        // a method called ReturnToTitle is not supposed to do this but
        // it isn't like there's a black fade/audio fade anywhere else (lol

        while (imgFade.color.a < 1)
        {
            imgFade.color += new Color(0, 0, 0, 0.05f);
            AudioListener.volume -= 0.05f;
            yield return null;
        }
        imgFade.color = Color.black;

        // Audio listener is global, so reenable it to avoid fucking up other scenes
        AudioListener.volume = 1;

        // Now there's an audible glitch at LoadScene, so mute the audio sources
        // These will just get reset on a scene load
        //audioCtrl.SetBgmVolume(0);
        //audioCtrl.SetSeVolume(0);

        yield return null;

        // Set this to the desired scene's build index; invalid values will default to title
        AdagioLoad.destination = titleIndex;
        // Loading the load scene after this will bring you to the destination scene
        SceneManager.LoadScene(loaderIndex);
    }
}
