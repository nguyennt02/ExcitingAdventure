using UnityEngine;

public class PlayerRenserer : PlayerVisual
{
    private TrailRenderer _tr;
    protected override void Awake()
    {
        base.Awake();
        _tr = GetComponent<TrailRenderer>();
    }
    protected override void OnDash(bool isDash)
    {
        _tr.emitting = isDash;
    }
}
