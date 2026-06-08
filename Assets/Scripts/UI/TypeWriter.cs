using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TypeWriter : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    
    private bool isTyping = false;
    private string typingText = "";
    //private float typingTotalProgress;
    private float typingCharacterProgress;
    private int typingIndex;
    private float typingTotalTime;
    private float typingCharacterTime;

    void Awake()
    {
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
        isTyping = true;
        typingText = text;
        typingCharacterProgress = 0f;
        typingIndex = 0;
        typingTotalTime = timeSecond;
        typingCharacterTime = timeSecond / text.Length;
        // clear the current text
        textMesh.text = "";
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
        textMesh.color = color;
    }
}
