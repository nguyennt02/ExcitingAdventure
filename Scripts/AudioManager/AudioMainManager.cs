using UnityEngine;

public class AudioMainManager : AudioManager
{
    private static AudioMainManager instance;
    public static AudioMainManager Instance => instance;

    private void Awake()
    {
        if (instance) Debug.LogError("AudioMainManager da ton tai", this);
        instance = this;
    }
}
