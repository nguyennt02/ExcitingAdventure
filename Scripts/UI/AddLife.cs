using UnityEngine;
using UnityEngine.UI;

public class AddLife : GameOver
{
    private static AddLife instance;
    public static AddLife Instance => instance;

    [SerializeField] Sprite _empty;
    [SerializeField] Sprite _full;

    private void Awake()
    {
        if (instance) Debug.LogError("Ton tai 1 AddLife", this);
        instance = this;
    }

    public void CreatMaxLife(int maxLife)
    {
        for (int i = 0; i < maxLife; i++)
        {
            GameObject newImageObject = new GameObject("NewImage", typeof(RectTransform), typeof(Image));
            newImageObject.transform.SetParent(transform, false);
            newImageObject.transform.localScale = Vector3.one * 0.25f;
            newImageObject.GetComponent<Image>().sprite = _full;
        }
    }
    public void SetLife(int life)
    {
        int i = 0;
        foreach(Transform children in transform)
        {
            if(i < life)
            {
                i++;
                children.GetComponent<Image>().sprite = _full;
            }
            else
            {
                children.GetComponent<Image>().sprite = _empty;
            }
        }
    }
    public override void SetUI()
    {
        var missions = GameManager.Instance.GetQuantityFruits();
        for (int i = 0; i < missions.Length; i++)
        {
            var spriteFurit = missions[i].furit.GetComponentInChildren<SpriteRenderer>().sprite;
            var textQuantity = "x" + missions[i].quantity.ToString() + "/" + missions[i].MaximumQuantity.ToString();
            arr_Image[i].sprite = spriteFurit;
            arr_Text[i].SetText(textQuantity);
        }
    }
}
