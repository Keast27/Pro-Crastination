using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputSystemUIInputModule inputMod;
    public static InputSystemUIInputModule inputModule;
    public static InputAction submit;

    private void Awake()
    {
        inputModule = inputMod;
        submit = inputMod.submit.action;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
