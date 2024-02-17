using System.Collections;
using UnityEngine;

public class MovingFlatform : MonoBehaviour
{
    [SerializeField] private PointMove[] arr_PoinMove;
    [SerializeField] private float _speed;
    [SerializeField] private float _delay;
    [SerializeField] private float _acceleration;

    private Animator _anim;

    private Vector3 _PoinNext;
    private int direction = 1;
    private int index = 1;
    private float time = 0;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _PoinNext = arr_PoinMove[index].point.position;
    }
    private void Update()
    {
        time += Time.deltaTime;
        Moving();
        Navigation(_PoinNext);
    }

    private void Moving()
    {
        transform.position = Vector3.MoveTowards(transform.position, _PoinNext, (_acceleration/2) * time * time + _speed * Time.deltaTime);
    }

    private void Navigation(Vector3 target)
    {
        var distance = (target - transform.position).magnitude;
        if (distance > 0.1f) return;
        if (isDelay) return;
        StartCoroutine(Delay());
    }
    private bool isDelay = false;
    private IEnumerator Delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(_delay);
        ChangeDirection();
        PoinNext();
        isDelay = false;
    }
    private void ChangeDirection()
    {
        time = 0;
        if (isChangDirection())
        {
            direction *= -1;
        }
        if (isStop())
        {
            this.enabled = false;
            if (_anim)
            {
                _anim.SetBool("Active", false);
            }
        }
    }
    private bool isStop()
    {
        return arr_PoinMove[index].stop;
    }
    private bool isChangDirection()
    {
        return index == 0 || index == arr_PoinMove.Length - 1;
    }
    private void PoinNext()
    {
        index += direction;
        _PoinNext = arr_PoinMove[index].point.position;
    }
}
[System.Serializable]
public class PointMove
{
    public Transform point;
    public bool stop;
}