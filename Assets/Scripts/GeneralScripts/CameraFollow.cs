using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;

    [SerializeField] private float smoothSpeed = .125f;
    private Vector3 _velocity = Vector3.zero;
    [SerializeField] private Vector3 offset;

    private Vector3 _initialPosition;
    private Vector3 _smoothPosition;

    private void LateUpdate()
    {
        _initialPosition = targetToFollow.position + offset;
        _smoothPosition = Vector3.SmoothDamp(transform.position, _initialPosition, ref _velocity, smoothSpeed);
        transform.position = _smoothPosition;
    }
}