using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using TMPro;

public class CGDataStore : MonoBehaviourSingleton<CGDataStore>
{
    private JToken m_SeedJson;

    public bool m_LoadSaveGame = true;
    public TMP_Text m_DebugText;

    private string SaveDataPath
    {
        get
        {
            string saveFilePath = Application.persistentDataPath + "/ClimbingGym.json";
            return saveFilePath;
        }
    }

    public JToken SeedJson
    {
        get
        {
            if (m_SeedJson == null)
            {
                LoadSeedJSON();
            }

            return m_SeedJson;
        }
    }

    void Awake()
    {
        m_DebugText.text = "Awake";
        LoadSeedJSON();
        LoadSaveGame();
        SeedData();
    }

    void LoadSeedJSON()
    {
        m_DebugText.text = "Loading";
        string seedFilePath = Application.streamingAssetsPath + "/WallData.json";
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(seedFilePath);
            www.SendWebRequest();

            m_DebugText.text = "Waiting For Handler";
            while (!www.downloadHandler.isDone) { }
            m_DebugText.text = "Handler Done";
            m_SeedJson = JToken.Parse(www.downloadHandler.text);
        }
        if (File.Exists(seedFilePath))
        {
            string fileContents = File.ReadAllText(seedFilePath);
            m_SeedJson = JToken.Parse(fileContents);
            m_DebugText.text = "Loaded";
        }
    }

    public void SeedData()
    {
        if (m_SeedJson == null)
        {
            LoadSeedJSON();
        }

        CGWallBuilder.GetOrCreateInstance().LoadWallData(m_SeedJson);
        //TBLLogChannels.instance().LogChannel(TBLLogChannel.Debug, seedFilePath);
    }

    public void SaveGame()
    {
        JObject saveData = new JObject();
        //TBLReactionProcessor.GetOrCreateInstance().AppendJSON(ref saveData);

        File.WriteAllText(SaveDataPath, saveData.ToString());
    }

    void LoadSaveGame()
    {
        if (!m_LoadSaveGame)
        {
            return;
        }

        if (!File.Exists(SaveDataPath))
        {
            return;
        }

        string saveDataString = File.ReadAllText(SaveDataPath);
        if (saveDataString.Length > 0)
        {
            JObject saveData = JObject.Parse(saveDataString);
            if (saveData != null)
            {
                //TBLReactionProcessor.GetOrCreateInstance().LoadSavedAppliedSources(saveData);
            }
        }
    }
}