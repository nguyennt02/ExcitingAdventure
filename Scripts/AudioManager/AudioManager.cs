using UnityEngine;
using System;

public class AudioManager : MonoBehaviour, IMusicSource, ISfxSource
{
    [SerializeField] protected Sound[] musicSounds, sfxSounds;
    [SerializeField] protected AudioSource musicSource, sfxSource;

    protected virtual void Start()
    {
        MusicVolume(PlayerPrefs.GetFloat("MusicVolume",0));
        SFXVolume(PlayerPrefs.GetFloat("SFXVolume", 0));
    }

    protected virtual void OnEnable()
    {
        GamePlay.Instance.ToggleMusic += ToggleMusic;
        GamePlay.Instance.ToggleSFX += ToggleSFX;
        GamePlay.Instance.MusicVolume += MusicVolume;
        GamePlay.Instance.SFXVolume += SFXVolume;
    }
    protected virtual void OnDisable()
    {
        GamePlay.Instance.ToggleMusic -= ToggleMusic;
        GamePlay.Instance.ToggleSFX -= ToggleSFX;
        GamePlay.Instance.MusicVolume -= MusicVolume;
        GamePlay.Instance.SFXVolume -= SFXVolume;
    }

    public virtual void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null) Debug.LogError("Khong ton tai sound ten " + name);
        musicSource.clip = s.clip;
        musicSource.Play();
    }
    public virtual void StopMusic()
    {
        musicSource.Stop();
    }
    public virtual void StopSFX()
    {
        sfxSource.Stop();
    }
    public virtual void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null) Debug.LogError("Khong ton tai sound ten " + name);
        sfxSource.PlayOneShot(s.clip);
    }
    public virtual void ToggleMusic()
    {
        if (musicSource == null) return;
        musicSource.mute = !musicSource.mute;
    }

    public virtual void ToggleSFX()
    {
        if (sfxSource == null) return;
        sfxSource.mute = !sfxSource.mute;
    }

    public virtual void MusicVolume(float value)
    {
        if (musicSource == null) return;
        musicSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public virtual void SFXVolume(float value)
    {
        if (sfxSource == null) return;
        sfxSource.volume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}
public interface IMusicSource
{
    void PlayMusic(string name);
    void StopMusic();
}
public interface ISfxSource
{
    void PlaySFX(string name);
    void StopSFX();
}