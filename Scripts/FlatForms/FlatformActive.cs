using UnityEngine;

public class FlatformActive : ObjActive
{
    protected MovingFlatform _move;
    protected override void Awake()
    {
        base.Awake();
        _move = GetComponent<MovingFlatform>();
    }
    public override void Active(GameObject player)
    {
        _move.enabled = true;
        base.Active(player);
    }
}
