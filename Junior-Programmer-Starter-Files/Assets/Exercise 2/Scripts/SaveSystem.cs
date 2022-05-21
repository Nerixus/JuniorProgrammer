using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    public string username = "";
    public int highScore = 0;

    public delegate void HandleGameLoaded(string v_username, int v_highScore);
    public static event HandleGameLoaded OnGameLoaded;

    int deleteCount = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string username;
        public int highScore;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            deleteCount++;
            if (deleteCount == 3)
            {
                DeleteCurrentData();
                PlayerPrefs.DeleteAll();
            }
        }
    }

    public void SaveGameData()
    {
        SaveData data = new SaveData();
        data.username = username;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            username = data.username;
            highScore = data.highScore;
            OnGameLoaded?.Invoke(username, highScore);
        }
    }

    void DeleteCurrentData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
