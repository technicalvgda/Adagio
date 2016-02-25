using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    public float rotateSpeed = 180f;
    void OnTriggerEnter2D(Collider2D c)
    {
        c.transform.GetComponent<Inventory>().addItem(gameObject);
        Debug.Log("Item found");
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
    }
}
