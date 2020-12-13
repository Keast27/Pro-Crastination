using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class WorkButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private SpriteRenderer spriteRenderer;
    public Sprite up;
    public Sprite down;
    public void OnPointerDown(PointerEventData eventData)
    {
        spriteRenderer.sprite = down;
        Debug.Log("click click");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        spriteRenderer.sprite = up;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = up;

    }


    void Update()
    {

    }
   
}

