using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleScreenManager : MonoBehaviour
{
    [SerializeField] private LayoutGroup screenContainer;
    [SerializeField] private MeshRenderer screenMesh;
    [SerializeField] private GameObject textPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddText("hello world"); 
        AddText("fake long unity error with multiples lines", Color.red);
        AddText("godot is better!", Color.gold);
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
        GameObject textObject = Instantiate(textPrefab, screenContainer.transform);
        TextMeshProUGUI textMesh = textObject.GetComponent<TextMeshProUGUI>();
        textMesh.text = text;
        textMesh.color = color;
    }

    public void ChangeBackgroundColor(Color color)
    {
        screenMesh.material.color = color;
    }
}
