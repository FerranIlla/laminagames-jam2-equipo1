using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DanceMoveTypes { _1,_2,_3,_4,_5}

//[System.Serializable]
public class DanceMove
{
    public DanceMoveTypes type = DanceMoveTypes._1;
    string name;
    //image
    //animation
    public int DebugNumber { get; }

    public DanceMove(string _name, int _move)
    {
        name = _name;
        if (_move < 0 || _move > System.Enum.GetNames(typeof(DanceMoveTypes)).Length - 1) Debug.LogWarning("Tried to construct a DanceMove with an invalid move index.");
        type = (DanceMoveTypes)_move;

        DebugNumber = ((int)type) + 1; //debug
    }

}
