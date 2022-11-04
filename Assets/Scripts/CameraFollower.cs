using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform Target;

    public float otstavanie;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 transformPosition = transform.position;
        transformPosition.z = Target.position.z - otstavanie;
        transform.position = transformPosition;
    }
}
