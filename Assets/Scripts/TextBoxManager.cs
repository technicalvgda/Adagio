using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{
    /** 
    For a text box to simulate a conversation between two characters.
    
    Things that need to be specified: 
        - names of the first and second speaker.
        - file that contains the dialogue.
    
    In the text file, format should look something like:
        Zimri: Hello.
        NPC: I must go.
        Good bye!
        Zimri: Have a nice day.
    If a speaker has more than one line, you can include the speaker's name in each 
        line after the first, but it is not necessary.
    See sampledialogue.txt for an example.
    **/

    public GameObject textBox;

    public Text thedialogue;

    public TextAsset dialogue;
    public string[] dialoguelines;

    int currentLine = 0;
    int endAtLine;
    public string firstCharacter;
    public string secondCharacter;
    bool firstSpeaker = true;

    // Use this for initialization
    void Start()
    {
        //Each line in the text file stored into dialogueLines.
        if (dialogue != null)
        {
            dialoguelines = (dialogue.text.Split('\n'));
        }

        if (endAtLine == 0)
        {
            endAtLine = dialoguelines.Length - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Detects a change in speaker.
        if (dialoguelines[currentLine].Contains(firstCharacter))
        {
            firstSpeaker = true;
        }

        if (dialoguelines[currentLine].Contains(secondCharacter))
        {
            firstSpeaker = false;
        }

        //Text color changes depending on speaker.
        if (firstSpeaker == true)
        {
            thedialogue.color = Color.red;
        }
        else
        {
            thedialogue.color = Color.blue;
        }

        //Display line.
        thedialogue.text = dialoguelines[currentLine];

        //Press U for next line.
        if (Input.GetKeyDown(KeyCode.U))
        {
            currentLine += 1;
        }

        //Text box disappears after all lines have been spoken.
        if (currentLine > endAtLine)
        {
            textBox.SetActive(false);
        }


    }
}
