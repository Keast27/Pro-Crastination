using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Each interactable object (with text) needs 2 colliders
/// A normal one and one with istrigger checked
/// The istrigger one should be larger than the normal one
/// And is the range from where you can "talk" with the object
/// </summary>
public class Talkable : MonoBehaviour, IInteractable
{
    public DialogueScript script;
    //public Dialogue dialogue;
    //public bool notStarted = true;

    private void Start()
    {
    }

    // Starts conversation
    public void Interact(InputAction.CallbackContext context)
    {
        script.StartDialogue();
    }

    // Must be within range to start conversation
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Prevents bug that constantly restarts conversation
        //if (Input.GetKeyDown(KeyCode.Space) && notStarted)
        //{
        //    notStarted = false;
        //    TriggerDialogue();
        //}
        //if (FindObjectOfType<DialogueManager>().endText)
        //{
        //    notStarted = true;
        //}
    }
}