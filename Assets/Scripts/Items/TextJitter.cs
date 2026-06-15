using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextJitter : MonoBehaviour
{
    public string[] randomTexts;
    public float randomIntervalMin = .1f;
    public float randomIntervalMax = 1f;
    private float interval = 0;
    private TextMeshProUGUI textMesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() => textMesh = GetComponent<TextMeshProUGUI>();

    // Update is called once per frame
    void Update()
    {
        interval -= Time.deltaTime;
        if (interval < 0)
        {
            interval = Random.Range(randomIntervalMin, randomIntervalMax);
            textMesh.text = randomTexts[Random.Range(0, randomTexts.Length - 1)];
        }
    }
}
