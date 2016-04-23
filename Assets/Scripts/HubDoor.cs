using UnityEngine;
using System.Collections;
/* Basic script to recieve a button press. 
 * Is attached to the button in the button's inspection window.
 */

public class HubDoor : MonoBehaviour
{

    float initialYPos;
    float endingYPos;
    float movmentValue = 3f;
    void Start()
    {
        Deactivate();
        initialYPos = transform.position.y;
        endingYPos = initialYPos - movmentValue;
    }
    public void Deactivate()
    {
        StartCoroutine(LowerDoor());
        //gameObject.SetActive (false);
    }
    public void Activate()
    {
        StartCoroutine(RaiseDoor());
    }

    private IEnumerator LowerDoor()
    {
        while (transform.position.y > endingYPos)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
    private IEnumerator RaiseDoor()
    {
        while (transform.position.y < initialYPos)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }


}
