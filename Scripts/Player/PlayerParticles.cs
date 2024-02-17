using UnityEngine;

public class PlayerParticles : PlayerVisual
{
    [Header("Particles")]
    [SerializeField] private ParticleSystem _jumpParticles;
    [SerializeField] private ParticleSystem _launchParticles;
    [SerializeField] private ParticleSystem _moveParticles;
    [SerializeField] private ParticleSystem _landParticles;

    protected override void OnGroundEnter()
    {
        _moveParticles.Play();
        _landParticles.Play();
    }
    protected override void OnGroundExit()
    {
        _moveParticles.Stop();
    }
    protected override void OnRun(Vector2 FrameInput)
    {
        var inputStrength = Mathf.Abs(FrameInput.x);
        _moveParticles.transform.localScale = Vector3.MoveTowards(_moveParticles.transform.localScale, Vector3.one * inputStrength, 2 * Time.fixedDeltaTime);
    }
    protected override void OnJump()
    {
        _jumpParticles.Play();
    }
}
