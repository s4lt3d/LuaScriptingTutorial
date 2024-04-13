using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField]
    HoverSettings hover;
    
    private Vector3 startPosition;
    private Vector3 startOffset;
    
    void Start()
    {
        Reset();
    }
    
    void Update()
    {
        transform.localPosition = hover.CalclateHover(Time.time);
    }

    public void Reset()
    {
        hover.StartPosition = transform.localPosition;
        hover.StartOffset = transform.position;
    }
}
