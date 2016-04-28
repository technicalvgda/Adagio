using UnityEngine;
using System.Collections;

public class AnimStorm : MonoBehaviour
{
    public AdagioAudio audioCtrl;

    public void StormCrash()
    {
        audioCtrl.PlaySe(0, 0);
    }

    public void StormAmbience()
    {
        audioCtrl.PlaySe(1, 1);
    }
}
