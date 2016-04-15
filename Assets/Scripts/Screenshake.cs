using UnityEngine;
using System.Collections;

public class Screenshake : MonoBehaviour
{
	private Vector3 origPos;
	private Quaternion origRot;
	private bool shaking;
	//THE MAGIC NUMBERS
	//Decay: .0002
	//Intesntiy: .015
	//Please do not change these variables. (Design team wants this.)
	//It is only public for testing/reusability.
	public float shake_decay;
	public float shake_intensity;

	void Start(){
		//does nothing when we start
		shaking = false;
	}

	//Press E to trigger, only for testing, can be changed for other stuff like on entry etc.
	void OnTriggerStay2D(Collider2D col){
		if(Input.GetKeyDown(KeyCode.E)){
			shaking = true;
			Shake ();
		}
	}

	void Update(){
		//We have some shake left in us
		if(shake_intensity > 0 && shaking){
			//How much of a shake we are at
			Camera.main.transform.position = origPos + Random.insideUnitSphere*shake_intensity;
			Camera.main.transform.rotation = new Quaternion(
				origRot.x + Random.Range(-shake_intensity, shake_intensity) * 2f,
				origRot.y + Random.Range(-shake_intensity, shake_intensity) * 2f,
				origRot.z + Random.Range(-shake_intensity, shake_intensity) * 2f,
				origRot.w + Random.Range(-shake_intensity, shake_intensity) * 2f);
			//lessening the shake over time
			shake_intensity -= shake_decay;
			if(shake_intensity <= 0){
				shaking = false;
			}
		}
	}

	void Shake(){
		//captures the starting position
		origPos = Camera.main.transform.position;
		origRot = Camera.main.transform.rotation;
	}
	
}