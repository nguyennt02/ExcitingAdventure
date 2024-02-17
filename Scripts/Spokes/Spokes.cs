using UnityEngine;

public class Spokes : MonoBehaviour, IActive
{
    public void Active(GameObject player)
    {
        player.GetComponent<IPlayerDie>().Die();
    }
}
