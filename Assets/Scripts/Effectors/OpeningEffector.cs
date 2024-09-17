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
    [SerializeField] private AnimationCurve _fadeOutCurve;
    [SerializeField] private AnimationCurve _musicOutCurve;
    
    
    public float FadeOutDuration;
    
    public float FadeScreenAlpha
    {
        get => _fadeScreen.color.a;
        set
        {
            var c = _fadeScreen.color;
            c.a = value;
            _fadeScreen.color = c;
        }
    }
    
    // public event Action Finished;

    public async UniTaskVoid  PlayEffect(Action callback)
    {
        await UniTask.DelayFrame(1);

        // _soundPlayer.MusicVolume = 1;
        _soundPlayer.PlayBg(_music);

        Go.to(this, FadeOutDuration, new GoTweenConfig().floatProp("FadeScreenAlpha", 0, false).setEaseCurve(_fadeOutCurve));
        //Go.to(_fadeScreen, FadeOutDuration, new GoTweenConfig().colorProp("color", new Color(0, 0, 0, 0), true));
        await UniTask.Delay(TimeSpan.FromSeconds(FadeOutDuration - 1.5), ignoreTimeScale: false);
        Go.to(_leftClouds, 2.5f, new GoTweenConfig().position(new Vector3(-25, 0, 0), true));
        Go.to(_rightClouds, 2.5f, new GoTweenConfig().position(new Vector3(25, 0, 0), true));
        
        // Go.to(_bg.transform, ScaleUpDuration, new GoTweenConfig().scale(ScaleValue));
        // await UniTask.Delay(TimeSpan.FromSeconds(2), ignoreTimeScale: false); // !!!
        Go.to(_soundPlayer, 2f, new GoTweenConfig().floatProp("MusicVolume", 0, false).setEaseCurve(_musicOutCurve).setDelay(0.5f));

        await UniTask.Delay(TimeSpan.FromSeconds(3), ignoreTimeScale: false); // !!!

        /*var timer = 2f;
        var volumePart = _soundPlayer.GetMusicVolume();
        while (timer > 0)
        {
            _soundPlayer.SetMusicVolume(timer * volumePart);
            timer -= Time.deltaTime;
            await UniTask.DelayFrame(1);
        }*/

        // _music.UnloadAudioData();
        // _soundPlayer.StopMusic();
        //_soundPlayer.MusicVolume = 1;
        
        
        callback();
    }

}