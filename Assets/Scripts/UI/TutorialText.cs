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

    [SerializeField] private InputActionAsset inputActions;
    private InputAction takeAction;
    private InputAction throwAction;
    private InputAction rollAction;

    private void Start()
    {
        canvas = GetComponentInParent<BothCanvas>();
        Debug.Log(canvas);

        takeAction = inputActions.FindAction("Player/Take");
        throwAction = inputActions.FindAction("Player/Throw");
        rollAction = inputActions.FindAction("Player/Roll");

        switch (action)
        {
            case "prendre":
                takeAction.Enable();
                takeAction.performed += DeleteGameobject;
                break;
            case "lancer":
                
                throwAction.Enable();
                throwAction.performed += DeleteGameobject;
                break;
            case "rouler":
                rollAction.Enable();
                rollAction.performed += DeleteGameobject;
                break;

        }
    }

    private void Update()
    {
        //InputSystem.onActionChange += player.OnInputChange;


        if (canvas.player.isKeyboard)
        {
            switch (action)
            {
                case "prendre":
                    texte.text = canvas.player.inputActions.FindAction("Player/Take").GetBindingDisplayString(0) + " pour " + action;
                    break;
                case "lancer":
                    texte.text = canvas.player.inputActions.FindAction("Player/Throw").GetBindingDisplayString(0) + " pour " + action;
                    break;
                case "rouler":
                    texte.text = canvas.player.inputActions.FindAction("Player/Roll").GetBindingDisplayString(0) + " pour " + action;
                    break;

            }

            

        }
        else
        {
            switch (action)
            {
                case "prendre":
                    texte.text = canvas.player.inputActions.FindAction("Player/Take").GetBindingDisplayString(1) + " pour " + action;
                    break;
                case "lancer":
                    texte.text = canvas.player.inputActions.FindAction("Player/Throw").GetBindingDisplayString(1) + " pour " + action;
                    break;
                case "rouler":
                    texte.text = canvas.player.inputActions.FindAction("Player/Roll").GetBindingDisplayString(3) + " pour " + action;
                    break;

            }

        }


    }

    private void DeleteGameobject(InputAction.CallbackContext ctx)
    {
        Debug.Log("AAAAA");
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
        
    }

    
}
