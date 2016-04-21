using UnityEngine;
using System.Collections;

public abstract class AdagioUIElement : MonoBehaviour
{
    public virtual void Init() { }
    public virtual void Enter() { this.gameObject.SetActive(true); }
    public virtual void Exit() { this.gameObject.SetActive(false); }
}
