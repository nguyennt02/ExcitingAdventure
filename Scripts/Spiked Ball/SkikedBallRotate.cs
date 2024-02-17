using UnityEngine;

public class SkikedBallRotate : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _DirectionRight;
    private Vector3 direction = Vector3.forward;

    private void Start()
    {
        if (!_DirectionRight)
        {
            direction = Vector3.back;
        }
    }
    private void Update()
    {
        transform.Rotate(direction * _speed * Time.deltaTime);
    }
}
