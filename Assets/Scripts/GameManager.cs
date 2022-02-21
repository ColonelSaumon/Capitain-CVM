using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    private AudioManager _audioManager;
    public AudioManager AudioManager { get { return _audioManager; } }
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
            _audioManager = this.GetComponent<AudioManager>();
        }
    }

    private void Start()
    {
        SaveData();
        SceneManager.activeSceneChanged += ChangementScene;
        ChangementScene(new Scene(), SceneManager.GetActiveScene());
        //List<string> cl = new List<string>();
        //cl.Add("test_1");
        //cl.Add("test_2");
        //cl.Add("test_3");
        //cl.Add("test_4");
        //PlayerData test = new PlayerData(
        //    15, 42, 2022, ChestList: cl
        //    );
        //string jdata = PlayerDataJson.WriteJson(test);
        //Debug.Log(jdata);
        //PlayerData read = PlayerDataJson.ReadJson(jdata);
        //Debug.Log(read.Vie);
    }

    public void SaveData()
    {
        StartCoroutine(SaveData(this.PlayerData));
    }

    public IEnumerator SaveData(PlayerData data)
    {
        using (StreamWriter stream = new StreamWriter(
            Path.Combine(Application.persistentDataPath, "savedata_encrypt.json"),
            false, System.Text.Encoding.UTF8))
        {
            DataManipulator manipulator = new DataManipulator();
            stream.Write(manipulator.Encrypt(PlayerDataJson.WriteJson(data)));
            Debug.Log(Path.Combine(Application.persistentDataPath, "savedata_encrypt.json"));
        }
        yield return new WaitForEndOfFrame();
    }

    private void LoadPlayerData()
    {
        string path = Path.Combine(Application.persistentDataPath, "savedata_encrypt.json");
        if (File.Exists(path))
        {
            using (StreamReader stream = new StreamReader(path,
            System.Text.Encoding.UTF8))
            {
                DataManipulator manipulator = new DataManipulator();
                this._playerData = PlayerDataJson.ReadJson(manipulator.Decrypt(stream.ReadToEnd()));
            }
            //DataManipulator manipulator = new DataManipulator();
            //this._playerData = manipulator.Decrypt(path);
        }
        else
        {
            this._playerData = new PlayerData(4, 2);
            SaveData();
        }
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
        //Debug.Log($"Volume général : {_playerData.VolumeGeneral}, Volume musique : {_playerData.VolumeMusique}, Volume effets : {_playerData.VolumeEffet}");
    }

    public void RechargerNiveau()
    {
        this.PlayerData.UIPerteEnergie = null;
        this.PlayerData.UIPerteVie = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name,
            LoadSceneMode.Single);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void ChangerScene(string nomScene)
    {
        _audioManager.StopAudio(0.3f);
        GameObject.Find("Fondu").SetActive(true);
        SceneManager.LoadScene(nomScene);
    }

    public void ChangementScene(Scene current, Scene next)
    {
        GameObject.Find("Fondu").SetActive(false);
    }

    #endregion
}
