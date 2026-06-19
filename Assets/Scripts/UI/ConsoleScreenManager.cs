using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleScreenManager : MonoBehaviour
{
    [Header("Conole texts")]
    [SerializeField] private LayoutGroup textContainer;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private Color actualPrintColor = Color.white;
    [SerializeField] private bool doInitText;
    [SerializeField, DrawIf("doInitText")] private string initText;
    
    [Header("Conole screen")]
    [SerializeField] private MeshRenderer screenMesh;
    [SerializeField] private bool doInitScreenColor;
    [SerializeField] private float emissiveIntensity;
    [SerializeField, DrawIf("doInitScreenColor")] private Color initScreenColor;



    private List<GameObject> activeTexts = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (doInitText)
            AddText(initText); 
        if (doInitScreenColor)
            SetScreenColor(initScreenColor);
        AddText("godot is better", new Color(1f, 1f, 1f, .0042f));//shhhhhhh
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    // Texts
    public void SetPrintColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString("#" + hex, out Color color)) {
            SetPrintColor(color);
        } else {
            throw new System.Exception("can't parse hex ["+hex+"] to color");
        }
    }

    public void SetPrintColor(Color color)
    {
        actualPrintColor = color;
    }

    public void AddText(string text, float typingTime = 0)
    {
        AddText(text, actualPrintColor, typingTime);
    }

    public void AddText(string text, Color color, float typingTime = 0)
    {
        GameObject textObject = Instantiate(textPrefab, textContainer.transform);
        Debug.Log(text);
        if (text == initText || text == "godot is better")
        {
            Destroy(textObject.GetComponent<AudioSource>());
        }

		if (!textObject.TryGetComponent(out TypeWriter typewriter))
		{
            Debug.LogWarning("console screen didn't detect TypeWriter in textPrefab");
			typewriter = textObject.AddComponent<TypeWriter>();
		}
        activeTexts.Add(textObject);
        typewriter.CheckInit();// in case it does not itself
		typewriter.Typing(text, typingTime);
        typewriter.SetColor(color);
        LayoutRebuilder.ForceRebuildLayoutImmediate(textContainer.GetComponent<RectTransform>());
    }
    
    public void ClearTexts()
    {
        foreach (var textObject in activeTexts)
        {
            Destroy(textObject);
        }
        activeTexts.Clear();
    }

    // background
    public void ChangeScreenColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString("#" + hex, out Color color)) {
            SetScreenColor(color);
        } else {
            throw new System.Exception("can't parse hex ["+hex+"] to color");
        }
    }

    public void SetScreenColor(Color color)
    {
        screenMesh.material.color = color;
        if (emissiveIntensity > 0) screenMesh.material.SetColor("_EmissionColor", color * emissiveIntensity);
    }
}