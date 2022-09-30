using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string filePath;
    private StreamReader sreader;
    private StreamWriter swriter;
    private string fileContent;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;

        // Has Saved Data
        if (HasSavedData())
        {
            sreader = new StreamReader(Application.persistentDataPath + "/" + filePath);
            fileContent = sreader.ReadToEnd();
            sreader.Close();
            PlayerDataJSON toLoadData = new PlayerDataJSON();
            toLoadData = JsonUtility.FromJson<PlayerDataJSON>(fileContent);
            PlayerData.levelsStats = JsonConvert.DeserializeObject<Dictionary<string, LevelStats>>(toLoadData.levelsStats);
            PlayerData.usedParchments = toLoadData.usedParchments;
            PlayerData.parchmentsQuantity = toLoadData.parchmentsQuantity;
        }
        else
        {
            InitializePlayerData();
            SaveData();
        }
    }

    void InitializePlayerData()
    {
        PlayerData.levelsStats = new Dictionary<string, LevelStats>();
        PlayerData.usedParchments = 0;
        PlayerData.parchmentsQuantity = 22;
    }

    public void UpdateData()
    {
        int newParchmentsQuantity = 22;
        int newParchmentsExpended = 0;
        foreach (var level in PlayerData.levelsStats)
        {
            newParchmentsExpended += level.Value.usedPieces;
            newParchmentsQuantity += level.Value.collectedPieces;
        }
        PlayerData.usedParchments = newParchmentsExpended;
        PlayerData.parchmentsQuantity = newParchmentsQuantity;
    }

    public void SaveData()
    {
        //Persistencia de datos con archivo, guardado de datos
        Debug.Log("FilePath: " + Application.persistentDataPath + "/" + filePath);
        swriter = new StreamWriter(Application.persistentDataPath + "/" + filePath, false);
        PlayerDataJSON toSaveData = new PlayerDataJSON();
        toSaveData.levelsStats = JsonConvert.SerializeObject(PlayerData.levelsStats); ;
        toSaveData.usedParchments = PlayerData.usedParchments;
        toSaveData.parchmentsQuantity = PlayerData.parchmentsQuantity;
        fileContent = JsonUtility.ToJson(toSaveData);
        Debug.Log(fileContent);
        swriter.Write(fileContent);
        swriter.Close();
    }

    bool HasSavedData()
    {
        return File.Exists(Application.persistentDataPath + "/" + filePath);
    }
    [System.Serializable]
    class PlayerDataJSON
    {
        public string levelsStats;
        public int usedParchments;
        public int parchmentsQuantity;
    }
}

