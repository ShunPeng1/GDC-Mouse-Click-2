using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 vitriball;
    private bool isDragging = false;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        isDragging = true;
        _rb.isKinematic = true;
        vitriball = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + vitriball;
        transform.position = newPosition;
    }

    void OnMouseUp()
    {
        isDragging = false;
        _rb.isKinematic = false;
        Vector2 direction = (vitriball - (transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition))) * 10f;
        _rb.AddForce(direction, ForceMode2D.Impulse);
    }
}
