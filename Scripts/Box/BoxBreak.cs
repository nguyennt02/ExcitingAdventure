
using System.Collections;
using UnityEngine;

public class BoxBreak : MonoBehaviour
{
    private Rigidbody2D _rg;
    [SerializeField] private Vector2 _ForceDirection = Vector2.right;
    [SerializeField] private float _Force = 20;
    [SerializeField] private float _timeLife;

    // Start is called before the first frame update
    private void Awake()
    {
        _rg = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        AudioMainManager.Instance.PlaySFX("Impact");
        AddForce();
        Destroy(gameObject, _timeLife);
    }
    private void AddForce()
    {
        _rg.AddForce(_Force * 10 * _ForceDirection);
    }
}
