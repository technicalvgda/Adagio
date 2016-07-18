using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CodexHandler : MonoBehaviour {

    //store text for each codex
    Dictionary<string, string> textDictionary = new Dictionary<string, string>();
    private List<TextAsset> codexList;
    /*
    //EVENTS
    public delegate void LoadCodex(string message);

    // Event declaration
    public event LoadCodex OnLoad;
    */

    //PlayerPref data stored in string
    string codeciesObtained;

    //Objects for text display
    public GameObject textPanel;
    public Text codexText;

    public GameObject scrollContent;
    public GameObject buttonTemplate;

    //the total number of codecies available in game (the number in resources folder)
    private int maxCodecies;
    

    //the number counted
    int numberOfCodecies;

    // Use this for initialization
    void Start ()
    {
        maxCodecies = CodexPrep.maxCodecies;


        //get text component on codex text panel
        //codexText = textPanel.GetComponent<Text>();
        //make sure text panel is inactive
        textPanel.SetActive(false);


        
        //retrieve the playerpref data, each index is a 0 if unobtained and 1 if obtained
        codeciesObtained = PlayerPrefs.GetString("Codecies");
        Debug.Log(codeciesObtained);


        //get names of all files in codex folder
        //finds only .txt files, skips all meta files
        foreach (TextAsset codex in Resources.LoadAll("Codecies"))//foreach (string file in System.IO.Directory.GetFiles("Assets/Resources/Codecies/","*.txt"))
        {
            Debug.Log(codex.name);
            //count number of codecies created
            numberOfCodecies++;

            //instantiate a new button for this codex
            GameObject button = Instantiate(buttonTemplate) as GameObject;


            //store the button's script
            CodexButton codexScript = button.GetComponent<CodexButton>();
            //set handler in codex script to this object
            codexScript.SetHandler(this);
            //store the name of the file and the codex title in the codeex button
            codexScript.InitializeName(codex.name);//file);
            //create new entry
            textDictionary.Add(codex.name, codex.text);
            //set parent to scroll view
            //second argument is worldPositionStays
            //setting to false retain local orientation and scale rather than world orientation and scale
            button.transform.SetParent(scrollContent.transform, false);

            //check if codex is obtained
            //subtract 1 so number of codecies matches index (indecies start at 0)
            if (codeciesObtained[numberOfCodecies -1] == '0')
            {
                //set button to inactive
                codexScript.Uncollected();
            }         
        }       
    }
	
	// Update is called once per frame
	void Update ()
    {
       //temp code
       if(Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToTitle();
        }
	}
    public void CloseText()
    {
        textPanel.SetActive(false);
    }

    public void ReturnToTitle()
    {
        // Load the title screen
        SceneManager.LoadScene(0);
    }
    public void LoadContents(string codexTextFileName)
    {
        //set text panel to true
        textPanel.SetActive(true);

        Debug.Log("Retrieving Text from: " + codexTextFileName);
        //Load Text file named codexTextFileName
        //TextAsset textFile = Resources.Load(codexTextFileName) as TextAsset;

        //get text from file specified
        string text = textDictionary[codexTextFileName];
       // string text = System.IO.File.ReadAllText(codexTextFileName);
      
       
        Debug.Log(text);
        //set codexText to the text contained in text file
        codexText.text = text;
        
    }
 
   



}
