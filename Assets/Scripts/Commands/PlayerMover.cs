using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private LayerMask _boxLayer;
    private const float _boardSpacing = 1f;

    public void Move(Vector3 movement)
    {
        transform.position = transform.position + movement;
    }

    public bool IsValidMove(Vector3 movement, MoveCommand command)
    {
        var o = Physics2D.Raycast(transform.position, movement, _boardSpacing, _boxLayer);
        if (Physics2D.Raycast(transform.position, movement, _boardSpacing, _boxLayer))
        {
            command._boxMover = o.transform.gameObject.GetComponent<BoxMover>();
            return command._boxMover.Move(movement);
        }
        
        
        return !Physics2D.Raycast(transform.position, movement, _boardSpacing, _obstacleLayer);
    }
}
