using UnityEngine;
using System.Collections;

public class JumpPad : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.up, ForceMode2D.Force);

        }
    }
}
