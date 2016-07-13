using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CodexButton : MonoBehaviour {


    private CodexHandler codexHandler;
	private string nameToDisplay;
    private string nameOfFile;

    Text nameText;

    public void Awake()
    {
        nameText = GetComponentInChildren<Text>();
    }
    public void SetHandler(CodexHandler handler)
    {
        codexHandler = handler;
    }
    public void InitializeName(string fileName)
    {
        //store full filename for retrieval
        nameOfFile = fileName;
        //split filename into 2 parts (number, title, extension)
        string[] splitFileName = fileName.Split('.');
        //store title as name to display
        nameToDisplay = splitFileName[1];
        Debug.Log(fileName + ", "+ nameToDisplay);
        //set the text label for this codex to its title
        nameText.text = nameToDisplay;


    }
    public void SendContents()
    {
        //check if anyone is subscribed to the event, if not, skip this

        //load filename stored on this button to text handler
        codexHandler.LoadContents(nameOfFile);
    }
}
