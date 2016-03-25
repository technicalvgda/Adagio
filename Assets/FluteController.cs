using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FluteController : MonoBehaviour {
	public Flute flute1, flute2, flute3, flute4, flute5;
	List<Flute> fluteArray = new List<Flute>();
	Flute temp;
	int randStart, index;
	private Flute[] solution = new Flute[3];
	public List<Flute> wipSolution = new List<Flute>();
	private SpriteRenderer sr;
	private Color solutionColor;
	// Use this for initialization
	void Start () {
        index = -1;
		sr = this.GetComponent<SpriteRenderer>();
        fluteArray.Add(flute1);
        fluteArray.Add(flute2);
        fluteArray.Add(flute3);
        fluteArray.Add(flute4);
        fluteArray.Add(flute5);

        //Randomizing the flute array to make a randomized solution out of
        for (int i = 4; i >= 0; i--){
			randStart = Random.Range (0, i);
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
		solutionColor = solutionColor/3;
		sr.color = solutionColor;
       
    }

	void Update () {
        //if the flute is active, our progress array does not contain the currently pressed button, and our array is not maxed out
        //add our currently pressed button
        //if our array is maxed out, then ignores this add
        //if we press a button for a fltue in the array, we remove it from the progress array, allowing us to add a new flute
        index++;
        if (index > 5)
        {
            index = 0;
        }
        if (fluteArray[index].state == true && !wipSolution.Contains(fluteArray[index]) && wipSolution.Count < 3)
        {
            wipSolution.Add(fluteArray[index]);
            Debug.Log("Added to progress");
        }
        if (index <= wipSolution.Count && wipSolution[index].state == false)
        {
            wipSolution.RemoveAt(index);
            Debug.Log("removed from progress");
            index--;
        }
        //all of the correct flutes have been pressed
        if (solution[0].state == true && solution[1].state == true && solution[2].state == true)
        {
            Debug.Log("Flute solved");
        }

    }
    

}
