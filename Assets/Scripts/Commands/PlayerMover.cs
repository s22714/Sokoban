using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private LayerMask _obstacleLayer;
    private const float _boardSpacing = 1f;

    public void Move(Vector3 movement)
    {
        transform.position = transform.position + movement;
    }

    public bool IsValidMove(Vector3 movement)
    {
        return !Physics2D.Raycast(transform.position, movement, _boardSpacing, _obstacleLayer);
    }
}
