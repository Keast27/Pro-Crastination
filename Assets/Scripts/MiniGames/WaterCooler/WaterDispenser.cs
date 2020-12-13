using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// Class to handle dispensing water
/// </summary>
public class WaterDispenser : MonoBehaviour, IPointerDownHandler // Turns out this is all you needed
{
    [SerializeField]
    private GameObject water = null;
    private bool activated = false;

    public void ToggleDispenser()
    {
        activated = !activated;
        if(water)
        {
            water.SetActive(activated);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerCurrentRaycast);
        ToggleDispenser();
    }
}
