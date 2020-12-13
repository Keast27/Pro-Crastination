using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public GameObject choiceBox; // Box for Question + choices
    private int selectedChoice = 0;
    private int maxChoices;
    private float direction;
    private int SelectedChoice
    {
        get { return selectedChoice; }
        set
        {
            if (selectedChoice == value)
                return;

            selectedChoice = value;

            if (selectedChoice == maxChoices)
                selectedChoice = 0;
            else if (selectedChoice < 0)
                selectedChoice = maxChoices - 1;

            choiceButtons[selectedChoice].button.Select();
        }
    }

    public Choice[] choiceButtons = new Choice[5];
    private static DialogueChoice[] choices = new DialogueChoice[5];
    private static GameObject s_gameObject;
    private static bool firstRun;
    private static bool justOpened;

    private void Awake()
    {
        firstRun = true;
        s_gameObject = gameObject;
        s_gameObject.SetActive(false);
    }

    public static void ShowChoices(DialogueChoice[] _choices)
    {
        choices = _choices;
        justOpened = true;
        s_gameObject.SetActive(true);
    }

    public static void HideChoices()
    {
        s_gameObject.SetActive(false);
    }

    private void Update()
    {
    }

    private void MoveChoice(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>().y;

        if (context.canceled)
            direction = 0;

        if (direction > 0)
            direction = -1;
        else if (direction < 0)
            direction = 1;
    }

    private void SelectChoice(InputAction.CallbackContext context)
    {
        if (justOpened)
        {
            justOpened = false;
            return;
        }
        choiceButtons[selectedChoice].button.onClick.Invoke();
    }

    private void OnEnable()
    {
        selectedChoice = 0;
        UIManager.inputModule.move.action.started += MoveChoice;
        UIManager.inputModule.move.action.canceled += MoveChoice;
        UIManager.inputModule.submit.action.performed += SelectChoice;
        maxChoices = 5;

        for (int i = 0; i < 5; i++)
        {
            if (choices[i] == null)
            {
                maxChoices = i;
                break;
            }

            choiceButtons[i].ChoiceText = choices[i].choice;
            //choiceButtons[i].button.onClick.AddListener(choices[i].ChoiceSelected);
            //choiceButtons[i].button.onClick.AddListener(HideChoices);
            choiceButtons[i].button.onClick.AddListener(() => Debug.Log("Called by " + choiceButtons[i].name));
            choiceButtons[i].gameObject.SetActive(true);
        }
        choiceButtons[selectedChoice].button.Select();
    }

    private void OnDisable()
    {
        if (firstRun)
        {
            firstRun = false;
            return;
        }
        UIManager.inputModule.move.action.started -= MoveChoice;
        UIManager.inputModule.move.action.canceled -= MoveChoice;
        UIManager.inputModule.submit.action.performed -= SelectChoice;

        for (int i = 0; i < 5; i++)
        {
            if (choices[i] == null)
                break;

            choiceButtons[i].button.onClick.RemoveAllListeners();
            choiceButtons[i].gameObject.SetActive(false);
        }

        choices = null;
        justOpened = false;
    }
}
