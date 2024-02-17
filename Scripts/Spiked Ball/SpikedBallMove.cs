using UnityEngine;

public class SpikedBallMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float limit;

    private void Update()
    {
        float angle = limit * Mathf.Sin(Time.time * speed);
        transform.localRotation = Quaternion.Euler(0,0,angle);
    }
}
