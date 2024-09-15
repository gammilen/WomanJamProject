using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class OpeningEffector : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _fadeScreen;
    [SerializeField] private SpriteRenderer _bg;
    [SerializeField] private SoundPlayer _soundPlayer;
    [SerializeField] private AudioClip _music;
    [SerializeField] private Transform _leftClouds;
    [SerializeField] private Transform _rightClouds;
    
    public float FadeOutDuration;
    
    // public event Action Finished;

    public async UniTaskVoid  PlayEffect(Action callback)
    {
        _soundPlayer.SetMusicVolume(1);
        _soundPlayer.PlayMusic(_music);
        
        Go.to(_fadeScreen, FadeOutDuration, new GoTweenConfig().colorProp("color", new Color(0, 0, 0, 0), false));
        await UniTask.Delay(TimeSpan.FromSeconds(FadeOutDuration), ignoreTimeScale: false);
        Go.to(_leftClouds, 1, new GoTweenConfig().position(new Vector3(-25, 0, 0), true));
        Go.to(_rightClouds, 1, new GoTweenConfig().position(new Vector3(25, 0, 0), true));
        
        // TODO: move clouds
        // Go.to(_bg.transform, ScaleUpDuration, new GoTweenConfig().scale(ScaleValue));
        await UniTask.Delay(TimeSpan.FromSeconds(2), ignoreTimeScale: false); // !!!
        var timer = 1f;
        var volumePart = _soundPlayer.GetMusicVolume();
        while (timer > 0)
        {
            _soundPlayer.SetMusicVolume(timer * volumePart);
            timer -= Time.deltaTime;
            await UniTask.DelayFrame(1);
        }

        _music.UnloadAudioData();
        
        callback();
    }

}