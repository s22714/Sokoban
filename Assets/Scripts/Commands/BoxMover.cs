using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MonoBehaviour
{
    private const float _boardSpacing = 1f;
    [SerializeField] private LayerMask _obstacleLayer;
    private BoxCollider2D _boxCollider;
    private Vector3 _destination;
    private Vector3 _originalPosition;
    public bool moving = false;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _destination = transform.position;
        _originalPosition = transform.position;
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _destination, 3 * Time.deltaTime);
        if(transform.position == _destination)
        {
            moving = false;
        }
    }
    public bool Move(Vector3 movement)
    {
        _boxCollider.enabled = false;
        if (IsValidMove(movement))
        {
            _boxCollider.enabled = true;
            _destination = transform.position + movement;
            moving = true;
            return true;
        }
        _boxCollider.enabled = true;
        return false;
    }
    public bool IsValidMove(Vector3 movement)
    {
        if (_destination != transform.position)
        {
            return false;
        }
        return !Physics2D.Raycast(transform.position, movement, _boardSpacing, _obstacleLayer);
    }

    public void ResetPosition()
    {
        _destination = _originalPosition;
        transform.position = _originalPosition;
    }
}
