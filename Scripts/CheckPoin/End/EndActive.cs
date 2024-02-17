public class EndActive : StartActive
{
    public override void Off()
    {
        base.Off();
        GameManager.Instance.GameWiner();
    }
    protected override void ChangCheckPoint() { }
    protected override void InputActive() { }
}