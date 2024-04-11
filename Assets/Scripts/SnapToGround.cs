using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SnapToGround : MonoBehaviour
{
    void SnapRotatedObjectToGround(GameObject objectToMove)
    {
        Collider collider = objectToMove.GetComponent<Collider>();
        Vector3 lowestPoint = collider.bounds.min;
        RaycastHit hit;

        if (Physics.Raycast(lowestPoint + Vector3.up * 0.1f, Vector3.down, out hit))
        {
            float distanceToMoveDown = Vector3.Distance(lowestPoint, hit.point);
            objectToMove.transform.position -= Vector3.up * distanceToMoveDown;
        }
    }
    void Start()
    {
        SnapRotatedObjectToGround(gameObject);
    }
}