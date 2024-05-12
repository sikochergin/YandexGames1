using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

[System.Serializable]
public class ProgressData
{
    public int PALevel = 0;
    public int AMlevel = 0;
    public int Walls = 0;
    public int Money = 0;
}

public class Progress : MonoBehaviour
{
    public ProgressData CurrentProgressData;

    public static Progress Instance;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [ContextMenu("Save")]
    public void Save()
    {
        string json = JsonUtility.ToJson(CurrentProgressData);

        YandexGame.savesData.DataJson = json;
        YandexGame.SaveProgress();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        YandexGame.LoadProgress();
        string json = YandexGame.savesData.DataJson;

        if (string.IsNullOrEmpty(json))
        {
            CurrentProgressData = new ProgressData();
        }
        else
        {
            CurrentProgressData = JsonUtility.FromJson<ProgressData>(json);
        }
    }
}