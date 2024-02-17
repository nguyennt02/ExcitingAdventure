using UnityEngine;

public class InputManager : IInput
{
    private InputManager() { }
    private static InputManager instance = null;
    public static InputManager Instance
    {
        get => instance ?? (instance = new InputManager());
    }

    public bool JumpDown => Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.J);
    public bool JumpHeld => Input.GetButton("Jump") || Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.J);
    public bool Dash => Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K);
    public bool Hold => Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.L);
    public Vector2 Move => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
}