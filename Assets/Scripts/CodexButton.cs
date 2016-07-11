using UnityEngine;
using System.Collections;

public class CodexButton : MonoBehaviour {


    private CodexHandler codexHandler;
	private string nameToDisplay;
    private string nameOfFile;

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
    }
    public void SendContents()
    {
        //check if anyone is subscribed to the event, if not, skip this

        //load filename stored on this button to text handler
        codexHandler.LoadContents(nameOfFile);
    }
}
