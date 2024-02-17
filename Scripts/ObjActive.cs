using UnityEngine;

public class ObjActive : MonoBehaviour, IActive
{
    protected Animator _anim;
    protected virtual void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    public virtual void Active(GameObject player)
    {
        if (_anim)
        {
            _anim.enabled = true;
            _anim.SetBool("Active", true);
        }
    }
}
public interface IActive
{
    void Active(GameObject player);
}
