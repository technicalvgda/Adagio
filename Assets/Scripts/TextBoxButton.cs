using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TextBoxButton : MonoBehaviour 
{
	public GameObject textBox;
		public GameObject TextButton;

		public Text theText;

		public TextAsset textFile;
		public string[] textLines;


		public int currentLine = 0;
		public int endAtLine;

		GameObject player;
		bool playerContact = false;

		private bool isTyping = false;
		private bool cancelTyping = false;
		public float typeSpeed;

	// !for text file, create an empty line for the first line and last line to prevent an array error!
		void Start ()
		{
				textBox.SetActive (false);

				if (textFile != null) //making sure there's text
					{
						textLines = (textFile.text.Split('\n')); //splits text whenever there is a new line
					}
		
				if (endAtLine == 0) // create empty lines at the end so Array error does not occur
					{
					endAtLine = textLines.Length - 1; // last line of text, one less because of array
					}
			}
	
		void OnTriggerStay2D(Collider2D c)
		{
				if(playerContact == true && Input.GetKeyDown(KeyCode.E))
					{
						textBox.SetActive (true); // make text box appear
					}
			}
	
		void OnTriggerEnter2D(Collider2D col)
		{
				if (col.tag == "Player") 
					{
						playerContact = true;
					}
			}
	
		void OnTriggerExit2D(Collider2D col)
		{
				if (col.tag == "Player") 
					{
						playerContact = false;
					}
			}
	
		// Update is called once per frame
		void Update ()
		{
				//theText.text = textLines [currentLine];//gets text from file

				if (playerContact == true && Input.GetKeyDown(KeyCode.E)) //press E for next line
					{
						if (currentLine >= endAtLine) 
							{
								Destroy(TextButton, 0.0F);//make text button disappear prevents array error
							}
			
						if (!isTyping) 
							{
								currentLine += 1; // play next line

								if (currentLine > endAtLine) //after last line
									{ 
										textBox.SetActive (false); // makes text box disappear
									} 
								else 
									{
										StartCoroutine (TextScroll (textLines [currentLine]));
									}
							}
						else if (isTyping && !cancelTyping) 
							{
								cancelTyping = true;
							}
			

					}
		

			}
	
		private IEnumerator TextScroll (string lineOfText) //making words appear word by word
		{
				int letter = 0;
				theText.text = ""; //display nothing on textbox
				isTyping = true;
				cancelTyping = false;
				while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
					{
							theText.text += lineOfText[letter];
							letter += 1; // next letter
							yield return new WaitForSeconds(typeSpeed); // waiting for text to appear
					}
				theText.text = lineOfText; //print whole line of text on screen

				isTyping = false;
				cancelTyping = false;
			}
	
	}