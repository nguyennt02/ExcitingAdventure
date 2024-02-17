using System.Collections;
using UnityEngine;

public class PlayerSounds : PlayerVisual
{
    private ISfxSource _SfxSource;
    protected override void Awake()
    {
        base.Awake();
        _SfxSource = GetComponent<ISfxSource>();
    }
    protected override void OnRun(Vector2 FrameInput)
    {
        if (_grounded)
        {
            if (FrameInput.x != 0 && !isPlayRunSound)
            {
                StartCoroutine(PlayRunCoroutine());
            }
        }
    }
    private bool isPlayRunSound = false;
    private IEnumerator PlayRunCoroutine()
    {
        isPlayRunSound = true;
        _SfxSource.PlaySFX("Run");
        yield return new WaitForSeconds(0.5f);
        isPlayRunSound = false;
    }
    protected override void OnDash(bool isDash)
    {
        if (isDash)
        {
            _SfxSource.PlaySFX("Dash");
        }
    }
    protected override void OnJump()
    {
        _SfxSource.PlaySFX("JumpUp");
    }
    protected override void JumpWalled()
    {
        _SfxSource.PlaySFX("JumpUp");
    }
    protected override void OnGroundEnter()
    {
        _SfxSource.PlaySFX("JumpLand");
    }
}
