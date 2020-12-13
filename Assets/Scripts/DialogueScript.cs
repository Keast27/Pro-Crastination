using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Dialogue Script", menuName = "Dialogue/Script")]
public class DialogueScript : ScriptableObject
{
    public string speaker; // Name of object

    public Dialogue dialogue;
    public DialogueChoice[] choices = new DialogueChoice[5];

    public bool HasChoices { get { return choices.Length > 0; } }

    public void StartDialogue()
    {
        UIManager.currentScript = this;
        if (dialogue)
            DialogueManager.SetDialogue(dialogue, speaker);
        else if (HasChoices)
            ChoiceManager.ShowChoices(choices);
    }
}
