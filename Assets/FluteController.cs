using UnityEngine;
using System.Collections;

public class FluteController : MonoBehaviour {
	public Flute flute1, flute2, flute3, flute4, flute5;
	Flute[] fluteArray = new Flute[5];
	Flute temp;
	int randStart, stateCount;
	private Flute[] solution = new Flute[3];
	private Flute[] wipSolution = new Flute[3];
	private SpriteRenderer sr;
	private Color solutionColor;
	// Use this for initialization
	void Start () {
		sr = this.GetComponent<SpriteRenderer>();
		fluteArray[0] = flute1;
		fluteArray[1] = flute2;
		fluteArray[2] = flute3;
		fluteArray[3] = flute4;
		fluteArray[4] = flute5;

		//Randomizing the flute array to make a randomized solution out of
		for(int i = 0; i < fluteArray.Length; i++){
			randStart = Random.Range (0, 5);
			temp = fluteArray[randStart];
			fluteArray[randStart] = fluteArray[i];
			fluteArray[i] = temp;
			if(i < 3){
				solution[i] = fluteArray[i];
			}
		}
		//mixing our three colors together
		for(int j = 0; j < solution.Length; j++){
			solutionColor += solution[j].fluteCol;
		}
		solutionColor = solutionColor/solution.Length;
		sr.color = solutionColor;
	}

	void Update () {
		// Limit our choices to three here
		// maybe many if statements, some kind of loop, look into the TOGGLE statement
		if(solution[0].state && solution[1].state && solution[2].state){
			Debug.Log("Flute solved");	
		}
	}

}
