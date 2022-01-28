using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Référence au GameManager
    /// </summary>
    private static GameManager _instance;
    /// <summary>
    /// Permet d'accès à l'instance en cours du GameManager
    /// </summary>
    public static GameManager Instance { get { return _instance; } }
    /// <summary>
    /// Contient les données de jeu
    /// </summary>
    private PlayerData _playerData;
    public PlayerData PlayerData { get { return _playerData; } }

    private void Awake()
    {
        if (_instance != null)
            DestroyImmediate(this.gameObject);

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        // Initialisation des données de jeu
        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        this._playerData = new PlayerData(4, 2);
    }

    private void Update()
    {
        Debug.Log($"Score : {this._playerData.Score}");
    }
}
