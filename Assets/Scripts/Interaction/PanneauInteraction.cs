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

        _messageBox.enabled = false;
    }

    public override void DoAction()
    {
        _messageBox.enabled = !_messageBox.enabled;
    }

    public override void ExitAction()
    {
        _messageBox.enabled = false;
    }
}
