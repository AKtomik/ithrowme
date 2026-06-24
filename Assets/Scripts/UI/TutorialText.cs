using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialText : MonoBehaviour
{
    private BothCanvas canvas;
    [SerializeField] private TextMeshProUGUI texte;
    [SerializeField] private string action;
    [SerializeField] private string input;
    [SerializeField] private bool doLog = false;

    [SerializeField] private InputActionAsset inputActions;
    private InputAction inputAction;

    private void Start()
    {
        canvas = GetComponentInParent<BothCanvas>();

        //takeAction = inputActions.FindAction("Player/Take");
        //throwAction = inputActions.FindAction("Player/Throw");
        //rollAction = inputActions.FindAction("Player/Roll");

        inputAction = inputActions.FindAction(input);
        inputAction.Enable();
        inputAction.performed += DeleteGameobject;
    }

    private void Update()
    {
        if (action == "rouler" && !canvas.player.isKeyboard)
        {
            texte.text = canvas.player.inputActions.FindAction(input).GetBindingDisplayString(3) + " pour " + action;
        }
        else
        {
            texte.text = canvas.player.inputActions.FindAction(input).GetBindingDisplayString(!canvas.player.isKeyboard ? 1 : 0) + " pour " + action;
        }
            
    }

    private void DeleteGameobject(InputAction.CallbackContext ctx)
    {
        if (doLog) Debug.Log("tutorial action completed:"+action);
        inputAction.performed -= DeleteGameobject;
        Destroy(gameObject);
    }

    
}
