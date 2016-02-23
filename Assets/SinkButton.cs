using UnityEngine;
using System.Collections;

public class SinkButton : MonoBehaviour {

    public Transform sinker;
    public bool signal = false;
    Vector3 sinkHeight;     //for the signal to be off
    Vector3 sinkLow;        //for the signal to be on
    Vector3 shift;
    Vector3 speed = new Vector3(0.0f, 0.0f, 0.0f);

    public GameObject PlayerObject;
    public LayerMask SwitchLayers;
    public LayerMask PlayerLayer;
    public bool onPlate;
    private Transform m_PlayerGround;


    // Use this for initialization
    void Start () {

        sinkHeight = sinker.position;//this sets the original hight of the plate
        sinkLow = sinker.position;
        sinkLow.y = (sinkHeight.y - 0.3f);//this sets the pressed state of the plate

        

    }

    void FixedUpdate () {


        //signal = false;
        Debug.Log("Y: " + sinker.transform.localPosition.y);
        m_PlayerGround = PlayerObject.transform.FindChild("PlayerGround");//defines the point where
        onPlate = Physics2D.OverlapPoint(m_PlayerGround.position, SwitchLayers);//the collision is detected (player-switch)

        if (onPlate)
        {
            Sink();
        }
        else
        {
            Rise();
        }
        //----
        if ((sinker.transform.localPosition.y) < (-0.1f))
        {
            signal = true;
        }
        else
        {
            signal = false;
            //Debug.Log("Item has been fixed!");
        }
        //----
        if (signal)
        {
            //insert action here
        }
        


	}



    void Sink()
    {
        shift = Vector3.SmoothDamp((sinker.position), new Vector3((this.transform.localPosition.x), (this.transform.localPosition.y - 0.2f), shift.z), ref (speed), 1.0f);
        sinker.position = shift;
    }

    void Rise()
    {
        shift = Vector3.SmoothDamp((sinker.position), new Vector3((this.transform.localPosition.x), (this.transform.localPosition.y + 0.1f), shift.z), ref (speed), 1.0f);
        sinker.position = shift;
    }

    void Filler()
    {
        m_PlayerGround = PlayerObject.transform.FindChild("PlayerGround");//defines the point where
        onPlate = Physics2D.OverlapPoint(m_PlayerGround.position, SwitchLayers);//the collision is detected (player-switch)
    }

}
