using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D)),
    RequireComponent(typeof(Animator)),
    RequireComponent(typeof(SpriteRenderer))]
public class PlayerMouvement : MonoBehaviour
{
    /// <summary>
    /// Référence vers l'animator
    /// </summary>
    private Animator _animator;
    /// <summary>
    /// Représente la direction actuelle du joueur
    /// </summary>
    [SerializeField]
    private Vector2 _direction = Vector2.zero;

    /// <summary>
    /// Représente la vitesse actuelle du joueur
    /// Limité entre 0 et 6 m/s
    /// </summary>
    [SerializeField, Range(0, 25)]
    private float _vitesse = 60f;

    /// <summary>
    /// Représente la vitesse de monté du personnage
    /// </summary>
    [SerializeField, Range(0, 25)]
    private float _vitesseMonte = 35f;

    /// <summary>
    /// Force du saut du joueur
    /// </summary>
    [SerializeField]
    private float _forceSaut = 300f;

    /// <summary>
    /// Trigger pour savoir si le personnage saute
    /// </summary>
    private bool _vaSaute = false;

    /// <summary>
    /// Référence vers le Rigidbody du GO
    /// </summary>
    private Rigidbody2D _rb;

    /// <summary>
    /// Détermine si le personne est au sol
    /// </summary>
    private bool _estAuSol;

    /// <summary>
    /// Référence au component SpriteRenderer
    /// </summary>
    private SpriteRenderer _sr;

    /// <summary>
    /// Delegate définissant l'action qui doit être exécuter à
    /// la demande du joueur
    /// </summary>
    public System.Action InteractionAction;

    /// <summary>
    /// Défini si le personne est dans l'action de monté
    /// </summary>
    [SerializeField]
    private bool _enMonte;

    void Start()
    {
        // Lie _rb au ridigbody
        _rb = this.gameObject.GetComponent<Rigidbody2D>();
        // Initialise _estAuSol
        _estAuSol = false;
        // Lie _animator à l'animator
        _animator = this.gameObject.GetComponent<Animator>();
        // Lie _sr à SpriteRenderer
        _sr = this.gameObject.GetComponent<SpriteRenderer>();
        // Au départ, aucune interaction
        InteractionAction = null;
        // Initialise _enMonte
        this._enMonte = false;
    }

    void Update()
    {
        this.transform.Translate(_direction *
            (_enMonte ? _vitesseMonte : _vitesse)
            * Time.deltaTime);

        if (!_enMonte)
        {
            // Établit la vitesse pour l'animator
            _animator.SetFloat("Speed", Mathf.Abs(_direction.x * _vitesse));

            if ((_direction.x < 0) != _sr.flipX)
                _sr.flipX = _direction.x <= 0;
        }
    }

    void FixedUpdate()
    {
        // Saut de l'utilisateur
        if (!_enMonte && _vaSaute && _estAuSol)
        {
            _rb.AddForce(transform.up * _forceSaut, ForceMode2D.Impulse);
            _vaSaute = false;
            _estAuSol = false;
        }
    }

    /// <summary>
    /// Méthode exécuté lorsque la personne utilisateur réalise l'InputAction Horizontal
    /// </summary>
    /// <param name="value">Valeur de l'input (ex. appuyer ou non)</param>
    public void OnHorizontal(InputValue value)
    {
        if (!_enMonte)
        {
            _direction.x = value.Get<float>();
            _direction.y = 0;
        }
    }

    /// <summary>
    /// Méthode exécuté lorsque la personne utilisateur réalise l'InputAction Vertical
    /// </summary>
    /// <param name="value">Valeur de l'input (ex. appuyer ou non)</param>
    public void OnVertical(InputValue value)
    {
        if (_enMonte)
        {
            _direction.x = 0;
            _direction.y = value.Get<float>();
        }
    }

    /// <summary>
    /// Permet de connaître le saut du personnage
    /// </summary>
    public void OnJump()
    {
        if (_estAuSol)
            _vaSaute = true;
    }

    /// <summary>
    /// Appelé lorsque le personnage entre en collision
    /// </summary>
    /// <param name="collision">Collider de l'autre GO</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _estAuSol = collision.gameObject.tag.Equals("Tilemap")
            || collision.gameObject.tag.Equals("Plateform");

        if (_enMonte && collision.gameObject.CompareTag("Tilemap"))
            OnAction();
    }

    public void OnAction()
    {
        if (InteractionAction != null)
            InteractionAction();
    }

    public void SetEnMonte(bool value = true)
    {
        this._enMonte = value;
    }

    public void SetDirectionToZero()
    {
        if (_enMonte)
            this._direction = Vector2.zero;
    }
}
