using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TypeWriter : MonoBehaviour
{
    public AudioSource bipSound;

    private TextMeshProUGUI textMesh;
    
    private bool isTyping = false;
    private string typingText = "";
    //private float typingTotalProgress;
    private float typingCharacterProgress;
    private int typingIndex;
    private float typingTotalTime;
    private float typingCharacterTime;

    private bool inited = false;

    void Awake()
    {
        CheckInit();
    }

    public void CheckInit()
    {
        if (inited) return;
        inited = true;
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = "";
    }

    void Update()
    {
        if (!isTyping) return;

        typingCharacterProgress += Time.deltaTime;

        float stairStep = typingCharacterTime;
        if (typingCharacterProgress >= stairStep)
        {
            if (bipSound)
            {
                bipSound.PlayOneShot(bipSound.clip);
            }
            
            typingCharacterProgress -= stairStep;
            textMesh.text += typingText[typingIndex];
            typingIndex++;
            if (typingIndex >= typingText.Length)
            {
                StopTyping();
            }
        }
        
    }

    public void Typing(string text, float timeSecond)
    {
        if (!textMesh) Debug.LogError("Typing but not yet awake! wake up before Typing (aka add me to the tree)");
        isTyping = true;
        typingText = text;
        typingCharacterProgress = 0f;
        typingIndex = 0;
        typingTotalTime = timeSecond;
        typingCharacterTime = timeSecond / text.Length;
        // clear the current text
        if (textMesh) textMesh.text = "";
    }
    
    public void StopTyping()
    {
        isTyping = false;
        typingText = "";
        typingCharacterProgress = 0f;
        typingIndex = 0;
        typingTotalTime = 0;
        typingCharacterTime = 0;
    }
    
    public void SetColor(Color color)
    {
        if (!textMesh) {
            Debug.LogError("SetColor but not yet awake! wake up before SetColor (aka add me to the tree)");
            return;
        }
        textMesh.color = color;
    }
}
