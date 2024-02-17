using UnityEngine;

public class FruitActive : ObjActive
{
    [SerializeField] public FRUITS FRUITS;
    public override void Active(GameObject player)
    {
        _anim.SetBool("Active", false);
        AudioMainManager.Instance.PlaySFX("Furit");
        GameManager.Instance.FuritIncreased(gameObject);
    }
    public void DestroyFruit()
    {
        Destroy(gameObject);
    }
}
public enum FRUITS
{
    APPLE,
    BANANAS,
    CHERRIES,
    KIWI,
    MELON,
    ORANGE,
    PINEAPPLE,
    STRAWBERRY
}
