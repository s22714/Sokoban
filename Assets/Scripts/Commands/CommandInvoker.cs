using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CommandInvoker
{
    public static Stack<MoveCommand> _undoStack = new Stack<MoveCommand>();

    public static void ExecuteCommand(MoveCommand command)
    {
        _undoStack.Push(command);
        command.Execute();
        
    }

    public static void UndoCommand()
    {
        if (_undoStack.Count == 0) return;
        
        ICommand activeCommand = _undoStack.Pop();
        activeCommand.Undo();
        
    }

    public static void ClearStack()
    {
        _undoStack.Clear();
    }

    public static void Reset()
    {
        if (_undoStack.Count == 0) return;
        while (_undoStack.Count > 0)
        {
            MoveCommand activeCommand = _undoStack.Pop();
            activeCommand._playerMover.ResetPosition();
            activeCommand._boxMover?.ResetPosition();
        }
    }
}
