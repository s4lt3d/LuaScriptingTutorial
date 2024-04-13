using UnityEngine;
using UnityEngine.Serialization;

public class ConstantRotation : MonoBehaviour
{
    public float rotationSpeed = 1;
    public Vector3 rotationAxis = new(0, 1, 0);
    public float initialAngle;

    private void Update()
    {
        initialAngle += rotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.AngleAxis(initialAngle, rotationAxis);
    }
}