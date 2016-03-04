using UnityEngine;
using System.Collections;


public class MultiStageSwitch : MonoBehaviour
{	
		SpriteRenderer sr;
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
				sr = this.GetComponent<SpriteRenderer>();
		        mainState = false;
		    }

	    // Update is called once per frame
	    void Update()
	    {
		MultiStageSwitchResponse status0 = (MultiStageSwitchResponse)GameObject.Find("StageSwitch (0)").GetComponent(typeof(MultiStageSwitchResponse));
		MultiStageSwitchResponse status1 = (MultiStageSwitchResponse)GameObject.Find("StageSwitch (1)").GetComponent(typeof(MultiStageSwitchResponse));
		MultiStageSwitchResponse status2 = (MultiStageSwitchResponse)GameObject.Find("StageSwitch (2)").GetComponent(typeof(MultiStageSwitchResponse));

		        state0 = status0.stateOfSwitch;
		        state1 = status1.stateOfSwitch;
		        state2 = status2.stateOfSwitch;

		        if (state0 && state1 && state2)
			        {
			            mainState = true;
			            Debug.Log("All switches actived!");
						sr.color = new Color(0.3f, 0.3f, 1);
			        }
		else{ mainState = false;
				sr.color = new Color(1, 0.3f, 0.3f);
		        //if Switch0 && Switch1 && Switch
		        //mainState = true;
		}
	}


}