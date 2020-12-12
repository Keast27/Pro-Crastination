using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Asset")]
public class Dialogue : ScriptableObject
{
    [TextArea(3, 10)]
    [SerializeField] private string[] sentences; // Dialogue
    private Queue<string> sentenceQueue;

    public Queue<string> Sentences
    {
        get
        {
            if (sentenceQueue == null)
            {
                sentenceQueue = new Queue<string>();
                foreach (string sentence in sentences)
                {
                    sentenceQueue.Enqueue(sentence);
                }
            }

            return sentenceQueue;
        }
    } // Dialogue
}