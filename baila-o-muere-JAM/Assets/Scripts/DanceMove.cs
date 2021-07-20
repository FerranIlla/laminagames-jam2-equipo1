using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DanceMoves { _1,_2,_3,_4,_5}

public class DanceMove
{
    public DanceMoves move = DanceMoves._1;
    string name;
    //image
    //animation
    public int DebugNumber { get; }

    DanceMove(string _name, int _move)
    {
        name = _name;
        if (_move < 0 || _move > System.Enum.GetNames(typeof(DanceMoves)).Length - 1) Debug.LogWarning("Tried to construct a DanceMove with an invalid move index.");
        move = (DanceMoves)_move;

        DebugNumber = ((int)move) - 1; //debug
    }

}
