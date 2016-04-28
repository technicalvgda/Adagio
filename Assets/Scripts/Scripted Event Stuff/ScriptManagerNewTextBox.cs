using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Text;
using System.IO;

/**
 * TODO:
 * Discriminate text lines based on name, then add it into the list.
 * 
 * if (line.contains name) then its delimination to the list? 
 * original code is overtly complicated
 **/

/// <summary>
/// HOW TO USE THIS CODE
/// 
/// UPDATE:
/// Format is the same. now supports multiple people speaking in different orders. etc
/// </summary>
public class ScriptManagerNewTextBox : MonoBehaviour
{
	public GameObject textBox;
	public GameObject TextButton;
	public string speaker1, speaker2, speaker3, speaker4;
	public Color ZimriColor,GraceColor,OrderColor,SplendorColor;
	public Text theTextPlayer, theTextNPC1, theTextNPC2, theTextNPC3;
	//temp string holder
	string[] filespeaker;
	Text[] boxArray; 
	public TextAsset textFile;
	public string[] textLines;
	//May the gods bless these Lists, for the List provides all that is good.
	public List<string> speakerLines = new List<string>();
	//List<string> speakerLines2 = new List<string>();
	int speaker;
	public int endAtLine;
	public int currentLine = 0;

	bool playerContact = false;
	//bool spoken1= false, spoken2 = false;
	private bool isTyping = false;
	private bool cancelTyping = false;
	public float typeSpeed;
	public bool startText;
	public bool startInitialText;
	void Start()
	{
		boxArray = new Text[]{theTextPlayer, theTextNPC1, theTextNPC2, theTextNPC3};
		/*
        print(textFile.text);
		boxArray = new Text[]{theTextPlayer, theTextNPC1, theTextNPC2, theTextNPC3};
        //speaker = 0; //the first speaker
        textBox.SetActive(false);
        //the delimiter is currently the speaker's name with an @ after the line
        //i.e. Zimri: words words@

		//the IF and FOR will grab and seperate all the text
        if (textFile != null) //making sure there's text
        {  
            textLines = textFile.text.Split('@'); //splits text whenever the delimiter is mentioned
        }
        //
        for (int i = 0; i < textLines.Length; i++)
        {
			speakerLines.Add(textLines[i]);
        }
		endAtLine = speakerLines.Count;
		//textBox.SetActive (true);
		//startInitialText = true;*/
	}
	public void setTextAndPrepareForOutput(TextAsset t)
	{
		speakerLines.Clear ();
		textFile = t;
		print(textFile.text);
		textLines = new string[0];
		boxArray = new Text[]{theTextPlayer, theTextNPC1, theTextNPC2, theTextNPC3};
		//speaker = 0; //the first speaker
		textBox.SetActive(false);
		//the delimiter is currently the speaker's name with an @ after the line
		//i.e. Zimri: words words@

		//the IF and FOR will grab and seperate all the text
		if (textFile != null) //making sure there's text
		{  

			textLines = textFile.text.Split('@'); //splits text whenever the delimiter is mentioned
		}
		//
		for (int i = 0; i < textLines.Length; i++)
		{
			speakerLines.Add(textLines[i]);
		}
		endAtLine = speakerLines.Count;
		textBox.SetActive (true);
		startInitialText = true;
	}
	void OnTriggerStay2D(Collider2D c)
	{
		if (playerContact == true && Input.GetKeyDown(KeyCode.E))
		{
			textBox.SetActive(true); // make text box appear
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
	void Update()
	{
		if ((playerContact == true && Input.GetKey (KeyCode.E)) || (startText == true))
		{			Debug.Log ("HERE");
			if (startInitialText == true || Input.GetKey (KeyCode.E)) 
			{
				Debug.Log ("HERE2");
				if (!isTyping) {
					Debug.Log ("ISTYPING");
					//			filespeaker = speakerLines[currentLine].Split(':');
					//Clears the other textboxes
					boxArray [0].text = "";
					boxArray [1].text = "";
					boxArray [2].text = "";
					boxArray [3].text = "";
					if (currentLine < endAtLine) {
						Debug.Log ("HERE3");
						filespeaker = speakerLines [currentLine].Split (':');
						if (speakerLines [currentLine].Contains (speaker1 + ":")) {
							Debug.Log ("HERE ZIMRI");
							theTextPlayer.color = ZimriColor;
							speaker = 0;
							currentLine++;
						} else if (speakerLines [currentLine].Contains (speaker2 + ":")) {
							Debug.Log ("HERE GRACE");
							theTextNPC1.color = GraceColor;
							speaker = 1;
							currentLine++;
						} else if (speakerLines [currentLine].Contains (speaker3 + ":")) {
							Debug.Log ("HERE ORDER");
							theTextNPC2.color = OrderColor;
							speaker = 2;
							currentLine++;
						} else if (speakerLines [currentLine].Contains (speaker4 + ":")) {
							Debug.Log ("HERE SPLENDOR");
							theTextNPC3.color = SplendorColor;
							speaker = 3;
							currentLine++;
						}
						StartCoroutine (TextScroll (filespeaker [1], speaker));
						Array.Clear (filespeaker, 0, 2); //clears our delimiter array from origin to the end
					} 
					else 
					{
						Debug.Log ("CLEANUP");
						startText = false;
						textFile = null;
						currentLine = 0;
						endAtLine = 0;
						textLines = new string[0];
						speakerLines.Clear ();
					}
					startInitialText = false;
				}
			}
		}
	}

	private IEnumerator TextScroll(string lineOfText, int speakercode) //making words appear word by word
	{	
		//speaker index: 
		//0 Zimri
		//1 NPC1
		//2 NPC2
		//3 NPC3
		int letter = 0;
		boxArray[speakercode].text = ""; //display nothing on textbox
		isTyping = true;
		cancelTyping = false;

		while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
		{
			boxArray[speakercode].text += lineOfText[letter];
			letter += 1; // next letter
			yield return new WaitForSeconds(typeSpeed); // waiting for text to appear
		}
		boxArray[speakercode].text = lineOfText; //print whole line of text on screen
		isTyping = false;
		cancelTyping = false;
	}
}