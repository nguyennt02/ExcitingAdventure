using UnityEngine;

public class PlayerAnim : PlayerVisual
{
    private Transform _playerOBJ;
    private Animator _anim;

    protected override void Awake()
    {
        base.Awake();
        _playerOBJ = transform.parent.parent;
        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_player == null) return;
        SetJumpFallParameter();
    }
    private void SetJumpFallParameter()
    {
        _anim.SetFloat("yVelocity", _player.FrameVelocity.y);
    }
    protected override void GroundedChanged(GameObject target)
    {
        _anim.SetBool("Grounded", target);
    }

    protected override void OnRun(Vector2 FrameInput)
    {
        HandleSpriteFlip(FrameInput);
        SetRunParameter(FrameInput);
    }

    private bool isFacingRight = true;
    private void HandleSpriteFlip(Vector2 FrameInput)
    {
        if (isFacingRight && FrameInput.x < 0 || !isFacingRight && FrameInput.x > 0)
        {
            OnFacing();
        }
    }
    private void OnFacing()
    {
        isFacingRight = !isFacingRight;
        _playerOBJ.localScale = new Vector3(-_playerOBJ.localScale.x, _playerOBJ.localScale.y, _playerOBJ.localScale.z);
    }
    private void SetRunParameter(Vector2 FrameInput)
    {
        float runX = Mathf.Abs(FrameInput.x);
        _anim.SetFloat("xFrameInput", runX);
    }
    protected override void JumpWalled()
    {
        OnFacing();
    }
    protected override void OnHold(bool isHold)
    {
        _anim.SetBool("Hold", isHold);
    }
}
