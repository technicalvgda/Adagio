using UnityEngine;
using System.Collections;

public class AdagioMenu : AdagioUIElement
{
    public float initialDelay;
    public AdagioSelectable[] selectables;
    protected WaitForSeconds waitDelay;
    protected WaitForSeconds waitOffset = new WaitForSeconds(0.2f);
    
    void Awake()
    {
        waitDelay = new WaitForSeconds(initialDelay);
    }

    public override void Enter()
    {
        base.Enter(); // self should be enabled first
        StartCoroutine(_SelectablesFadeIn());
    }

    public override void Exit()
    {
        for (int i = 0; i < selectables.Length; i++)
            selectables[i].Exit();
        base.Exit(); // self should be disabled last
    }

    protected virtual IEnumerator _SelectablesFadeIn()
    {
        yield return waitDelay;

        for (int i = 0; i < selectables.Length; i++)
        {
            selectables[i].Enter();
            yield return waitOffset;
        }

        // This is supposed to enable the button after it has completely settled,
        // because doing it early will break the fade-in animation (due to it sharing
        // the same layer with Highlighted)
        // This is no longer necessary as the fade-in is moved to its own layer.
        // Huge props to Unity for having the layer Weight default to 0, so nothing appears to play
        //for (int i = 0; i < 50; i++)
        //    yield return null;

        // This is now done in the objects' animation clips.
        //SetAllInteractable(true);
    }

    protected void SetAllInteractable(bool arg)
    {
        for (int i = 0; i < selectables.Length; i++)
            selectables[i].interactable = arg;
    }
}
