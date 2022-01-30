using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchelleInteraction : BaseInteraction
{
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
        
        InteractionUI.Instance.ActiveMessage("Quitter l'échelle");
        pm.InteractionAction -= DoAction;
        pm.InteractionAction += QuitterEchelle;
    }   

    public void QuitterEchelle()
    {
        Debug.Log("Quitte l'échelle");

        PlayerMouvement pm = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerMouvement>();
        pm.SetEnMonte(false);
        pm.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        pm.SetDirectionToZero();

        InteractionUI.Instance.ActiveMessage(this._messageInteraction);
        pm.InteractionAction -= QuitterEchelle;
        pm.InteractionAction += DoAction;
    }
}
