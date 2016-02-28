using UnityEngine;
using System.Collections;


public class ColorToSwitch : MonoBehaviour {
	SpriteRenderer sr;
	public ColorToMatch colSM;
	Color c1,c2,c3,tempC;
	Color[] colorArraySwitch = new Color[3];
	int randStart;
	void Start () {
		sr = this.GetComponent<SpriteRenderer>();
        //colorArraySwitch = colSM.getArray ();
		colorArraySwitch [0] = new Color (colSM.color0.r, colSM.color0.g, colSM.color0.b);
		colorArraySwitch [1] = new Color (colSM.color1.r, colSM.color1.g, colSM.color1.b);
		colorArraySwitch [2] = new Color (colSM.color2.r, colSM.color2.g, colSM.color2.b);
		//Array is shuffled so we don't always cycle through color the order they are added in.
		//May not seem like it works, but that's only because we have three colors, more colors and it'll show.
		for (int i = 0; i < colorArraySwitch.Length; i++) {
			randStart = Random.Range (0, 2);
			tempC = colorArraySwitch [randStart];
			colorArraySwitch [randStart] = colorArraySwitch [i];
			colorArraySwitch [i] = tempC;
		}

	}

	public void Cswitch(int state)
	{
		//Block starts out white to mask starting color, which may land on the selected color in ColorToMatch.
		if (state == -1) {
			sr.color = new Color (1, 1, 1);
		} else {
			sr.color = colorArraySwitch [state];
		}
	}
	public Color getColor(){
		return sr.color;
	}

}
