using UnityEngine;
using UnityEngine.Serialization;

public class Hover : MonoBehaviour, IResettable
{
    [FormerlySerializedAs("setting")]
    [FormerlySerializedAs("hoverSettings")]
    [SerializeField]
    HoverSettings settings;
    
    private Vector3 startPosition;
    
    void Start()
    {
        Reset();
    }
    
    void Update()
    {
        transform.localPosition = startPosition + Vector3.up * (Mathf.Sin(startPosition.x + Time.realtimeSinceStartup * settings.hoverTime) * settings.hoverHeight);
    }

    public void Reset()
    {
        startPosition = transform.localPosition;
    }
}

public interface IResettable
{
    public void Reset();
}
