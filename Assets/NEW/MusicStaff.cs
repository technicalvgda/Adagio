using UnityEngine;
using System.Collections;

// The staff has reference to the music note. It positions the note according to the
// pitch value (which goes from 0 to 8, for a total of 9 steps). The pitch value
// wraps around when it goes too low or too high.

public class MusicStaff : MonoBehaviour
{
    public GameObject note;
    public int pitch;
    Transform noteTransform;

    void Start()
    {
        noteTransform = note.transform;
        ChangePitch(pitch);
    }

    public void SlideUp()
    {
        ChangePitch(pitch = (pitch + 1) % 8);
    }

    public void SlideDown()
    {
        ChangePitch(--pitch < 0 ? pitch += 9 : pitch);
    }

    void ChangePitch(int pitch)
    {
        noteTransform.localPosition =
            new Vector3(noteTransform.localPosition.x, -0.8f + 0.2f * pitch, 0);
    }
}
