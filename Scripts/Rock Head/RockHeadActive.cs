using UnityEngine;

public class RockHeadActive : FlatformActive
{
    public override void Active(GameObject player)
    {
        base.Active(player);
        AudioMainManager.Instance.PlaySFX("Scream");
    }
}
