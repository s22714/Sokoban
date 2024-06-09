using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MonoBehaviour
{
    private const float _boardSpacing = 1f;
    [SerializeField] private LayerMask _obstacleLayer;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    public bool Move(Vector3 movement)
    {
        _boxCollider.enabled = false;
        if (IsValidMove(movement))
        {
            _boxCollider.enabled = true;
            transform.position = transform.position + movement;
            return true;
        }
        _boxCollider.enabled = true;
        return false;
    }
    public bool IsValidMove(Vector3 movement)
    {
        return !Physics2D.Raycast(transform.position, movement, _boardSpacing, _obstacleLayer);
    }
}
