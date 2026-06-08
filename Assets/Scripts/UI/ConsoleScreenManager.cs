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
    [SerializeField, DrawIf("doInitScreenColor")] private Color initScreenColor;

    private List<GameObject> activeTexts = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (doInitText)
            AddText(initText); 
        if (doInitScreenColor)
            ChangeScreenColor(initScreenColor);
        AddText("godot is better", new Color(.042f, .042f, .042f));//shhhhhhh
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

    public void AddText(string text)
    {
        AddText(text, actualPrintColor);
    }

    public void AddText(string text, Color color)
    {
        GameObject textObject = Instantiate(textPrefab, textContainer.transform);
        TextMeshProUGUI textMesh = textObject.GetComponent<TextMeshProUGUI>();
        textMesh.text = text;
        textMesh.color = color;
        activeTexts.Add(textObject);
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
            ChangeScreenColor(color);
        } else {
            throw new System.Exception("can't parse hex ["+hex+"] to color");
        }
    }

    public void ChangeScreenColor(Color color)
    {
        screenMesh.material.color = color;
    }
}