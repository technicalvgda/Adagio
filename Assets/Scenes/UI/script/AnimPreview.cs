using UnityEngine;
using System.Collections;

public class AnimPreview : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    public void Startup()
    {
        anim.Play("animPreviewStencil", 0);
        anim.Play("animPreviewArray", 1);
    }

    public void Shutdown()
    {
        anim.Play("animPreviewFadeOut", 0);
        //anim.Play("Blank", 0);
        anim.Play("Blank", 1);
    }
}
