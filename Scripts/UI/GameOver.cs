using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] protected Image[] arr_Image;
    [SerializeField] protected TextMeshProUGUI[] arr_Text;

    protected virtual void Start()
    {
        SetUI();
    }

    public virtual void SetUI()
    {
        var missions = GameManager.Instance.GetQuantityFruits();
        for(int i = 0;i < missions.Length;i++)
        {
            var spriteFurit = missions[i].furit.GetComponentInChildren<SpriteRenderer>().sprite;
            var textQuantity = "x" + missions[i].quantity.ToString();
            arr_Image[i].sprite = spriteFurit;
            arr_Text[i].SetText(textQuantity);
        }
    }
}
