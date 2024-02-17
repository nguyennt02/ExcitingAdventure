using UnityEngine;

public abstract class PlayerVisual : MonoBehaviour
{
    protected IPlayerMoveCtrl _player;
    protected bool _grounded;
    protected bool _walled;

    protected virtual void Awake()
    {
        _player = GetComponentInParent<IPlayerMoveCtrl>();
    }
    protected virtual void OnEnable()
    {
        if (_player == null) Debug.LogError("_player bị null", this);
        _player.WalledChanged += WalledChanged;
        _player.GroundedChanged += GroundedChanged;
        _player.Runed += Runed;
        _player.Jumped += Jumped;
        _player.JumpWalled += JumpWalled;
        _player.Holded += Holded;
        _player.Dashed += Dashed;
    }
    protected virtual void OnDisable()
    {
        if (_player == null) Debug.LogError("_player bị null", this);
        _player.WalledChanged -= WalledChanged;
        _player.GroundedChanged -= GroundedChanged;
        _player.Runed -= Runed;
        _player.Jumped -= Jumped;
        _player.JumpWalled -= JumpWalled;
        _player.Holded -= Holded; 
        _player.Dashed -= Dashed;
    }
    protected virtual void WalledChanged(GameObject target1)
    {
        this._walled = target1;
        if (target1)
        {
            OnWallEnter();
            OnTarget1Enter(target1);
        }
        else
        {
            OnWallExit();
        }
    }
    protected virtual void GroundedChanged(GameObject target)
    {
        this._grounded = target;
        if (target)
        {
            OnGroundEnter();
            OnTargetEnter(target);
        }
        else
        {
            OnGroundExit();
        }
    }
    protected virtual void Runed(Vector2 FrameInput)
    {
        OnRun(FrameInput);
    }
    protected virtual void Jumped()
    {
        if (_grounded)
        {
            OnJump();
        }
    }
    protected virtual void JumpWalled()
    {
        OnJumpWall();
    }
    protected virtual void Holded(bool isHold)
    {
        OnHold(isHold);
    }
    protected virtual void Dashed(bool isDash)
    {
        OnDash(isDash);
    }
    protected virtual void OnWallEnter() { }
    protected virtual void OnWallExit() { }
    protected virtual void OnTarget1Enter(GameObject target1) { }
    protected virtual void OnGroundEnter() { }
    protected virtual void OnGroundExit() { }
    protected virtual void OnTargetEnter(GameObject target) { }
    protected virtual void OnRun(Vector2 FrameInput) { }
    protected virtual void OnJump() { }
    protected virtual void OnJumpWall() { }
    protected virtual void OnHold(bool isHold) { }
    protected virtual void OnDash(bool isDash) { }
}
