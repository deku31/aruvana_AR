using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom : MonoBehaviour
{
    public float zoomSpeed = 0.5f;
    public float minZoom = 0.5f;
    public float maxZoom = 2.0f;

    private Vector3 initialScale;
    private Vector2 initialTouchPosition;
    private float initialDistance;
    private bool isZooming = false;
    public Transform target;
    public Transform target2;


    // rotation
    public float rotateSpeed = 5.0f;

    private Vector2 initialTouchPosition2;
    private float initialRotation;
    private bool isRotating = false;


    void Start()
    {
        initialScale = target.localScale;
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            if (!isZooming)
            {
                initialTouchPosition = Input.GetTouch(0).position - Input.GetTouch(1).position;
                initialDistance = initialTouchPosition.magnitude;
                isZooming = true;
            }
            else
            {
                Vector2 currentTouchPosition = Input.GetTouch(0).position - Input.GetTouch(1).position;
                float currentDistance = currentTouchPosition.magnitude;
                float zoomFactor = currentDistance / initialDistance;

                Vector3 newScale = initialScale * zoomFactor;

                // Clamp the new scale to the min and max zoom values
                newScale.x = Mathf.Clamp(newScale.x, initialScale.x * minZoom, initialScale.x * maxZoom);
                newScale.y = Mathf.Clamp(newScale.y, initialScale.y * minZoom, initialScale.y * maxZoom);
                newScale.z = Mathf.Clamp(newScale.z, initialScale.z * minZoom, initialScale.z * maxZoom);

                target.localScale = newScale;
            }
        }
        else
        {
            isZooming = false;
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                initialTouchPosition2 = touch.position;
                initialRotation = transform.eulerAngles.y;
                isRotating = true;
            }
            else if (touch.phase == TouchPhase.Moved && isRotating)
            {
                float rotationFactor = touch.deltaPosition.x * rotateSpeed * Time.deltaTime;
                float newRotation = initialRotation + rotationFactor;

                target2.rotation = Quaternion.Euler(transform.eulerAngles.x, newRotation, transform.eulerAngles.z);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isRotating = false;
            }
        }
        else
        {
            isRotating = false;
        }
    }
}
