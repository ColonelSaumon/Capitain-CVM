using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button)),
    RequireComponent(typeof(Image))]
public class ButtonChangingImage : MonoBehaviour
{
    [SerializeField]
    private Sprite _stateOn;
    [SerializeField]
    private Sprite _stateOff;
    [SerializeField]
    private bool _state = false;
    [SerializeField]
    Button.ButtonClickedEvent _onClick;

    private Button _button;
    private Image _sprite;

    // Start is called before the first frame update
    void Start()
    {
        _button = this.gameObject.GetComponent<Button>();
        _sprite = this.gameObject.GetComponent<Image>();
        _button.onClick.AddListener(ChangeState);
        _sprite.sprite = (_state ? _stateOn : _stateOff);
    }

    public void ChangeState()
    {
        _sprite.sprite = (!_state ? _stateOn : _stateOff);
        _state = !_state;
        _onClick.Invoke();
    }
}
