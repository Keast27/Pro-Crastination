using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Class to handle dispensing water
/// </summary>
public class WaterDispenser : MonoBehaviour
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
}
