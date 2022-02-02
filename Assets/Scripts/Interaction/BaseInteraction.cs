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
    /// Trigger qui indique si l'interaction est possible
    /// </summary>
    private bool _peutInteragir = true;

    /// <summary>
    /// Permet d'arrêter l'interaction avec le GO
    /// </summary>
    /// <param name="ActionVisible">
    /// Détermine si l'action doit être visible (cache le UI d'interaction)
    /// </param>
    public void ArreterInteraction(bool ActionVisible = true)
    {
        _peutInteragir = false;
        if (ActionVisible)
            InteractionUI.Instance.DesactiveMessage();
    }

    /// <summary>
    /// Permet de lancer l'action à utiliser
    /// </summary>
    public abstract void DoAction();

    /// <summary>
    /// Définit l'action à réaliser lors que l'on quitte
    /// le collider
    /// </summary>
    public virtual void ExitAction() { return; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_peutInteragir) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            InteractionUI.Instance.ActiveMessage(this._messageInteraction);
            collision.gameObject.GetComponent<PlayerMouvement>().InteractionAction
                += this.DoAction;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_peutInteragir) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            ExitAction();
            InteractionUI.Instance.DesactiveMessage();
            collision.gameObject.GetComponent<PlayerMouvement>().InteractionAction = null;
        }
    }
}
