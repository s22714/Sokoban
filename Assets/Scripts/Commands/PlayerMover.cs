using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private LayerMask _boxLayer;
    private const float _boardSpacing = 1f;
    private Vector3 _destination;
    private Vector3 _originalPosition;
    public bool moving = false;
    public static Animator _animator;


    private void Start()
    {
        _destination = transform.position;
        _originalPosition = transform.position;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (transform.position == _destination) 
        {
            moving = false;
            SetAnimation(Vector3.zero);
        }
        transform.position = Vector2.MoveTowards(transform.position, _destination, 3 * Time.deltaTime);
    }
    public void ResetPosition()
    {
        transform.position = _originalPosition;
        _destination = _originalPosition;
    }
    public void Move(Vector3 movement)
    {
        _destination = transform.position + movement;
        SetAnimation(movement);
        moving = true;
    }

    public bool IsValidMove(Vector3 movement, MoveCommand command)
    {
        if(transform.position != _destination)
        {
            return false;
        }

        var o = Physics2D.Raycast(transform.position, movement, _boardSpacing, _boxLayer);
        if (Physics2D.Raycast(transform.position, movement, _boardSpacing, _boxLayer))
        {
            command._boxMover = o.transform.gameObject.GetComponent<BoxMover>();
            return command._boxMover.Move(movement);
        }
        
        
        return !Physics2D.Raycast(transform.position, movement, _boardSpacing, _obstacleLayer);
    }

    private void SetAnimation(Vector3 movement)
    {
        if(movement.x > 0)
        {
            _animator.SetBool("Right", true);
            return;
        }
        if(movement.y > 0)
        {
            _animator.SetBool("Up", true);
            return;
        }
        if (movement.x < 0)
        {
            _animator.SetBool("Left", true);
            return;
        }
        if (movement.y < 0)
        {
            _animator.SetBool("Down", true);
            return;
        }
        if (!moving)
        {
            foreach(var b in _animator.parameters)
            {
                if(b.type == AnimatorControllerParameterType.Bool)
                {
                    if (b.name == "Win")
                    {
                        continue;
                    }
                    _animator.SetBool(b.name,false);
                }
            }
        }
    }

    private void Win()
    {
        StorageScript.canFinish = true;
    }
}
