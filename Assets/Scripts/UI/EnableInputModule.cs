using UnityEngine;
using UnityEngine.InputSystem.UI;

public class EnableInputModule : MonoBehaviour
{
    private InputSystemUIInputModule module;

    private void Start()
    {
        module = GetComponent<InputSystemUIInputModule>();
        module.enabled = true;
    }
}
