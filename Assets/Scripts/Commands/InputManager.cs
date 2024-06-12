using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
        _inputs.BasicMouseAndKeys.Pause.performed += OnPause;
        _inputs.BasicMouseAndKeys.Reset.performed += OnReset;
    }

    private void OnDisable()
    {
        _inputs.Disable();
        _inputs.BasicMouseAndKeys.Move.performed -= OnMove;
        _inputs.BasicMouseAndKeys.Undo.performed -= OnUndo;
        _inputs.BasicMouseAndKeys.Pause.performed -= OnPause;
        _inputs.BasicMouseAndKeys.Reset.performed -= OnReset;
    }

    private void RunPlayerCommand(PlayerMover playerMover, Vector3 movement)
    {
        if (playerMover == null)
        {
            return;
        }

        MoveCommand command = new MoveCommand(playerMover, movement);

        if (playerMover.IsValidMove(movement,command))
        {
            CommandInvoker.ExecuteCommand(command);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("input");
        Vector2 temp = context.ReadValue<Vector2>();
        RunPlayerCommand(_player, new Vector3( temp.x, temp.y ));
    }

    public void OnUndo(InputAction.CallbackContext context)
    {
        CommandInvoker.UndoCommand();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        GameGuiManager.PauseEvent?.Invoke();
    }

    public void OnReset(InputAction.CallbackContext context)
    {
        CommandInvoker.Reset();
    }
}
