using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

/// <summary>
/// Représente les différents contrôleurs
/// qui peut être lu dans le programme
/// </summary>
public enum ControlerType
{
    XBOX_GAMEPAD, DUALSHOCK_GAMEPAD,
    SWITCH_GAMEPAD, IOS_GAMEPAD, KEYBOARD
}

/// <summary>
/// Permet de lier un sprite avec un type
/// de controler
/// </summary>
[System.Serializable]
public struct ControlerSprite
{
    public ControlerType Controler;
    public Sprite Sprite;
}

[RequireComponent(typeof(Image))]
public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance { get; private set;}

    /// <summary>
    /// Lien vers l'enfant contenant le texte
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _textInteraction;

    /// <summary>
    /// Lien vers l'enfant contenant le texte du controle
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _textKeyboardControl;

    /// <summary>
    /// Liste des sprites à utiliser selon le type
    /// de contrôler
    /// </summary>
    [SerializeField]
    private List<ControlerSprite> _sprites;

    /// <summary>
    /// Défini le contrôle à retrouver sur un clavier
    /// </summary>
    [SerializeField]
    private Key _keyboardControlActionName;

    private Image _image;

    private void Start()
    {
        InteractionUI.Instance = this;

        _image = this.gameObject.GetComponent<Image>();

        if (_textInteraction == null || _textKeyboardControl == null)
            throw new MissingReferenceException("Un ou des enfants sont manquants");

        if (Keyboard.current != null)
        {
            InputSystem.onDeviceChange +=
                (device, change) =>
                {
                    //if (device.name.Contains("Keyboard"))
                    //    this.SetKeyboardControlText();
                    InteractionUI.Instance.UpdateUIButtonLayout();
                };
            //this.SetKeyboardControlText();
            this.UpdateUIButtonLayout();
        }

        DesactiveMessage();
    }

    public void UpdateUIButtonLayout()
    {
        SetKeyboardControlText();
        SetSpriteFromControl();
        this._textKeyboardControl.enabled = (Gamepad.current == null);
    }

    public void SetKeyboardControlText()
    {
        if (Keyboard.current != null)
        {
            UnityEngine.InputSystem.Controls.KeyControl control
                = Keyboard.current.FindKeyOnCurrentKeyboardLayout(_keyboardControlActionName.ToString());
            if (control != null)
            {
                string[] path = control.path.Split('/');
                _textKeyboardControl.text = path[path.Length - 1].ToUpper();
            } else
                _textKeyboardControl.text = string.Empty;
        }
        else
            _textKeyboardControl.text = string.Empty;
    }

    public void SetSpriteFromControl()
    {
        this._image.sprite = GetSpriteFromControler();
    }

    /// <summary>
    /// Récupère le sprite à utiliser selon le controler
    /// et le texte à ajouter au besoin
    /// </summary>
    /// <returns>Le sprite à afficher</returns>
    private Sprite GetSpriteFromControler()
    {
        if (Gamepad.current == null)
        {
            Sprite keyboard = _sprites.Find(x => x.Controler.Equals(ControlerType.KEYBOARD)).Sprite;
            return keyboard;
        }
        
        string type = Gamepad.current.GetType().ToString();

        if (type.Contains("XInput"))
            return _sprites.Find(x => x.Controler.Equals(ControlerType.XBOX_GAMEPAD)).Sprite;
        else if (type.Contains("DualShock"))
            return _sprites.Find(x => x.Controler.Equals(ControlerType.DUALSHOCK_GAMEPAD)).Sprite;
        else if (type.Contains("SwitchPro"))
            return _sprites.Find(x => x.Controler.Equals(ControlerType.SWITCH_GAMEPAD)).Sprite;
        else if (type.Contains("iOSGame"))
            return _sprites.Find(x => x.Controler.Equals(ControlerType.IOS_GAMEPAD)).Sprite;
        else return null;
    }

    /// <summary>
    /// Affiche le GO avec le message
    /// </summary>
    /// <param name="message">Message à afficher</param>
    public void ActiveMessage(string message)
    {
        this._textInteraction.text = message;
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// Désactive le GO
    /// </summary>
    public void DesactiveMessage()
    {
        this.gameObject.SetActive(false);
    }
}
