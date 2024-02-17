using System.Collections;
using UnityEngine;

public class ObjParentActiveCtrl : MonoBehaviour
{
    [SerializeField] protected float _delay = 0;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Active(collision.gameObject));
        }
    }
    protected virtual IEnumerator Active(GameObject player)
    {
        yield return new WaitForSeconds(_delay);
        foreach (Transform children in transform.parent)
        {
            IActive Active = children.GetComponent<IActive>();
            if (Active != null)
            {
                Active.Active(player);
            }
        }
    }
}
