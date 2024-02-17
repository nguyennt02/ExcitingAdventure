using System.Collections;
using UnityEngine;

public class Player : PlayerVisual, IPlayerDie, IJet, IHealing
{
    [SerializeField] private int _maxLife;
    private int _life;
    private ISfxSource _SfxSource;

    protected override void Awake()
    {
        base.Awake();
        _SfxSource = GetComponentInChildren<ISfxSource>();
    }

    private void Start()
    {
        _life = _maxLife;
        AddLife.Instance.CreatMaxLife(_maxLife);
    }

    public void Healing(int life)
    {
        _life += life;
        if (_life > _maxLife)
        {
            _life = _maxLife;
        }
        AddLife.Instance.SetLife(_life);
    }

    #region DIE
    [SerializeField] private float timeImmortal;
    private bool _immortal = false;
    public void Die()
    {
        if (_immortal) return;
        StartCoroutine(Immortal());
        _life--;
        AddLife.Instance.SetLife(_life);
        if (_life == 0)
        {
            GameManager.Instance.Gameover();
        }
        if (_life > 0)
        {
            _SfxSource.PlaySFX("Hit");
            GameManager.Instance.GamePlay(1);
            GameManager.Instance.ReturnToCheckPoint(gameObject);
        }
    }

    private IEnumerator Immortal()
    {
        _immortal = true;
        yield return new WaitForSeconds(timeImmortal);
        _immortal = false;
    }

    #endregion
    #region PARENT
    protected override void OnTargetEnter(GameObject target)
    {
        SetParentOfPlayer(target);
    }
    protected override void OnGroundExit()
    {
        SetParentOfPlayer();
    }

    private bool isParent = false;
    protected override void OnHold(bool isHold)
    {
        if (isHold)
        {
            if (isParent) return;
            SetParentOfPlayer(_target);
            isParent = true;
        }
        else
        {
            if (!isParent) return;
            SetParentOfPlayer();
            isParent = false;
        }
    }

    private GameObject _target;

    protected override void OnTarget1Enter(GameObject target1)
    {
        _target = target1;
    }
    protected override void OnWallExit()
    {
        SetParentOfPlayer();
        isParent = false;
    }
    private void SetParentOfPlayer(GameObject target)
    {
        var flatform = target.GetComponent<MovingFlatform>();
        if (flatform)
        {
            transform.SetParent(target.transform);
        }
    }
    private void SetParentOfPlayer()
    {
        transform.SetParent(null);
    }

    public void Jet(Vector2 Direction, float Force)
    {
        _player.FrameVelocity = Direction * Force;
    }
    #endregion
}
public interface IPlayerDie
{
    void Die();
}
public interface IHealing
{
    void Healing(int life);
}
public interface IJet
{
    void Jet(Vector2 Direction, float Force);
}

