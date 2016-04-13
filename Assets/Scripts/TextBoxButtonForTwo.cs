using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Text;
using System.IO;

/// <summary>
/// HOW TO USE THIS CODE
/// 
/// 1. Specify the two characters talking.
/// 2. Format the text file so it looks like this 
///         NPC:Hello Zimri.@
///         Zimri:Hello stranger.
/// 2a. Name the person saying the line, then end the line with @
///     Unless the line is at the very end of the textfile, then do NOT include the @
/// 2b. DO NOT double up speakers one right after the other. This will fuck shit up.
///     i.e.  Zimri: blahblah@
///           Zimri: blasdasd@
///           NPC: You fucked up.@
/// 3. Thats all.
/// 
/// 
/// </summary>
public class TextBoxButtonForTwo : MonoBehaviour
{
    public GameObject textBox;
    public GameObject TextButton;
    public string speaker1, speaker2;
    public Text theTextPlayer;
    public Text theTextNPC;
    string[] filespeaker1;
    string[] filespeaker2;
    public TextAsset textFile;
    string[] textLines;
    //May the gods bless these Lists, for the List provides all that is good.
    List<string> speakerLines1 = new List<string>();
    List<string> speakerLines2 = new List<string>();
    int speaker;
    int endAtLine;
    int currentLine;

    bool playerContact = false;
    bool spoken1= false, spoken2 = false;
    private bool isTyping = false;
    private bool cancelTyping = false;
    public float typeSpeed;

    void Start()
    {
        print(textFile.text);
        
        speaker = 0; //the first speaker
        textBox.SetActive(false);
        //the delimiter is currently the speaker's name with an @ after the line
        //i.e. Zimri: words words@

        if (textFile != null) //making sure there's text
        {  
            textLines = textFile.text.Split('@'); //splits text whenever the delimiter is mentioned
        }
        //Separating all the lines into their speakers
        for (int i = 0; i < textLines.Length; i++)
        {
            if (textLines[i].Contains(speaker1+":"))
            {
                filespeaker1 = textLines[i].Split(':');
                speakerLines1.Add(filespeaker1[1]);
                Array.Clear(filespeaker1, 0, 2);
            }
            else {
                filespeaker2 = textLines[i].Split(':');
                speakerLines2.Add(filespeaker2[1]);
                Array.Clear(filespeaker2, 0, 2);
            }
        }
        //getting how many lines we should speak
        if (speakerLines1.Count > speakerLines2.Count)
        {
            endAtLine = speakerLines2.Count;
            Debug.Log(speakerLines2.Count);
        }
        else {
            endAtLine = speakerLines1.Count;
            Debug.Log(speakerLines1.Count);
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
        if (playerContact == true && Input.GetKeyDown(KeyCode.E)) //press E for next line
        {
           
            if (!isTyping)
            {
                switch (speaker)
                {
                    //First Speaker is talking
                    case 0:
                        theTextPlayer.color = Color.blue;
                        StartCoroutine(TextScrollPlayer(speakerLines1[currentLine]));
                        speaker = 1;
                        spoken1 = true;
                        break;
                    //Second Speaker is talking
                    case 1: 
                        theTextNPC.color = Color.green;
                        StartCoroutine(TextScrollNPC(speakerLines2[currentLine]));
                        speaker = 0;
                        spoken2 = true;
                        break;
                    //Everyone's done
                    case 3:
                        textBox.SetActive(false);
                        break;
                    default:
                        Debug.Log("Default");
                        break;                   
                }
                //This block fixes the problem of one character having one mroe line that the other, throwing off the current line
                //count and shit so that the textbox would never properly quit
                //This is saying if we have maxed out the lines, and one person has spoken, but the other has not, we can still finish
                //Bless this if statement
                if(((spoken1 && !spoken2) || (!spoken1 && spoken2)) && currentLine >= endAtLine)
                {
                    speaker = 3;
                }
                //Both people have spoken, so move onto the next lines.
                if (spoken1 && spoken2)
                {
                    currentLine++;
                    spoken1 = spoken2 = false;
                    //We have reached the end of the lines
                    if (currentLine >= endAtLine)
                    {
                        speaker = 3;
                    }
                }
                
                
            }
            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }

    private IEnumerator TextScrollPlayer(string lineOfText) //making words appear word by word
    {
        int letter = 0;
        theTextPlayer.text = ""; //display nothing on textbox
        isTyping = true;
        cancelTyping = false;

        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            theTextPlayer.text += lineOfText[letter];
            letter += 1; // next letter
            yield return new WaitForSeconds(typeSpeed); // waiting for text to appear
        }
        theTextPlayer.text = lineOfText; //print whole line of text on screen

        isTyping = false;
        cancelTyping = false;
    }

    private IEnumerator TextScrollNPC(string lineOfText) //making words appear word by word
    {
        int letter = 0;
        theTextNPC.text = ""; //display nothing on textbox
        isTyping = true;
        cancelTyping = false;

        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            theTextNPC.text += lineOfText[letter];
            letter += 1; // next letter
            yield return new WaitForSeconds(typeSpeed); // waiting for text to appear
        }
        theTextNPC.text = lineOfText; //print whole line of text on screen

        isTyping = false;
        cancelTyping = false;
    }
}