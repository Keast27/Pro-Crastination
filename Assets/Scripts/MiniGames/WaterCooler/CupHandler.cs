using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// Handles moving the cup
/// </summary>
public class CupHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private enum CupState { Upright, Tilted, Overturned};

    [SerializeField]
    private TargetJoint2D targetJoint = null;
    [SerializeField]
    private Color fillColor = Color.blue;
    [SerializeField]
    private float fillSeconds = 1;
    [SerializeField]
    private SpriteRenderer cupSprite = null;
    private WaitForSeconds wait = new WaitForSeconds(0.05f);
    private Camera mainCamera;
    private Coroutine movementCoroutine;
    private Coroutine waterFillCoroutine;
    private float fill = 0;
    private CupState cupState = CupState.Overturned;

    /// <summary>
    /// Sets up the camera, target joint, and cup state coroutine
    /// </summary>
    private void Awake()
    {
        mainCamera = Camera.main;
        if(targetJoint)
        {
            targetJoint.target = gameObject.transform.position;
        }
        StartCoroutine(CupStateUpdateCoroutine());
    }

    /// <summary>
    /// Start updating the target joint when the object is clicked
    /// </summary>
    /// <param name="eventData">The event data</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if(targetJoint)
        {
            targetJoint.enabled = true;
            movementCoroutine = StartCoroutine(MovementCoroutine());
        }
    }

    /// <summary>
    /// Stop updating the target joint when the object is clicked
    /// </summary>
    /// <param name="eventData">The event data</param>
    public void OnPointerUp(PointerEventData eventData)
    {
        if(targetJoint)
        {
            targetJoint.enabled = false;
            if(movementCoroutine != null)
            {
                StopCoroutine(movementCoroutine);
            }
        }
    }

    /// <summary>
    /// Start trying to fill the cup with water when it hits the water stream
    /// </summary>
    /// <param name="collision">The other collider</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Water")
        {
            waterFillCoroutine = StartCoroutine(WaterFillCoroutine());
        }
    }

    /// <summary>
    /// Stop trying to fill the cup with water when it hits the water stream
    /// </summary>
    /// <param name="collision">The other collider</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Water")
        {
            if(waterFillCoroutine != null)
            {
                StopCoroutine(waterFillCoroutine);
            }
        }
    }

    /// <summary>
    /// Moves the target joint's target to the mouse position every so often
    /// </summary>
    /// <returns>An enumerator</returns>
    private IEnumerator MovementCoroutine()
    {
        while(true)
        {
            Vector2 cursorPosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            targetJoint.target = cursorPosition;
            yield return wait;
        }
    }

    /// <summary>
    /// Update the color based on its fill
    /// </summary>
    private void UpdateColor()
    {
        if(cupSprite)
        {
            cupSprite.color = Color.Lerp(Color.white, fillColor, Mathf.Min(fill, 1));
        }
    }

    /// <summary>
    /// Fill the cup more over time if it's upright
    /// </summary>
    /// <returns>An enumerator</returns>
    private IEnumerator WaterFillCoroutine()
    {
        while(fill < 1)
        {
            if(cupState == CupState.Upright)
            {
                fill += 0.05f / fillSeconds;
                UpdateColor();
                //If fill >= 1, you can trigger an end event here
            }
            yield return wait;
        }
    }

    /// <summary>
    /// Determines whether the cup is upright, tilted, or overturned
    /// </summary>
    /// <returns>An enumerator</returns>
    private IEnumerator CupStateUpdateCoroutine()
    {
        while(true)
        {
            float zRotation = gameObject.transform.rotation.eulerAngles.z;
            if(zRotation >= 340 || zRotation <= 20)
            {
                cupState = CupState.Upright;
            }
            else if(zRotation >= 270 || zRotation <= 90)
            {
                cupState = CupState.Tilted;
            }
            else
            {
                cupState = CupState.Overturned;
                fill = 0;
                UpdateColor();
            }
            yield return wait;
        }
    }
}
