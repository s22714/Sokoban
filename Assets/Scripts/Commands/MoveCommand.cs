using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    PlayerMover _playerMover;
    public BoxMover _boxMover;
    Vector3 _movement;

    public MoveCommand(PlayerMover player, Vector3 moveVector, BoxMover boxMover = null)
    {
        this._playerMover = player;
        this._movement = moveVector;
        _boxMover = boxMover;
    }

    public void Execute()
    {
        _playerMover.Move(_movement);
    }

    public void Undo()
    {
        if (_boxMover != null)
        {
            _boxMover.Move(-_movement);
        }
        _playerMover.Move(-_movement);
    }

}
