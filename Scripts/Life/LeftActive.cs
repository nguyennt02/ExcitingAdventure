using UnityEngine;

public class LeftActive : MonoBehaviour, IActive
{
    public void Active(GameObject player)
    {
        player.GetComponent<IHealing>().Healing(1);
        Destroy(gameObject);
    }
}
