using System.Collections;
using UnityEngine;

public class FireActive : ObjActive
{
    [SerializeField] private float Delay;
    private ISfxSource sfxSource;
    protected override void Awake()
    {
        base.Awake();
        sfxSource = GetComponent<ISfxSource>();
    }
    public void OnFire()
    {
        StartCoroutine(Fireing());
    }
    private IEnumerator Fireing()
    {
        yield return new WaitForSeconds(Delay);
        _anim.SetBool("Active", false);
    }
    public void OnObjChildren()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        sfxSource.PlaySFX("Fire");
    }
    public void OffObjChildren()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        sfxSource.StopSFX();
    }
}
