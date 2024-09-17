using System;
using Cysharp.Threading.Tasks;
using RyanNielson.InputBinder;
using TMPro;
using UnityEngine;


// TODO: create man view and use it here and in ManSentenceEffector
public class ManPresentController : MonoBehaviour
{
    [SerializeField] private GameObject _manBio;
    [SerializeField] private TMP_Text _manBioText;
    [SerializeField] private SpriteRenderer _manView;
    [SerializeField] private SoundPlayer _soundPlayer;
    
    [SerializeField] private ParticleSystem _heavenEffect;
    [SerializeField] private ParticleSystem _hellEffect;
    [SerializeField] private ParticleSystem _hellEffect2;
    [SerializeField] private AudioClip _hellSound;
    [SerializeField] private AudioClip _heavenSound;
    
    [SerializeField] private InputBinder _binder;
    
    public float ManViewAlpha
    {
        get => _manView.color.a;
        set
        {
            var c = _manView.color;
            c.a = value;
            _manView.color = c;
        }
    }
    
    public event Action MoveNextEvent;
    private bool _presentingMan;
    private bool _isOutsideOfScreen;

    private Vector3 _initPosition;
    
    private void Start()
    {
        _binder.BindKey(KeyCode.Mouse0, InputEvent.Pressed, HandleClick);
        _initPosition = _manView.transform.position;
    }

    private void HandleClick()
    {
        if (_presentingMan)
        {
            _presentingMan = false;
            MoveNext();
        }
        
    }

    public async UniTaskVoid ShowResult(Action callback)
    {
        _presentingMan = false;
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        if (GameCore.Instance.Data.Decisions[^1].Decision)
        {
            _heavenEffect.Play();
            _soundPlayer.PlayMusic(_heavenSound);
            
            Go.to(this, 3, new GoTweenConfig().floatProp("ManViewAlpha", 0, false).setDelay(0.5f));
        }
        else
        {
            _hellEffect.Play();
            _hellEffect2.Play();
            _soundPlayer.PlayMusic(_hellSound);

            Go.to(_manView.transform, 3, new GoTweenConfig().position(new Vector3(0,-15,1), true));

        }
        _isOutsideOfScreen = true;
        await UniTask.Delay(TimeSpan.FromSeconds(4));
        _heavenEffect.Stop();
        _hellEffect.Stop();
        _hellEffect2.Stop();
        callback();
    }

    public async UniTaskVoid Present(MenData.ManData data)
    {
        await UniTask.DelayFrame(5);
        _presentingMan = true;
        //_manView.transform.position = new Vector3();  
        _manBio.SetActive(true);
        _manBioText.text = data.Bio;
        _manView.sprite = data.View;
        ManViewAlpha = 1;

        if (_isOutsideOfScreen)
        {
            _manView.transform.position = _initPosition + new Vector3(15, -0.56f, 1);
            _isOutsideOfScreen = false;
            Go.to(_manView.transform, 1, new GoTweenConfig().position(new Vector3(0.3f,-0.56f,1)));
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }
    }

    private void MoveNext()
    {
        _manBio.SetActive(false);
        MoveNextEvent?.Invoke();
    }
}