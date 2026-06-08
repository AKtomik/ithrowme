using UnityEngine;

public class DrawIfAttribute : PropertyAttribute
{
    public string conditionField;
    public bool match;

    public DrawIfAttribute(string conditionField, bool match = true)
    {
        this.conditionField = conditionField;
        this.match = match;
    }
}