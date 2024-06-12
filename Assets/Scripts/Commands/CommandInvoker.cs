using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CommandInvoker
{
    public static Stack<ICommand> _undoStack = new Stack<ICommand>();

    public static void ExecuteCommand(ICommand command)
    {
        Debug.Log("cokolwiek");
        _undoStack.Push(command);
        command.Execute();
        
    }

    public static void UndoCommand()
    {
        if (_undoStack.Count > 0)
        {
            ICommand activeCommand = _undoStack.Pop();
            activeCommand.Undo();
        }
    }

    public static void ClearStack()
    {
        _undoStack.Clear();
    }

    public static void Reset()
    {
        while (_undoStack.Count > 0)
        {
            ICommand activeCommand = _undoStack.Pop();
            activeCommand.Undo();
        }
    }
}
