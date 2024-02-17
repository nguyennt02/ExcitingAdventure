using UnityEngine;

public class BoxActive : StartActive
{
    [SerializeField] private GameObject _BoxBreak;
    [SerializeField] private GameObject _item;
    [SerializeField] private int _MaxLife;
    [SerializeField] private float _Jet;
    private int _life;
    private void Start()
    {
        _life = _MaxLife;
    }
    public override void Active(GameObject player)
    {
        _life--;
        AudioMainManager.Instance.PlaySFX("Box");
        if(_life <= 0)
        {
            GameObject Box = Instantiate(_BoxBreak, transform);
            Box.transform.SetParent(null);

            if (_item)
            {
                GameObject Item = Instantiate(_item, transform);
                Item.transform.SetParent(null);
            }

            player.GetComponent<IJet>().Jet(Vector2.up, _Jet);

            Destroy(gameObject);
        }
        else if( _life > 0) {
            base.Active(player);
        }
    }
    protected override void ChangCheckPoint(){}
    protected override void InputActive() { }

}
