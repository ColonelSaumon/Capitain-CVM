using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchelleInteraction : BaseInteraction
{
    [SerializeField]
    private BoxCollider2D _colliderPlateform;

    public override void ExitAction() { QuitterEchelle(); }

    public override void DoAction()
    {

        // Placer le code pour l'action ici
        Debug.Log("Monter à l'échelle");

        PlayerMouvement pm = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerMouvement>();
        pm.SetEnMonte(true);
        Rigidbody2D rb = pm.gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = new Vector2(rb.velocity.x, 0);

        _colliderPlateform.enabled = false;

        pm.gameObject.GetComponent<Animator>().SetBool("EnMonte", true);
        
        InteractionUI.Instance.ActiveMessage("Quitter l'échelle");
        pm.InteractionAction -= DoAction;
        pm.InteractionAction += QuitterEchelle;
    }   

    public void QuitterEchelle()
    {
        Debug.Log("Quitte l'échelle");

        PlayerMouvement pm = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerMouvement>();
        pm.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        pm.SetDirectionToZero();
        pm.SetEnMonte(false);

        _colliderPlateform.enabled = true;

        pm.gameObject.GetComponent<Animator>().SetBool("EnMonte", false);

        InteractionUI.Instance.ActiveMessage(this._messageInteraction);
        pm.InteractionAction -= QuitterEchelle;
        pm.InteractionAction += DoAction;
    }
}
