using UnityEngine;

public class StartActive : ObjActive
{
    protected bool _actived = false;
    public virtual void Off()
    {
        _anim.SetBool("Active", false);
    }

    public override void Active(GameObject player)
    {
        base.Active(player);
        ChangCheckPoint();
        InputActive();
    }
    protected virtual void ChangCheckPoint()
    {
        if (_actived) return;
        _actived = true;
        Debug.Log("chuyển tới check point này", this);
        var checkPoint = transform.position + Vector3.up;
        GameManager.Instance.SetCheckPoint(checkPoint);
    }
    protected virtual void InputActive()
    {
        GameManager.Instance.GamePlay(0);
    }
}
