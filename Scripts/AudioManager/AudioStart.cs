
using UnityEngine;

public class AudioStart : AudioManager
{
    private static AudioStart instance;
    public static AudioStart Instance => instance;

    private void Awake()
    {
        if (instance) Debug.LogError("AudioStart da ton tai", this);
        instance = this;
    }
    protected override void AddEven(){}
    protected override void OnDestroy(){}
}
