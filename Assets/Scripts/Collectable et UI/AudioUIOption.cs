using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioUIOption : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer;
    [SerializeField]
    private string _parameter;

    private Slider _slider;

    private void Start()
    {
        _slider = this.gameObject.GetComponent<Slider>();
        _slider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float value)
    {
        _audioMixer.SetFloat(_parameter, _slider.value);
    }
}
