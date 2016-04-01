using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    /*private*/public bool loadScene = false;

    //[SerializeField]
    private int scene;
    //[SerializeField]
    private Text loadingText;


    // Updates once per frame
    void Update()
    {
        if (loadScene == false)
        {
            loadScene = true;
            StartCoroutine(LoadNewScene());
        }
        if (loadScene == true)
        {
        }
    }
    IEnumerator LoadNewScene()
    {
        //yield return new WaitForSeconds(3);
        AsyncOperation async = SceneManager.LoadSceneAsync(3);
        while (!async.isDone)
        {
            yield return null;
        }

    }

}