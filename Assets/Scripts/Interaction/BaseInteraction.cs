using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteraction : MonoBehaviour
{
    /// <summary>
    /// Message qui sera afficher pour interactig
    /// </summary>
    [SerializeField]
    protected string _messageInteraction = "Interagir";

    /// <summary>
    /// Permet de lancer l'action à utiliser
    /// </summary>
    public abstract void DoAction();

    /// <summary>
    /// Défini l'action à réaliser
    /// </summary>
    //protected abstract void InternalAction();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InteractionUI.Instance.ActiveMessage(this._messageInteraction);
            collision.gameObject.GetComponent<PlayerMouvement>().InteractionAction
                += this.DoAction;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InteractionUI.Instance.DesactiveMessage();
            collision.gameObject.GetComponent<PlayerMouvement>().InteractionAction
                -= this.DoAction;
        }
    }
}
