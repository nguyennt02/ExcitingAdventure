using UnityEngine;

public interface IInput
{
    bool JumpDown { get; }
    bool JumpHeld { get; }
    bool Dash {  get; }
    bool Hold { get; }
    Vector2 Move { get; }
}

