using UnityEngine;
using System.Collections;

public class MultiStageSwitch : MonoBehaviour
{


    public bool mainState;


    public GameObject Switch0;
    public GameObject Switch1;
    public GameObject Switch2;

    bool state0;
    bool state1;
    bool state2;

    // Use this for initialization
    void Start()
    {
        mainState = false;
    }

    // Update is called once per frame
    void Update()
    {
        StateSwitch status0 = (StateSwitch)GameObject.Find("StageSwitch (0)").GetComponent(typeof(StateSwitch));
        StateSwitch status1 = (StateSwitch)GameObject.Find("StageSwitch (1)").GetComponent(typeof(StateSwitch));
        StateSwitch status2 = (StateSwitch)GameObject.Find("StageSwitch (2)").GetComponent(typeof(StateSwitch));

        state0 = status0.stateOfSwitch;
        state1 = status1.stateOfSwitch;
        state2 = status2.stateOfSwitch;

        if (state0 && state1 && state2)
        {
            mainState = true;
            Debug.Log("All switched actived!");
        }
        else mainState = false;
        //if Switch0 && Switch1 && Switch
        //mainState = true;
    }
}
