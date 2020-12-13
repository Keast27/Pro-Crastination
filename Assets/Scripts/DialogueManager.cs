using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private static GameObject s_gameObject;
    private static Dialogue dialogue;

    private Queue<string> sentences; // Text to be displayed

    public TextMeshProUGUI textUI; // Box for dialogue
    //public Text question;

    public bool endText = false; // Conversation ended

    private void Awake()
    {
        s_gameObject = gameObject;
        s_gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
    }

    private void OnEnable()
    {
        UIManager.submit.performed += DisplayNextSentence;
        StartDialogue();
    }

    public static void SetDialogue(Dialogue _dialogue)
    {
        dialogue = _dialogue;
        s_gameObject.SetActive(true);
    }

    public void StartDialogue()
    {
        endText = false; // Text has started
        sentences = new Queue<string>();
        //texts.AddRange(choiceBox.GetComponentsInChildren<Text>()); // Puts choices in ChoiceBox into list
        //sentences.Clear(); // Gets rid of previous conversation
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
        }
    }

    // Goes to next sentence
    public void DisplayNextSentence()
    {
        // End conversation when there are no more sentences
        if (sentences.Count == 0 || sentences == null)
        {
            EndDialogue();
            return;
        }

        // Displays next sentence
        string sentence = sentences.Dequeue();
        StopAllCoroutines(); // Stops typing if sentence skipped while in the middle of displaying
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayNextSentence(InputAction.CallbackContext context)
    {
        DisplayNextSentence();
    }

    // Stops conversation
    public static void EndDialogue()
    {
        s_gameObject.SetActive(false);
        SteveController.ActionMap = PlayerActionMap.Standard;
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