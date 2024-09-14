using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer _source;

    private const string Volume = "Volume";
    public void UpdateVolume(float value)
    {
        _source.SetFloat(Volume, value);
    }

}