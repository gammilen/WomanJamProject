using UnityEngine;

public class PhoneRinging : MonoBehaviour
{
    [SerializeField] private Transform _phone;

    public bool IsShaking;
    private GoTween _tween;

    public void Shake()
    {
        StopShake();
        _tween = Go.to(_phone, 5, new GoTweenConfig().shake(new Vector3(0, 0, 1), GoShakeType.Eulers));

    }

    public void StopShake()
    {
        if (_tween != null)
        {
            _tween.pause();
            _tween.goTo(0);
            _tween = null;
        }
    }

}