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
public class NewTextBox : MonoBehaviour
{
    public GameObject textBox;
    public GameObject TextButton;
    public string speaker1, speaker2, speaker3, speaker4;
	public Text theTextPlayer, theTextNPC1, theTextNPC2, theTextNPC3;
	//temp string holder
    string[] filespeaker;
	Text[] boxArray; 
    public TextAsset textFile;
    string[] textLines;
    //May the gods bless these Lists, for the List provides all that is good.
    List<string> speakerLines = new List<string>();
    //List<string> speakerLines2 = new List<string>();
    int speaker;
    int endAtLine;
    int currentLine = 0;

    bool playerContact = false;
    //bool spoken1= false, spoken2 = false;
    private bool isTyping = false;
    private bool cancelTyping = false;
    public float typeSpeed;

    void Start()
    {
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
		if (playerContact == true && Input.GetKey(KeyCode.E)){
			if(!isTyping){
				filespeaker = speakerLines[currentLine].Split(':');
				//Clears the other textboxes
				boxArray[0].text = "";
				boxArray[1].text = "";
				boxArray[2].text = "";
				boxArray[3].text = "";
				if(speakerLines[currentLine].Contains(speaker1+":")){
					theTextPlayer.color = Color.white;
					speaker = 0;
					currentLine++;
				}
				else if(speakerLines[currentLine].Contains(speaker2+":")){
					theTextNPC1.color = Color.green;
					speaker = 1;
					currentLine++;
				}
				else if(speakerLines[currentLine].Contains(speaker3+":")){
					theTextNPC2.color = Color.red;
					speaker = 2;
					currentLine++;
				}
				else if(speakerLines[currentLine].Contains(speaker4+":")){
					theTextNPC3.color = Color.black;
					speaker = 3;
					currentLine++;
				}
				StartCoroutine(TextScroll(filespeaker[1], speaker));
				Array.Clear(filespeaker,0,2); //clears our delimiter array from origin to the end

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