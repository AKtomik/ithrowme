using UnityEngine;

public class MaterialJitter : MonoBehaviour
{
    public Material[] randomMaterials;
    public float randomIntervalMin = .1f;
    public float randomIntervalMax = 1f;
    private float interval = 0;
    private Renderer rendrerer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() => rendrerer = GetComponent<Renderer>();

    // Update is called once per frame
    void Update()
    {
        interval -= Time.deltaTime;
        if (interval < 0)
        {
            interval = Random.Range(randomIntervalMin, randomIntervalMax);
            rendrerer.material = randomMaterials[Random.Range(0, randomMaterials.Length - 1)];
        }
    }
}
