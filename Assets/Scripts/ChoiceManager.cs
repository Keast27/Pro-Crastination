using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public GameObject choiceBox; // Box for Question + choices

    public Choice[] choiceButtons = new Choice[5];
    private static DialogueChoice[] choices = new DialogueChoice[5];
    private static GameObject s_gameObject;

    private void Awake()
    {
        s_gameObject = gameObject;
        s_gameObject.SetActive(false);
    }

    public static void ShowChoices(DialogueChoice[] _choices)
    {
        choices = _choices;
        s_gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        for (int i = 0; i < 5; i++)
        {
            if (choices[i] == null)
                break;

            choiceButtons[i].ChoiceText = choices[i].choice;
            choiceButtons[i].button.onClick.AddListener(choices[i].ChoiceSelected);
            choiceButtons[i].gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < 5; i++)
        {
            if (choices[i] == null)
                break;

            choiceButtons[i].gameObject.SetActive(false);
        }
    }
}
