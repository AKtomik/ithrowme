using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleScreenManager : MonoBehaviour
{
    [Header("Conole texts")]
    [SerializeField] private LayoutGroup textContainer;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private bool doInitText;
    [SerializeField, DrawIf("doInitText")] private string initText;
    
    [Header("Conole screen")]
    [SerializeField] private MeshRenderer screenMesh;
    [SerializeField] private bool doInitScreenColor;
    [SerializeField, DrawIf("doInitScreenColor")] private Color initScreenColor;

    private GameObject[] activeTexts = new GameObject[] {};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (doInitText)
            AddText(initText); 
        if (doInitScreenColor)
            ChangeScreenColor(initScreenColor);
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    // Texts
    public void AddText(string text)
    {
        AddText(text, Color.white);
    }

    public void AddText(string text, Color color)
    {
        GameObject textObject = Instantiate(textPrefab, textContainer.transform);
        TextMeshProUGUI textMesh = textObject.GetComponent<TextMeshProUGUI>();
        textMesh.text = text;
        textMesh.color = color;
        activeTexts.Append(textObject);
    }
    
    public void ClearTexts()
    {
        foreach (var textObject in activeTexts)
        {
            Destroy(textObject);
        }
    }

    // background
    public void ChangeScreenColor(Color color)
    {
        screenMesh.material.color = color;
    }
}