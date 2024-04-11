using UnityEditor;
using UnityEngine;

public class SnapToGroundEditor : Editor
{
    private const string UNDO_GROUP_NAME = "Snap to Ground";

    [MenuItem("GameObject Tools/Snap to Ground %g")]
    static void SnapToGround()
    {
        Undo.SetCurrentGroupName(UNDO_GROUP_NAME);

        foreach (GameObject gameObject in Selection.gameObjects)
        {
            Collider collider = gameObject.GetComponent<Collider>();
            if (!collider)
            {
                Debug.LogWarning("GameObject " + gameObject.name + " does not have a collider.", gameObject);
                continue;
            }

            Vector3 lowestPoint = collider.bounds.min;
            RaycastHit hit;
            if (Physics.Raycast(lowestPoint + Vector3.up * 0.1f, Vector3.down, out hit))
            {
                Undo.RecordObject(gameObject.transform, UNDO_GROUP_NAME);

                float distanceToMoveDown = Vector3.Distance(lowestPoint, hit.point);
                gameObject.transform.position -= Vector3.up * distanceToMoveDown;
                Undo.CollapseUndoOperations(Undo.GetCurrentGroup());
            }
        }
    }
}