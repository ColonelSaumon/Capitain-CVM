using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMouvement : MonoBehaviour
{
    /// <summary>
    /// Représente la direction actuelle du joueur
    /// </summary>
    [SerializeField]
    private Vector2 _direction = Vector2.right;
    /// <summary>
    /// Représente la vitesse actuelle du joueur
    /// Limité entre 0 et 6 m/s
    /// </summary>
    [SerializeField]
    [Range(0, 400)]
    private float _vitesse = 60f;
    /// <summary>
    /// Référence vers le Rigidbody du GO
    /// </summary>
    private Rigidbody2D _rb;
    /// <summary>
    /// Vitesse actuelle, utilisé par Unity
    /// </summary>
    private Vector3 velocity = Vector3.zero;
    /// <summary>
    /// Durée du fondu d'un Vector3, utilisé par Unity
    /// </summary>
    private const float SMOOTH_TIME = .05f;

    void Start()
    {
        // Lie _rb au ridigbody
        _rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 targetVelocity = _direction * _vitesse * Time.deltaTime;
        _rb.velocity = Vector3.SmoothDamp(_rb.velocity,
            targetVelocity, ref velocity, SMOOTH_TIME);
    }
}
