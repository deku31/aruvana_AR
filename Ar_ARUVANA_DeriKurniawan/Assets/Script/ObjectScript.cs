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

    [Header("swipe")]
    [SerializeField] private float _rotationSpeed = 10f;

    private Vector2 _startPosition;
    private float _rotationAngle = 0f;

    void Awake()
    {
        ren = GetComponent<Renderer>();
    }

    void Update()
    {
        ren.material.color = Color.Lerp(starColor, EndColor, Mathf.PingPong(Time.time * speed, 1));

        //swipe script

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _startPosition = Input.GetTouch(0).position;
        }
        else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 currentPosition = Input.GetTouch(0).position;
            Vector2 direction = currentPosition - _startPosition;
            float rotationAngle = Vector2.SignedAngle(Vector2.up, direction);
            transform.rotation = Quaternion.Euler(0f, _rotationAngle - rotationAngle * _rotationSpeed, 0f);
        }
        else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _rotationAngle = transform.eulerAngles.y;
        }
    }
}
