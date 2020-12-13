using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// Class to handle dispensing water
/// </summary>
public class WaterDispenser : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private GameObject water = null;
    private bool activated = false;

    /// <summary>
    /// Toggle whether water is dispensing or not when clicked
    /// </summary>
    /// <param name="eventData">The event data</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        activated = !activated;
        if(water)
        {
            water.SetActive(activated);
        }
    }
}
