using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoeurUIManager : MonoBehaviour
{
    /// <summary>
    /// Image utiliser pour une barre pleine
    /// </summary>
    [SerializeField]
    private Sprite _barrePleine;
    /// <summary>
    /// Image utiliser pour une barre vide
    /// </summary>
    [SerializeField]
    private Sprite _barreVide;
    /// <summary>
    /// Texte utiliser pour afficher le nombre de vie
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _texteVie;
    /// <summary>
    /// Liste des spritesRenderer liées aux barres d'énergie
    /// </summary>
    [SerializeField]
    private Image[] _barreEnergie;

    // Start is called before the first frame update
    void Start()
    {
        ModifierEnergie();
        ModifierVie();

        GameManager.Instance.PlayerData.UIPerteEnergie += ModifierEnergie;
        GameManager.Instance.PlayerData.UIPerteVie += ModifierVie;
    }

    /// <summary>
    /// Permet de modifier l'affichage selon le nombre d'énergie actuel
    /// </summary>
    public void ModifierEnergie()
    {
        int Energie = GameManager.Instance.PlayerData.Energie;
        for (int i = 0; i < PlayerData.MAX_ENERGIE; i++)
        {
            if (i < Energie)
                this._barreEnergie[i].sprite = this._barrePleine;
            else
                this._barreEnergie[i].sprite = this._barreVide;
        }
    }

    public void ModifierVie()
    {
        this._texteVie.text = GameManager.Instance.PlayerData.Vie.ToString();
    }
}
