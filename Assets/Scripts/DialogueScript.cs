using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Script", menuName = "Dialogue/Script")]
public class DialogueScript : ScriptableObject
{
    public string name; // Name of object

    public Dialogue dialogue;
    public DialogueChoice[] choices = new DialogueChoice[5];

    public void StartDialogue()
    {
        SteveController.ActionMap = PlayerActionMap.UI;
        DialogueManager.SetDialogue(dialogue);
    }
}
