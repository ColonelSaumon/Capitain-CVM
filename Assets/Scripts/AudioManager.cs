using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Représente un élément audio avec un nom (étiquette)
/// </summary>
[System.Serializable]
struct AudioElement
{
    public string Nom;
    public AudioClip Clip;
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Représente la playliste de la trame sonore
    /// </summary>
    [SerializeField]
    private AudioElement[] _playlist;

    /// <summary>
    /// Référence vers l'audio source de l'objet
    /// </summary>
    private AudioSource _source;

    private void Start()
    {
        // Récupère la référence
        _source = this.gameObject.GetComponent<AudioSource>();
        // Joue la musique pour la scène
        AudioClip clip = null;
        foreach (AudioElement clipData in _playlist)
        {
            if (clipData.Nom.Equals(SceneManager.GetActiveScene().name))
                clip = clipData.Clip;
        }
        if (clip != null)
            _source.clip = clip;
        else
            _source.clip = _playlist[0].Clip;

        _source.Play();
    }
}
