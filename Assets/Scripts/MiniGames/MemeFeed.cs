using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MemeFeed : MonoBehaviour, IPointerDownHandler
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    public int currentSprite;

    public void OnPointerDown(PointerEventData eventData)
    {
        ChangeSprite();
        Debug.Log("Click");
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {

    }
    void ChangeSprite()
    {
        spriteRenderer.sprite = spriteArray[currentSprite];
        currentSprite++;

        if (currentSprite >= spriteArray.Length)
        {
            currentSprite = 0;
        }
    }
}