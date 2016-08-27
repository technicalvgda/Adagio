using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    // From another scene: MUST set AdagioLoad.destination to a valid scene ID, then load this scene
    public static int destination = -1;

    //Assets for the different loading bar objects
    public GameObject LoadingBarFilling,Symbols, SymbolsBackground,textBox;
	//the text
	private TextAsset text;
	//The actual image of the bar, used to manipulate the alpha
	private Image barImage;
	/*private*/public bool loadScene = false;
	//the object used to get the progress of the loading
	private AsyncOperation async = null;
	//set true/false to either increase or decrease the alpha of the loading bar
	private bool increaseAlpha;
	//[SerializeField]
	private int scene;
	//[SerializeField]
	private Text loadingText;
	void Start()
	{
		//get the image component
		barImage = LoadingBarFilling.GetComponent<Image> ();
		//set the X scale of the bar to 0
		barImage.rectTransform.localScale = new Vector3 (0, barImage.rectTransform.localScale.y, barImage.rectTransform.localScale.z);
		textBox.SetActive (false);
		increaseAlpha = false;

	}

	// Updates once per frame
	void Update()
	{
		if (loadScene == false)
		{
			loadScene = true;
			//start the loading
			StartCoroutine(LoadNewScene());
		}
		if (loadScene == true)
		{
		}
	}
	IEnumerator LoadNewScene()
	{
		 if (destination < 0 ||
            destination >= SceneManager.sceneCountInBuildSettings ||
            destination == SceneManager.GetActiveScene().buildIndex)
        {
            destination = 0;
        }

        async = SceneManager.LoadSceneAsync(destination);
		//make it so when the data is loaded the scene does not activate(meaning it will stay in the loading scene)
		async.allowSceneActivation = false;
        destination = -1;
		//while loading is not done
		while (!async.isDone) {

			//Debug.Log(async.progress);
			//make the bar longer as loading progresses
			barImage.rectTransform.localScale = new Vector3 (0.5293753f * (async.progress+0.1f), barImage.rectTransform.localScale.y, barImage.rectTransform.localScale.z);
			// Loading completed
			if (async.progress == 0.9f) 
			{
				//begin fading in/out of the bar for a more dynamic look
				LoadingBarFilling.GetComponent<LoadingBarFadeInOut> ().beginFading ();
				//set it so when the player presses any button the level will load
				if (Input.anyKeyDown)
					async.allowSceneActivation = true;
				//activate the text box so tell the player to press any button to continue
				textBox.SetActive (true);
			}
			yield return null;
		}
	}
}