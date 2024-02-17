using System.Collections;
using UnityEngine;

public class ObjActiveCtrl : ObjParentActiveCtrl
{
    protected override IEnumerator Active(GameObject player)
    {
        yield return new WaitForSeconds(_delay);
        IActive Active = GetComponent<IActive>();
        if (Active != null)
        {
            Active.Active(player);
        }
    }
}
