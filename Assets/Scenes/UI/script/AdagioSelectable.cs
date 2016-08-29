using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Button is used
//using UIButton = UnityEngine.UI.Button;

public class AdagioSelectable : AdagioUIElement
{
    //Transform t;
    Selectable selectable;
    
    public bool interactable { set { selectable.interactable = value; } }

    // Start() is called before the first Update() call (LATE)
    // Awake() is called as part of the instantiation process (EARLY)

    void Awake()
    {
        //t = this.transform;
        selectable = this.GetComponent<Selectable>();
    }

    public override void Init()
    {
        selectable.interactable = false;
        //this.GetComponent<Animator>().enabled = true;
        //this.GetComponent<Animator>().Play("Fade In");
    }

    public override void Enter()
    {
        base.Enter();
        Init();
    }
}
