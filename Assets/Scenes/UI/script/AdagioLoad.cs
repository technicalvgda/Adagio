using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AdagioLoad : MonoBehaviour
{
    // From another scene: MUST set AdagioLoad.destination to a valid scene ID, then load this scene
    public static int destination = -1;

    public SpriteRenderer spriteFade;
    public Transform spriteLoading;

    IEnumerator Start()
    {
        if (destination < 0 ||
            destination >= SceneManager.sceneCountInBuildSettings ||
            destination == SceneManager.GetActiveScene().buildIndex)
        {
            destination = 0;
        }

        AsyncOperation load = SceneManager.LoadSceneAsync(destination);
        load.allowSceneActivation = false;

        destination = -1;

        // The new scene is swapped in when progress is over 90% (apparently 90% counts as "ready")
        // !allowSceneActivation stalls it at 90% instead and it stays there until the boolean is true
        while (load.progress < 0.9f)
        {
            yield return null;
        }

        // The scene is supposedly 100% loaded (not 90%) when isDone is true;
        // this will never happen if allowSceneActivation is false
        //while (!load.isDone)
        //{
        //    yield return null;
        //}

        // The loading pane only shows up if the load has taken long enough (default: 1.5s)
        // Change this behavior in the attached animation clip

        // Load over, fade out
        this.GetComponent<Animation>().Stop();
        for (int i = 0; i < 10; i++)
        {
            if (spriteFade.color.a < 1)
                spriteFade.color += new Color(0, 0, 0, 0.1f);
            else spriteFade.color = Color.black;

            spriteLoading.localScale -= new Vector3(0.001f, 0.001f, 0);

            yield return null;
        }

        // Setting this to true allows the scene to be switched
        load.allowSceneActivation = true;
    }
}
