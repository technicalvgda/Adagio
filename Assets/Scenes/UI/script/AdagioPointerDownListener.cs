using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System;

// Makes an object respond as soon as the mouse button is down
// Attach this to the UI buttons, move everything from OnClick() into it

public class AdagioPointerDownListener : MonoBehaviour, IPointerDownHandler
{
    // This will be set in the Animation timeline in addition to UI.Button.interactable
    // This script detects the click, while UI.Button.interactable detects the mouse over

    // When clicking Add Property/Add Curve in the Animation timeline, Unity will
    // only let you select the TOPMOST script in the inspector because fuck you.
    // Script is anything with (Script) after its name. In this case, you will be
    // competing with "Button (Script)" and unable to edit both of them at the same time.

    // Bump the script to be animated all the way to the top, make changes to it,
    // then bump it back down. Repeat for every script that needs to be animated.
    // ALL the modified values will actually be saved, even when it doesn't explicitly
    // let you do it.

    // Boolean is animatable
    // http://docs.unity3d.com/Manual/animeditor-AnimationCurves.html

    public bool interactable;
    public UnityEvent onPointerDown;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (interactable)
            onPointerDown.Invoke();
    }
}

/*
//-- How to make the new UnityEvent part of the UI.Button component itself --
// The whole process feels wacky so it's left out in favor of the safer solution above,
// but it should work

// http://docs.unity3d.com/ScriptReference/SerializeField.html
// http://stackoverflow.com/questions/29052183/extending-unity-ui-components-with-custom-inspector
// http://forum.unity3d.com/threads/how-can-i-get-an-event-widget-in-my-own-inspectors.270418/
// http://forum.unity3d.com/threads/touch-and-hold-a-button-on-new-ui.266065/

// AdagioButton.cs: attach to a button
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class AdagioButton : UnityEngine.UI.Button
{
    //[System.Serializable]
    //public class SerializableEvent : UnityEvent { }
    //[SerializeField]
    //public SerializableEvent onPointerDown;

    // This is getting passed to OnInspectorGUI() as a serializable field
    public UnityEvent onPointerDown;

    public override void OnPointerDown(PointerEventData eventData)
    {
        //base.OnPointerDown(eventData);
        print("pointer down");
        onPointerDown.Invoke();
    }
}


// Make a folder anywhere called "Editor"
// Editor/AdagioButtonEditor.cs: don't attach to anything. In the Solution Explorer,
// this is found under ProjectName.CSharp.Editor. This script modifies the inspector
// so that the new UnityEvent shows up

using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;

[CustomEditor(typeof(AdagioButton))]
public class AdagioButtonEditor : UnityEditor.UI.ButtonEditor
{
    public override void OnInspectorGUI()
    {
        //AdagioButton component = (AdagioButton)target;

        // Shows the other UI.Button fields
        base.OnInspectorGUI();

        // Exposes the extra onPointerDown() UnityEvent
        serializedObject.Update();
        SerializedProperty onPointerDown = serializedObject.FindProperty("onPointerDown");
        EditorGUILayout.PropertyField(onPointerDown, true);
        serializedObject.ApplyModifiedProperties();
    }
}
*/
