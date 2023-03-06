using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    [Header("Blink")]
    public Color starColor = Color.black;
    public Color EndColor = Color.white;

    [Range(0, 5)]
    public float speed = 1;

    public Renderer ren;



    // rotation
    public float rotateSpeed = 5.0f;

    private Vector2 initialTouchPosition2;
    private float initialRotation;
    private bool isRotating = false;
    public Transform target2;

    void Awake()
    {
        ren = GetComponent<Renderer>();
    }

    void Update()
    {
        ren.material.color = Color.Lerp(starColor, EndColor, Mathf.PingPong(Time.time * speed, 1));

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

                target2.rotation = Quaternion.Euler(0, newRotation, 0);
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
