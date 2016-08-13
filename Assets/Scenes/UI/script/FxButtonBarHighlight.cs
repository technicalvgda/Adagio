using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FxButtonBarHighlight : MonoBehaviour
{
    Transform t;
    Image img;

    void Awake()
    {
        t = this.transform;
        img = this.GetComponent<Image>();
    }

    void Update()
    {
        //Vector2 scaled = Input.mousePosition / 100.0f;
        t.position = new Vector2(Input.mousePosition.x, t.position.y);
        img.color = new Color(1, 1, 1, 2.0f - Input.mousePosition.y / 100.0f);
    }
}
