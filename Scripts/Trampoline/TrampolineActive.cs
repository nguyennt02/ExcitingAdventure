using UnityEngine;

public class TrampolineActive : StartActive
{
    [SerializeField] private float _Force;
    public override void Active(GameObject player)
    {
        base.Active(player);
        SetDirection(player);
    }
    private void SetDirection(GameObject player)
    {
        // Lấy góc quay từ thành phần Z của Quaternion
        float angleInDegrees = transform.rotation.eulerAngles.z;

        // Chuyển đổi góc từ độ sang radian
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;

        // Tạo quaternion từ góc xoay
        Quaternion rotation = Quaternion.Euler(0, 0, angleInDegrees);

        // Xoay vector
        Vector2 rotatedVector = rotation * Vector2.up;

        player.GetComponent<IJet>().Jet(rotatedVector, _Force);
    }
    protected override void ChangCheckPoint() { }
    protected override void InputActive() { }
}
