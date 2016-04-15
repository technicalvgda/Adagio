using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TextBox : MonoBehaviour
{
    public GameObject textBox;
    public GameObject TextButton;

    public Text theText;


    GameObject player;
    bool playerContact = false;



    // !for text file, create an empty line for the first line and last line to prevent an array error!
    void Start()
    {
        textBox.SetActive(false);

    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (playerContact == true)
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
        //theText.text = textLines [currentLine];//gets text from file

        if (TextButton.GetComponent<CameraMove>().finished) //press E for next line
        {

            textBox.SetActive(false); // makes text box disappear
            TextButton.GetComponent<BoxCollider2D>().enabled = false;
        }


    }

   

}