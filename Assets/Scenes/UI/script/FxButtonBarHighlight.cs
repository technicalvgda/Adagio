using UnityEngine;
using System.Collections;

public class FxButtonBarHighlight : MonoBehaviour
{
    Transform t;
    SpriteRenderer sr;
    void Awake()
    {
        t = this.transform;
        sr = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 scaled = Input.mousePosition / 100.0f;
        t.position = new Vector2(-6.4f + scaled.x, t.position.y);
        sr.color = new Color(1, 1, 1, 2.0f - scaled.y);
    }
}
