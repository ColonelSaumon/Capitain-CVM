using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyUpgrade : MonoBehaviour
{
    /// <summary>
    /// Valeur de l'énergie regagner au contact
    /// </summary>
    [SerializeField]
    private int _regainEnergie = 1;
    [SerializeField]
    private AudioClip _clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            GameManager.Instance.AudioManager
                .PlayClipAtPoint(_clip, this.transform.position);
            GameManager.Instance
                .PlayerData.IncrEnergie(this._regainEnergie);
            GameObject.Destroy(this.gameObject);
        }
    }
}
