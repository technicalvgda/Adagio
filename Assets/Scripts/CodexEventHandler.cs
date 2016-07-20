using UnityEngine;
using System.Collections;

public class CodexEventHandler : MonoBehaviour {

    public GameObject codexCanvas;

    public void ActivateCodexCanvax(string text)
    {
        codexCanvas.SetActive(true);
        codexCanvas.GetComponent<CodexDisplayCanvas>().SetText(text);
    }
}
