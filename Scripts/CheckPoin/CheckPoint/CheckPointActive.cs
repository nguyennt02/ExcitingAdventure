using UnityEngine;

public class CheckPointActive : StartActive
{
    public override void Active(GameObject player)
    {
        InputActive();
        if (_actived) return;
        base.Active(player);
        AudioMainManager.Instance.PlaySFX("Collide");
    }
}
