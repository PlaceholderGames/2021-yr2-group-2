using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 2.0f;
    public Vector3 offset;

    void FixedUpdate()
    {
       Vector3 desiredPositon = target.position + offset;
       Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPositon, smoothSpeed * Time.deltaTime);
       transform.position = smoothedPosition;

       transform.LookAt(target);
    }
}
