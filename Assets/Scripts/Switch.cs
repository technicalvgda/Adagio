using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D c)
    {
        Debug.Log("Activated");
    }
    void OnCollisionStay2D(Collision2D c)
    {
    }
    void OnCollisionExit2D(Collision2D c)
    {
    }
}
