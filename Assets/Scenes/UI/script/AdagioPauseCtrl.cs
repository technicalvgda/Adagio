using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

using UIButton = UnityEngine.UI.Button;

public class AdagioPauseCtrl : MonoBehaviour
{
    [Header("Graphics")]
    public SpriteRenderer spriteFade;

    [Header("References")]
    public GameObject btnPause;
    public GameObject objScaleGroup;
    public Transform objButtonBar;
    Camera cam;

    [Header("First menu")]
    public AdagioMenu menuPause;
    AdagioMenu menuCurrent;

    [Space]
    [Tooltip("Build index of the title scene")]
    public int titleIndex = 0;
    [Tooltip("Build index of the loading screen")]
    public int loaderIndex = -1;

    WaitForSeconds delay05s = new WaitForSeconds(0.5f);

    void Awake()
    {
        cam = FindObjectOfType<Camera>();
    }
    
    int cd = 0;
    bool paused = false;
    void Update()
    {
        cd--;
        if (cd < 0 && Input.GetKeyDown(KeyCode.Escape) )
        {
            paused = !paused;
            cd = 60;
            if (paused) Pause();
            else Unpause();
        }
    }

    public void Pause() { StartCoroutine(_Pause() ); }
    IEnumerator _Pause()
    {
        btnPause.SetActive(false);

        Transform t = objScaleGroup.transform;
        float xy = cam.orthographicSize / 3.6f;
        t.localScale = new Vector3(xy, xy, 1);

        objScaleGroup.SetActive(true);

        for (int i = 0; i < 20; i++)
        {
            objButtonBar.localPosition = new Vector3(0, Mathf.Lerp(-4f, -3.11f, i / 20f), 0);
            yield return null;
        }

        menuPause.Enter();
        menuCurrent = menuPause;

        //yield return null;
        yield return new WaitForSeconds(4);
        Time.timeScale = 0;
    }

    public void Unpause() { StartCoroutine(_Unpause() ); }
    IEnumerator _Unpause()
    {
        Time.timeScale = 1;

        yield return null;

        btnPause.SetActive(true);
        objScaleGroup.SetActive(false);
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

        while (spriteFade.color.a < 1)
        {
            spriteFade.color += new Color(0, 0, 0, 0.05f);
            AudioListener.volume -= 0.05f;
            yield return null;
        }
        spriteFade.color = Color.black;

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
