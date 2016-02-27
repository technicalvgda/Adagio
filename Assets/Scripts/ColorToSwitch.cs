using UnityEngine;
using System.Collections;


public class ColorToSwitch : MonoBehaviour {
	SpriteRenderer sr;
	ColorToMatch colSM;
	Color c1,c2,c3;
	Color[] colorArraySwitch = new Color[3];

	void Start () {
		//colorArraySwitch = colSM.getArray ();
		colorArraySwitch[0] = new Color(colSM.color0.r,colSM.color0.g,colSM.color0.b);
		colorArraySwitch [1] = new Color(colSM.color1.r,colSM.color1.g,colSM.color1.b);
		colorArraySwitch [2] = new Color(colSM.color2.r,colSM.color2.g,colSM.color2.b);
		/*c1 = colSM.getColorArrayIndex(0);
		c2 = colSM.getColorArrayIndex (1);
		c3 = colSM.getColorArrayIndex (2);
		colorArraySwitch [0] = c1;
		colorArraySwitch [1] = c2;
		colorArraySwitch [2] = c3;
		sr = this.GetComponent<SpriteRenderer>();*/
	}
	
	public void Cswitch(int state)
	{
		sr.color = colorArraySwitch [state];
	}

}
