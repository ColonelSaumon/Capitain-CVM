using UnityEngine;
using UnityEngine.UI;

public class PanneauInteraction : BaseInteraction
{
    [SerializeField]
    private Canvas _messageBox;

    private void Start()
    {
        if (_messageBox == null)
            throw new MissingReferenceException("Le canvas doit être renseigné.");

        _messageBox.gameObject.SetActive(false);
    }

    public override void DoAction()
    {
        Debug.Log("DoAction");
        _messageBox.gameObject.SetActive(true);
    }

    public override void ExitAction()
    {
        _messageBox.gameObject.SetActive(false);
    }
}
