using UnityEngine;
using System.Collections;

public class ColorToMatch : MonoBehaviour {

	private SpriteRenderer sr;
	public Color color0, color1, color2;
	Color[] colorArray = new Color[3];
	int index;

	void Start()
	{
		colorArray [0] = color0;
		colorArray [1] = color1;
		colorArray [2] = color2;
		sr = this.GetComponent<SpriteRenderer> ();
		index = Random.Range (0, 2);
		sr.color = colorArray [index];
	}
	public Color getColorArrayIndex(int i)
	{
		return colorArray[i];
	}
	public int getIndex(){
		return index;
	}
	//public Color[] getArray()
	//{
	//	return colorArray;
	//}
}
