using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour, IAudioCtrl
{
    private static GamePlay instance;
    public static GamePlay Instance => instance;

    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SfxSlider;

    public event Action ToggleMusic;
    public event Action ToggleSFX;
    public event Action<float> MusicVolume;
    public event Action<float> SFXVolume;

    private void Awake()
    {
        if (instance) Debug.LogError("GamePlay da ton tai", this);
        instance = this;
    }

    private void Start()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0);
        SfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0);
    }

    public void OnClickSetting()
    {
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
    }
    public void OnClickExit()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(false);
    }
    public void SetToggMusic() {
        ToggleMusic?.Invoke();
    }
    public void SetToggSFX()
    {
        ToggleSFX?.Invoke();
    }
    public void SetMusicVolume()
    {
        MusicVolume?.Invoke(MusicSlider.value);
    }
    public void SetSFXVolume()
    {
        SFXVolume?.Invoke(SfxSlider.value);
    }
}
public interface IAudioCtrl
{
    public event Action ToggleMusic;
    public event Action ToggleSFX;
    public event Action<float> MusicVolume;
    public event Action<float> SFXVolume;
}