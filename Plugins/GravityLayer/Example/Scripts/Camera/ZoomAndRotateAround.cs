using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAndRotateAround : MonoBehaviour
{
    [SerializeField]
    private float _mouseSensitivity = 3.0f;

    [SerializeField]
    private float _rotationY;
    [SerializeField]
    private float _rotationX = 10.0f;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _initialDistanceFromTarget = 3.0f;

    [SerializeField]
    private float _zoomSpeed = 5.0f;
    [SerializeField]
    private float _zoomMin = 1.0f;
    [SerializeField]
    private float _zoomMax = 5.0f;

    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    [SerializeField]
    private float _smoothTime = 0.2f;

    [SerializeField]
    private Vector2 _rotationXMinMax = new Vector2(-40, 40);

    void Awake()
    {
        Vector3 _currentRotation = transform.localEulerAngles;
        _currentRotation.x = _rotationX;
        transform.localEulerAngles = _currentRotation;
        transform.position = _target.position - transform.forward * _initialDistanceFromTarget;
    }

    void Update()
    {
        if (_target == null) return;

        if (Input.GetMouseButton(0))
        {
            RotateAround();
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float zoomAmount = Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;

            Zoom(zoomAmount);
        }
    }

    void RotateAround()
    {
        float dist = Vector3.Distance(transform.position, _target.position);

        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _rotationY += mouseX;
        _rotationX -= mouseY;

        // Apply clamping for x rotation 
        _rotationX = Mathf.Clamp(_rotationX, _rotationXMinMax.x, _rotationXMinMax.y);

        Vector3 nextRotation = new Vector3(_rotationX, _rotationY);

        // Apply damping between rotation changes
        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
        transform.localEulerAngles = _currentRotation;

        // Substract forward vector of the GameObject to point its forward vector to the target    
        transform.position = _target.position - transform.forward * dist;
    }

    void Zoom(float zoomAmount)
    {
        float dist = Vector3.Distance(transform.position, _target.position);

        if ((zoomAmount > 0) & (dist > _zoomMin))
        {
            transform.position += transform.forward * zoomAmount;
        }

        if ((zoomAmount < 0) & (dist < _zoomMax))
        {
            transform.position += transform.forward * zoomAmount;
        }
    }
}
