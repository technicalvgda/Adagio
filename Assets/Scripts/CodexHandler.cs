using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CodexHandler : MonoBehaviour {


    //EVENTS
    public delegate void LoadCodex(string message);

    // Event declaration
    public event LoadCodex OnLoad;

    //PlayerPref data stored in string
    string codeciesObtained;

    //Objects for text display
    public GameObject textPanel;
    Text codexText;

    public GameObject buttonTemplate;

    int numberOfCodecies;
	// Use this for initialization
	void Start ()
    {
        //get text component on codex text panel
        codexText = textPanel.GetComponent<Text>();
        //make sure text panel is inactive
        textPanel.SetActive(false);
       

        //get names of all files in codex folder
        //finds only .txt files, skips all meta files
        foreach (string file in System.IO.Directory.GetFiles("Assets/Resources/Codecies/","*.txt"))
        {
            //count number of codecies created
            numberOfCodecies++;
            //print name of file
            Debug.Log(file);
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
            //instantiate button
            GameObject button = (GameObject)Instantiate(buttonTemplate);
            //set parent of button to scrollview


            //set handler in codex script to this object
            button.GetComponent<CodexButton>().SetHandler(this);
            //check if button should be activated
            if(codeciesObtained[j] == 1)
            {
                //activate codex
            }
            else
            {
                //deactivate codex
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadContents(string codexTextFileName)
    {
        //Load Text file named codexTextFileName
        TextAsset textFile = Resources.Load(codexTextFileName) as TextAsset;
       
      
        //set text panel to true
        textPanel.SetActive(true);
        //set codexText to the text contained in text file
        codexText.text = textFile.text;
        
    }




}
