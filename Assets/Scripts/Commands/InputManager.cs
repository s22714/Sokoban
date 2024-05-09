using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static BasicInputs;

public class InputManager : MonoBehaviour, IBasicMouseAndKeysActions
{
    [SerializeField] public PlayerMover _player;
    BasicInputs _inputs;

    private void Awake()
    {
        _inputs = new();
    }

    private void OnEnable()
    {
        _inputs.Enable();
        _inputs.BasicMouseAndKeys.Move.performed += OnMove;
        _inputs.BasicMouseAndKeys.Undo.performed += OnUndo;
    }

    private void OnDisable()
    {
        _inputs.Disable();
        _inputs.BasicMouseAndKeys.Move.performed -= OnMove;
        _inputs.BasicMouseAndKeys.Undo.performed -= OnUndo;
    }

    private void RunPlayerCommand(PlayerMover playerMover, Vector3 movement)
    {
        if (playerMover == null)
        {
            return;
        }

        if (playerMover.IsValidMove(movement))
        {
            ICommand command = new MoveCommand(playerMover, movement);
            CommandInvoker.ExecuteCommand(command);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 temp = context.ReadValue<Vector2>();
        RunPlayerCommand(_player, new Vector3( temp.x, temp.y ));
    }

    public void OnUndo(InputAction.CallbackContext context)
    {
        CommandInvoker.UndoCommand();
    }
}
