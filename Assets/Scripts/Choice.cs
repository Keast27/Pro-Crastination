using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Choice : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI text;

    public string ChoiceText 
    {
        get { return text.text; }
        set { text.text = value; }
    }

    private void Start()
    {
        button = GetComponent<Button>();
        text = GetComponent<TextMeshProUGUI>();
    }
}
