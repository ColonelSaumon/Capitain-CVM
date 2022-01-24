using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float _toleranceAngle = 45f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            float angle = Vector3.Angle(this.transform.up, collision.gameObject.transform.position);
            Debug.Log("Player contact, angle : " + angle);/* {collision point : " + pointCollision.ToString() + ", angle : " +
                (Vector3.Angle(this.transform.position, pointCollision) * 180 / Mathf.PI).ToString() + "}" );*/

            if (_toleranceAngle < angle && angle < _toleranceAngle + 90f)
                Debug.Log("Hit the head");
        }
    }
}
