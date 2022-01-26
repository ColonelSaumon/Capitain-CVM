using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Représente les données de jeu
/// </summary>
public class PlayerData
{
    /// <summary>
    /// Représente le nombre de points de vie du personnage
    /// </summary>
    private int _vie;
    /// <summary>
    /// Représente le nombre d'énergie (entre 0 et 4)
    /// </summary>
    private int _energie;
    /// <summary>
    /// Représente le maximum d'énergie du personnage
    /// </summary>
    public const int MAX_ENERGIE = 4;
    /// <summary>
    /// Permet d'identifier les actions sur le UI à réaliser
    /// lors de la perte d'énergie
    /// </summary>
    public System.Action UIPerteEnergie;
    /// <summary>
    /// Permet d'identifier les actions sur le UI à réaliser
    /// lors de la perte d'énergie
    /// </summary>
    public System.Action UIPerteVie;
    /// <summary>
    /// Permet d'identifier les actions à réaliser lors d'un gameover
    /// </summary>
    public System.Action Gameover;

    public int Energie { get { return this._energie; } }
    public int Vie { get { return this._vie; } }

    public PlayerData()
    {
        this._vie = 0;
        this._energie = 0;
        this.UIPerteEnergie = null;
        this.UIPerteVie = null;
        this.Gameover = null;
    }

    public PlayerData(int vie = 1, int energie = 2,
        System.Action uiPerteEnergie = null, System.Action uiPerteVie = null, System.Action gameOver = null)
    {
        this._vie = vie;
        this._energie = energie;
        this.UIPerteEnergie += uiPerteEnergie;
        this.UIPerteVie += uiPerteVie;
        this.Gameover += gameOver;
    }

    /// <summary>
    /// Diminue l'énergie du personnage
    /// </summary>
    /// <param name="perte">Niveau de perte (par défaut 1)</param>
    public void DecrEnergie(int perte = 1)
    {
        this._energie -= perte;
        this.UIPerteEnergie();
        if (this._energie <= 0)
        {
            DecrVie();
            this.DecrVie();
        }
    }

    /// <summary>
    /// Permet de réduire la vie d'un personnage
    /// </summary>
    public void DecrVie()
    {
        this._vie--;
        this.UIPerteVie();
        if (this._vie <= 0)
            this.Gameover();
        else
            this.IncrEnergie(MAX_ENERGIE);
    }

    /// <summary>
    /// Permet d'augmenter l'énergie jusqu'à MAX_ENERGIE
    /// </summary>
    /// <param name="gain">Gain d'augmentation</param>
    public void IncrEnergie(int gain)
    {
        this._energie += gain;
        if (this._energie > MAX_ENERGIE)
        {
            this._energie = MAX_ENERGIE;
            this.UIPerteEnergie();
        }
    }

    /// <summary>
    /// Permet d'augmenter la vie
    /// </summary>
    /// <param name="gain">Gain d'augmentation</param>
    public void IncrVie(int gain)
    {
        this._vie += gain;
        this.UIPerteVie();
    }
}
