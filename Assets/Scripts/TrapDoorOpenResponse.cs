using UnityEngine;
using System.Collections;
/* Basic script to recieve a button press. 
 * Is attached to the button in the button's inspection window.
 */

public class TrapDoorOpenResponse : MonoBehaviour {

    AudioSource audioSource;

	public bool goUp;
    float initialYPos;
    float endingYPos;
    float movmentValue = 3f;
    void Start()
    {
        if(GetComponent<AudioSource>() != null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        initialYPos = transform.position.y;

		if(goUp)
			endingYPos = initialYPos + movmentValue;
		else
        	endingYPos = initialYPos - movmentValue;
    }
	public void Deactivate()
    {
        if(audioSource != null)
        {
            audioSource.Play();
        }
        
        StartCoroutine(LowerDoor());
		//gameObject.SetActive (false);
	}
    public void Activate()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        StartCoroutine(RaiseDoor());
    }

    private IEnumerator LowerDoor()
    {
		if (goUp) {
			while (transform.position.y < endingYPos) {
				transform.position = new Vector2 (transform.position.x, transform.position.y + 0.02f);
				yield return new WaitForSeconds (0.02f);
			}
		} 
		else 
		{
			while (transform.position.y > endingYPos) {
				transform.position = new Vector2 (transform.position.x, transform.position.y - 0.02f);
				yield return new WaitForSeconds (0.02f);
			}
		}
        yield return null;
    }
    private IEnumerator RaiseDoor()
    {
		if (goUp) {
			while (transform.position.y < initialYPos) {
				transform.position = new Vector2 (transform.position.x, transform.position.y + 0.02f);
				yield return new WaitForSeconds (0.02f);
			}
		} 
		else 
		{
			while (transform.position.y > initialYPos) {
				transform.position = new Vector2 (transform.position.x, transform.position.y - 0.02f);
				yield return new WaitForSeconds (0.02f);
			}
		}
        yield return null;
    }


}	
