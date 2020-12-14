using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class MemeFeed : MonoBehaviour, IPointerDownHandler
{
    public TextMeshPro captioner;
    private SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    public int currentSprite;

    public string[] captions;
    public void OnPointerDown(PointerEventData eventData)
    {
        ChangeSprite();
        Debug.Log("Click");
    }

    void Start()
    {
        captioner.text = captions[currentSprite];
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {

    }
    void ChangeSprite()
    {
        spriteRenderer.sprite = spriteArray[currentSprite];
        captioner.text = captions[currentSprite];
        currentSprite++;

        if (currentSprite >= spriteArray.Length)
        {
            currentSprite = 0;
        }
    }
}