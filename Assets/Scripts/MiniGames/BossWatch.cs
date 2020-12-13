using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWatch : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite High;
    public Sprite Med;
    public Sprite Low;

    public GameObject workTab;
    public GameObject socialTab;

    private int risk = 0;
    private bool saw = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("ChangeRisk", 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(risk == 0)
        {
            saw = false;
            spriteRenderer.sprite = Low;
        } else if (risk == 1)
        {
            saw = false;
            spriteRenderer.sprite = Med;
        } else if (risk == 2)
        {
            spriteRenderer.sprite = High;

            if(socialTab.activeSelf == true)
            {
                saw = true;
               // Debug.Log("She sees the tab");
            }
        }

        if(saw == true)
        {
            socialTab.SetActive(false);
            workTab.SetActive(true);
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    void ChangeRisk()
    {
        risk = (risk + 1) % 3;

        //AT.rowNumber = lightState;

        Invoke("ChangeRisk", Random.Range(3f, 5f)); // specifies a random interval between 4 and 7 seconds
    }
}
