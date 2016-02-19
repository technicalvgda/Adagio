using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
    public float rotateSpeed = 180f;
    void OnCollisionEnter2D(Collision2D c)
    {
        c.transform.GetComponent<ItemCounter>().itemCount++;
        Debug.Log("Item found");
        Destroy(this.gameObject);
    }
	void Update () {
        transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
	}
}
