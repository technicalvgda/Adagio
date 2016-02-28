using UnityEngine;
using System.Collections;

public class ColorToMatch : MonoBehaviour {

	private SpriteRenderer sr;
	public Color color0, color1, color2;
	Color tempCol;
	Color[] colorArray = new Color[3];
	int index;

	void Start()
	{
		sr = this.GetComponent<SpriteRenderer> ();
		colorArray [0] = color0;
		colorArray [1] = color1;
		colorArray [2] = color2;
		index = Random.Range (0, 2);
		sr.color = colorArray [index];
	}
	public Color getColor()
	{
		return colorArray[index];
	}
}
