using UnityEngine;

public class LeftActive : MonoBehaviour, IActive
{
    public void Active(GameObject player)
    {
        player.GetComponent<IHealing>().Healing(1);
        AudioMainManager.Instance.PlaySFX("Furit");
        Destroy(gameObject);
    }
}
