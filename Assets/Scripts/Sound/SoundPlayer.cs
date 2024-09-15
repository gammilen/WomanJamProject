using UnityEngine;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private AudioSource _fxSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _menuSource;


    public void PlayFX(AudioClip audio, bool loop = false)
    {
        _fxSource.loop = loop;
        _fxSource.clip = audio;
        _fxSource.Play();
        
    }

    public void StopFX()
    {
        _fxSource.Stop();
    }
    
    public void PlayMusic(AudioClip audio)
    {
        _musicSource.clip = audio;
        _musicSource.Play();
    }
    
    public void PlayMenu(AudioClip audio)
    {
        _menuSource.clip = audio;
        _menuSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        _mixer.SetFloat("MusicVolume", volume);
    }

    public float GetMusicVolume()
    {
        float  value;
        _mixer.GetFloat("MusicVolume", out value);
        return value;
    }


}