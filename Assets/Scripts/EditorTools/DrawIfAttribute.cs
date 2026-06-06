using UnityEngine;

public class DrawIfAttribute : PropertyAttribute
{
    public string conditionField;
    public bool inverse;

    public DrawIfAttribute(string conditionField, bool inverse = false)
    {
        this.conditionField = conditionField;
        this.inverse = inverse;
    }
}