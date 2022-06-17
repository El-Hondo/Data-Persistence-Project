using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistanceManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static PersistanceManager Instance;
    public static string playerName;
    public static string highscorePlayerName;
    public static int highscore;
    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
        Load();
    }

    [System.Serializable]
    class SaveData
    {
        public string highscorePlayerName;
        public int highscore;
    }
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
            Application.Quit(); // original code to quit Unity player
    #endif
    }

    public static void Save()
    {
        SaveData data = new SaveData();
        data.highscorePlayerName = playerName;
        data.highscore = highscore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public static void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highscorePlayerName = data.highscorePlayerName;
            highscore = data.highscore;
        }
    }
}
