using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Choice", menuName = "Dialogue/Choice Asset")]
public class DialogueChoice : ScriptableObject
{
    UnityAction onChoiceSelected;
    public string choice;
    [SerializeField] Dialogue dialogue;
    [SerializeField] UnityEvent interaction;

    private void OnDisable()
    {
        if (dialogue == null)
            onChoiceSelected += () => DialogueManager.SetDialogue(dialogue);
    }

    public void ChoiceSelected()
    {
        onChoiceSelected?.Invoke();
        interaction?.Invoke();
    }
}
