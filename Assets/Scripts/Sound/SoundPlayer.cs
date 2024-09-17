using UnityEngine;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private AudioSource _fxSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _menuSource;

    public float MusicVolume
    {
        get
        {
            float  value;
            _mixer.GetFloat("MusicVolume", out value);
            return (value + 40) / 40;
        }
        set
        {
            _mixer.SetFloat("MusicVolume", -40 + 40 * value);
        }
    }

    public void PlayFX(AudioClip audio, bool loop = false)
    {
        Debug.Log(audio.name);
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

    public void StopMusic()
    {
        _musicSource.Stop();
        _musicSource.clip = null;
    }
    
    public void PlayMenu(AudioClip audio)
    {
        _menuSource.clip = audio;
        _menuSource.Play();
    }

}