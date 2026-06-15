using UnityEngine;

public class CompassDirection : MonoBehaviour
{
    public Transform compassNeedle;
    public Transform compassObjective;

    void Update()
    {
        compassNeedle.LookAt(compassObjective);
    }
}
