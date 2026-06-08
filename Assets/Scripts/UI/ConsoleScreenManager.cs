using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleScreenManager : MonoBehaviour
{
    [Header("Conole texts")]
    [SerializeField] private LayoutGroup textContainer;
    [SerializeField] private GameObject textPrefab;
    
    [Header("Conole screen")]
    [SerializeField] private MeshRenderer screenMesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddText("hello world"); 
        AddText("fake long unity error with multiples lines", Color.red);
        AddText("godot is better!", Color.gold);
        ChangeScreenColor(Color.gold);
    }

    // Update is called once per frame
    void Update()
    {
    }

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
    }

    public void ChangeScreenColor(Color color)
    {
        screenMesh.material.color = color;
    }
}