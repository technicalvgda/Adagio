using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public GameObject textBox;

	public Text theText;

	public TextAsset textFile;
	public string[] textLines;

	public int currentLine = 0;
	//when the script ends
	public int endAtLine;

	public PlayerController player;

	private bool isTyping = false;
	private bool cancelTyping = false;

	public float typeSpeed;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
		if (textFile != null) {
			textLines = (textFile.text.Split ('\n'));
		}

		if (endAtLine == 0) {
			endAtLine = textLines.Length - 1;
		}

		theText.text = textLines [currentLine];
		StartCoroutine (TextScroll(textLines[currentLine]));
	}

	void Update() {

		//Press enter to read each sentence.
		if (Input.GetKeyDown(KeyCode.Return)) {
			if (!isTyping) {
				
				currentLine += 1;

				if (currentLine > endAtLine) {
					textBox.SetActive (false);
					GameObject button = GameObject.Find ("ButtonScene");
					if (button != null) {
						CameraMove cameraMove = button.GetComponent<CameraMove> ();
						cameraMove.finished = true;
					}
				} 
				else 
				{
					StartCoroutine (TextScroll(textLines[currentLine]));
				}
			} 

			else if (isTyping && !cancelTyping) {
				cancelTyping = true;
			}
		}


	}

	private IEnumerator TextScroll (string lineOfText) {
		int letter = 0;
		theText.text = "";
		isTyping = true;
		cancelTyping = false;
		while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1)) {

			theText.text += lineOfText[letter];
			letter += 1;
			yield return new WaitForSeconds(typeSpeed);
		}
		theText.text = lineOfText;
		isTyping = false;
		cancelTyping = false;
	}
}
