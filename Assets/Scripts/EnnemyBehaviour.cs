using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnnemyBehaviour : MonoBehaviour
{
    /// <summary>
    /// Point de vie du personnage
    /// </summary>
    [SerializeField]
    private int _pv = 2;
    /// <summary>
    /// Angle de tolérange pour le calcul du saut sur la tête
    /// </summary>
    [SerializeField]
    private float _toleranceAngle = 1.5f;
    /// <summary>
    /// Décrit la durée de l'invulnaribilité
    /// </summary>
    public const float DelaisInvulnerabilite = 1f;
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
        if (this._pv <= 0)
        {
            _animator.SetTrigger("Destruction");
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.GetComponent<EnnemyPatrol>().enabled = false;
            GameObject.Destroy(this.gameObject, 0.5f);
        }

        if (Time.fixedTime > _tempsDebutInvulnerabilite + DelaisInvulnerabilite)
            _invulnerable = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            float angle = Vector3.Angle(this.transform.position, collision.contacts[0].point);

            if (angle < _toleranceAngle && !_invulnerable)
            {
                this._pv--;
                _animator.SetTrigger("DegatActif");
                _tempsDebutInvulnerabilite = Time.fixedTime;
                _invulnerable = true;
            }
        }
    }
}
