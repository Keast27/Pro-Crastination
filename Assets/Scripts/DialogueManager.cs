using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private static GameObject s_gameObject;
    private static Dialogue dialogue;
    private static string objectName;
    //private static DialogueManager self;
    private static bool firstTime = true;
    public static bool endText = false; // Conversation ended

    public static DialogueManager Self { get; private set; }

    private static Queue<string> sentences; // Text to be displayed

    public TextMeshProUGUI textUI; // Box for dialogue
    public TextMeshProUGUI nameUI; // Box for name
    //public Text question;


    private void Awake()
    {
        Self = this;
        s_gameObject = gameObject;
        firstTime = true;
        s_gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
    }

    private void OnEnable()
    {
        UIManager.submit.performed += DisplayNextSentence;
        nameUI.text = objectName;
        StartDialogue();
    }

    /*
    public void StartDialogue(Dialogue _dialogue, string _objectName)
    {
        dialogue = _dialogue;
        objectName = _objectName;
        UIManager.submit.performed += DisplayNextSentence;
        nameUI.text = objectName;
        s_gameObject.SetActive(true);
        StartDialogue();
    }
    */

    public static void SetDialogue(Dialogue _dialogue, string _objectName, bool last = false)
    {
        dialogue = _dialogue;
        objectName = _objectName;
        endText = last;
        s_gameObject.SetActive(true);
    }

    public static void SetDialogue(Dialogue _dialogue, bool last = false)
    {
        dialogue = _dialogue;
        endText = last;

        foreach (string sentence in dialogue.sentences)
            sentences.Enqueue(sentence);

    }

    public void StartDialogue()
    {
        endText = false; // Text has started
        sentences = new Queue<string>();

        foreach (string sentence in dialogue.sentences)
            sentences.Enqueue(sentence);

        textUI.gameObject.SetActive(true); // Displays text box

        DisplayNextSentence(); // Shows first sentence
    }

    // Types out sentence
    IEnumerator TypeSentence(string sentence)
    {
        textUI.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            textUI.text += letter;
            yield return new WaitForSeconds(0.01f);
            if (letter == '-')
                DisplayNextSentence();

            if (textUI.isTextOverflowing)
                textUI.pageToDisplay = textUI.textInfo.pageCount;
        }
    }

    // Goes to next sentence
    public void DisplayNextSentence()
    {
        textUI.pageToDisplay = 0;
        // End conversation when there are no more sentences
        if (sentences.Count == 0 || sentences == null)
        {
            if (endText)
            {
                EndDialogue();
                return;
            }

            if (UIManager.currentScript.HasChoices)
                ChoiceManager.ShowChoices(UIManager.currentScript.choices);
            return;
        }

        // Displays next sentence
        string sentence = sentences.Dequeue();
        StopAllCoroutines(); // Stops typing if sentence skipped while in the middle of displaying
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayNextSentence(InputAction.CallbackContext context)
    {
        if (firstTime)
        {
            firstTime = false;
            return;
        }

        DisplayNextSentence();
    }

    // Stops conversation
    public static void EndDialogue()
    {
        sentences.Clear();
        SteveController.ActionMap = PlayerActionMap.Standard;
        s_gameObject.SetActive(false);
        //firstTime = false;
        // If there are any choices to make they'll be shown after the orginal conversation ends
        //if (choiceBox.activeSelf)
        //{
        //    choiceBox.SetActive(false);
        //    endText = true; // End of conversation
        //}
        //else
        //{
        //    DisplayChoices(choices);
        //}
    }

    private void OnDisable()
    {
        UIManager.submit.performed -= DisplayNextSentence;
        //textUI.gameObject.SetActive(false);
    }

    // Displays choices
    public void DisplayChoices(string[] choices)
    {
        // Displayes the correct number of choices
        //if (choices.Length < texts.Count)
        //{
        //    for (int i = 1; i < texts.Count - choices.Length; i++)
        //    {
        //        texts[texts.Count - i].gameObject.SetActive(false);
        //    }
        //}
        //else
        //{
        //    for (int i = 1; i == texts.Count - choices.Length; i++)
        //    {
        //        texts[texts.Count - i].gameObject.SetActive(true);
        //    }
        //}
        //
        //for (int i = 0; i < choices.Length; i++)
        //{
        //    texts[i + 1].text = choices[i];
        //}

        //ChoiceManager.Destroy
        //choiceBox.SetActive(true); // Displays choices Box
    }
}