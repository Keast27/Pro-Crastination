using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class WorkTab : MonoBehaviour, IPointerDownHandler
{
    public GameObject workTab;
    public GameObject socialTab;

    public void OnPointerDown(PointerEventData eventData)
    {
        socialTab.SetActive(true);
        workTab.SetActive(false);
    }

    void Start()
    {

    }


    void Update()
    {

    }
}
