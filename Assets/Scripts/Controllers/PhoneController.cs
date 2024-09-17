using System;
using Cysharp.Threading.Tasks;
using RyanNielson.InputBinder;
using UnityEngine;

public class PhoneController : MonoBehaviour
{
    [SerializeField] private GameObject _phoneNormal;
    [SerializeField] private GameObject _phonePicked;
    [SerializeField] private SoundPlayer _soundPlayer;
    [SerializeField] private AudioClip _ringSound;
    [SerializeField] private AudioClip _pickSound;

    [SerializeField] private PhoneRinging _phoneRinging;

    [SerializeField] private InputBinder _binder;

    public event Action MoveNextEvent;
    private bool _isRinging;
    private void Start()
    {
        _binder.BindKey(KeyCode.Mouse0, InputEvent.Pressed, HandleClick);

    }

    public void StartRinging()
    {
        _isRinging = true;
        _soundPlayer.PlayFX(_ringSound, true);
        _phoneRinging.Shake();
    }

    public void SetPhoneNormal(bool normal)
    {
        _phoneNormal.SetActive(normal);
        _phonePicked.SetActive(!normal);
    }

    private void HandleClick()
    {
        if (_isRinging)
        {
            PickUp().Forget();

        }
    }

    private async UniTaskVoid PickUp()
    {
        _isRinging = false;
        _soundPlayer.StopFX();
        _soundPlayer.PlayFX(_pickSound, false);
        _phoneRinging.StopShake();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        SetPhoneNormal(false);
        MoveNextEvent?.Invoke();
    }

    public void PutDown()
    {
        SetPhoneNormal(true);
        _soundPlayer.PlayFX(_pickSound, false);
    }
    
    
        
}