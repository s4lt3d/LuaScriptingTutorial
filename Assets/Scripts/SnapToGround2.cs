using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SnapToGround2 : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float timeToSpawn = 0.1f;

    void SnapRotatedObjectToGround()
    {
        GameObject objectToMove = gameObject;
        Collider collider = objectToMove.GetComponent<Collider>();
        Vector3 lowestPoint = collider.bounds.min;
        RaycastHit hit;

        if (Physics.Raycast(lowestPoint + Vector3.up * 0.1f, Vector3.down, out hit))
        {
            float distanceToMoveDown = Vector3.Distance(lowestPoint, hit.point);
            objectToMove.transform.position -= Vector3.up * distanceToMoveDown;
        }

        if (objectToSpawn != null)
            Invoke("SpawnObject", timeToSpawn);
    }

    void SpawnObject() => Instantiate(objectToSpawn, transform.position + Vector3.forward * 1.5f + Vector3.up * 2,
        Random.rotation);

    void Start()
    {
        Invoke("SnapRotatedObjectToGround", timeToSpawn);
    }
}