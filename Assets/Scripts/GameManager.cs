using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Properties
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
    #endregion

    #region Methods
    private void Awake()
    {
        if (_instance != null)
            Destroy(this.gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            // Initialisation des données de jeu
            LoadPlayerData();
        }
    }

    private void Start()
    {
        List<string> cl = new List<string>();
        cl.Add("test_1");
        cl.Add("test_2");
        cl.Add("test_3");
        cl.Add("test_4");
        PlayerData test = new PlayerData(
            15, 42, 2022, ChestList: cl
            );
        string jdata = PlayerDataJson.WriteJson(test);
        Debug.Log(jdata);
        PlayerData read = PlayerDataJson.ReadJson(jdata);
        Debug.Log(read.Vie);
    }

    private void LoadPlayerData()
    {
        this._playerData = new PlayerData(4, 2);
    }

    private void Update()
    {
        //Debug.Log($"Score : {this._playerData.Score}");
        //Debug.Log($"Vie : {this._playerData.Vie}, Énergie : {this._playerData.Energie}");
        //string msg = string.Empty;
        //foreach (string chest in PlayerData.ListeCoffreOuvert)
        //{
        //    msg += chest + ";";
        //}
        //Debug.Log($"ChestList : {msg}");
    }
    #endregion
    public void RechargerNiveau()
    {
        this.PlayerData.UIPerteEnergie = null;
        this.PlayerData.UIPerteVie = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name,
            LoadSceneMode.Single);
    }
}
