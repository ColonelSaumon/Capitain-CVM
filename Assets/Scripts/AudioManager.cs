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
        SceneManager.activeSceneChanged += ChangementScene;
        // Pour l'exécuter imédiatement
        ChangementScene(new Scene(), SceneManager.GetActiveScene());
    }

    void ChangementScene(Scene current, Scene next)
    {
        // Joue la musique pour la scène
        AudioClip clip = null;
        foreach (AudioElement clipData in _playlist)
        {
            if (clipData.Nom.Equals(next.name))
                clip = clipData.Clip;
        }
        if (clip != null)
            _source.clip = clip;
        else
            _source.clip = _playlist[0].Clip;

        Play(0.3f);
    }

    public void StopAudio(float delay = 0)
    {
        if (delay == 0)
            _source.Stop();
        else
            StartCoroutine(FadeOut(delay));
    }

    private IEnumerator FadeOut(float FadeTime)
    {
        float startVolume = _source.volume;

        while (_source.volume > 0)
        {
            _source.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        _source.Stop();
        _source.volume = startVolume;
    }

    private IEnumerator FadeIn(float FadeTime)
    {
        float maxVolume = _source.volume;
        _source.volume = 0;
        _source.Play();

        while (_source.volume < maxVolume)
        {
            _source.volume += Time.deltaTime / FadeTime;

            yield return null;
        }
    }

    public void Play(float delay = 0)
    {
        if (delay == 0)
            _source.Play();
        else
            StartCoroutine(FadeIn(delay));
    }
}
