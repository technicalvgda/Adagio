using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CodexHandler : MonoBehaviour {

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

    int numberOfCodecies;
	// Use this for initialization
	void Start ()
    {
        //get text component on codex text panel
        //codexText = textPanel.GetComponent<Text>();
        //make sure text panel is inactive
        textPanel.SetActive(false);
       

        //get names of all files in codex folder
        //finds only .txt files, skips all meta files
        foreach (string file in System.IO.Directory.GetFiles("Assets/Resources/Codecies/","*.txt"))
        {
            //count number of codecies created
            numberOfCodecies++;

            //instantiate a new button for this codex
            GameObject button = Instantiate(buttonTemplate) as GameObject;


            //store the button's script
            CodexButton codexScript = button.GetComponent<CodexButton>();
            //set handler in codex script to this object
            codexScript.SetHandler(this);
            //store the name of the file and the codex title in the codeex button
            codexScript.InitializeName(file);
           
            //set parent to scroll view
            //second argument is worldPositionStays
            //setting to false retain local orientation and scale rather than world orientation and scale
            button.transform.SetParent(scrollContent.transform, false);
        }

        //Check playerprefs
        if (!PlayerPrefs.HasKey("Codecies"))
        {
            //create a string of all 0's that is the same size as the number of codecies
            string codexString = new string('0', numberOfCodecies);
            //set the playerpref to have the same data
            PlayerPrefs.SetString("Codecies", codexString );
           
        }
        //retrieve the playerpref data, each index is a 0 if unobtained and 1 if obtained
        codeciesObtained = PlayerPrefs.GetString("Codecies");
        Debug.Log(codeciesObtained);

        //Create all buttons by iterating through string
        for(int j = 0; j < codeciesObtained.Length; j++)
        {
            


            //check if button should be activated
            if (codeciesObtained[j] == 1)
            {
                //activate codex
            }
            else
            {
                //deactivate codex
            }
            //set parent of button to scrollview


         
           
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
        string text = System.IO.File.ReadAllText(codexTextFileName);
      
       
        Debug.Log(text);
        //set codexText to the text contained in text file
        codexText.text = text;
        
    }




}
