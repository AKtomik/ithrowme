using UnityEngine;

public class ToggleGroupAttribute : PropertyAttribute
{
    public string groupName;
    public ToggleGroupAttribute(string groupName) => this.groupName = groupName;
}