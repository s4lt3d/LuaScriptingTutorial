using UnityEngine;

[CreateAssetMenu(fileName = "HoverSettings", menuName = "HoverSettings", order = 1)]
public class HoverSettings : ScriptableObject
{
    public float hoverHeight = 1.0f;
    public float hoverTime = 1.0f;
    
    private Vector3 startPosition;
    private Vector3 startOffset;

    public Vector3 StartPosition
    {
        get => startPosition;
        set => startPosition = value;
    }

    public Vector3 StartOffset
    {
        get => startOffset;
        set => startOffset = value;
    }

    public Vector3 CalclateHover(float time)
    {
        return StartPosition + Vector3.up * (Mathf.Sin(StartOffset.x + Time.realtimeSinceStartup * hoverTime) * hoverHeight);
    }
}