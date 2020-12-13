﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class SocialTab : MonoBehaviour, IPointerDownHandler
{
    public GameObject workTab;
    public GameObject socialTab;

    public void OnPointerDown(PointerEventData eventData)
    {
        socialTab.SetActive(true);
        workTab.SetActive(false);
        Debug.Log("click click");
    }

    void Start()
    {

    }


    void Update()
    {

    }
}
