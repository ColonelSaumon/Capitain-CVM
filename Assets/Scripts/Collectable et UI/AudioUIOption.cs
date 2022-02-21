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
    private bool _valeurChanger;

    private void Start()
    {
        _slider = this.gameObject.GetComponent<Slider>();
        _slider.onValueChanged.AddListener(ChangeVolume);
        _valeurChanger = false;
    }

    public void ChangeVolume(float value)
    {
        _audioMixer.SetFloat(_parameter, _slider.value);
        _valeurChanger = true;
    }

    // Doit être appeler par le bouton de retour
    public void ChangeParameters()
    {
        if (_valeurChanger)
        {
            float value;
            _audioMixer.GetFloat(_parameter, out value);

            switch (_parameter)
            {
                case "Master" :
                    GameManager.Instance.PlayerData.VolumeGeneral = value;
                    GameManager.Instance.SaveData();
                    break;
                case "Musique":
                    GameManager.Instance.PlayerData.VolumeMusique = value;
                    GameManager.Instance.SaveData();
                    break;
                case "Effets":
                    GameManager.Instance.PlayerData.VolumeEffet = value;
                    GameManager.Instance.SaveData();
                    break;
            }
        }
    }
}
