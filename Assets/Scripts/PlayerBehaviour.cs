using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    /// <summary>
    /// Décrit si l'entité est invulnérable
    /// </summary>
    private bool _invulnerable = false;
    /// <summary>
    /// Réfère à l'animator du GO
    /// </summary>
    private Animator _animator;
    /// <summary>
    /// Représente le moment où l'invulnaribilité a commencé
    /// </summary>
    private float _tempsDebutInvulnerabilite;

    private void Start()
    {
        _animator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.fixedTime > _tempsDebutInvulnerabilite + EnnemyBehaviour.DelaisInvulnerabilite)
            _invulnerable = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ennemy") && !_invulnerable)
        {
            _animator.SetTrigger("DegatActif");
            GameManager.Instance.PlayerData.DecrEnergie();
            _tempsDebutInvulnerabilite = Time.fixedTime;
            _invulnerable = true;
        }
    }
}
