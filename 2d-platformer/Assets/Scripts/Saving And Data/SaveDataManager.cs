using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class SaveDataManager
{
    public SaveData saveData;

    public bool newGame;

    private static SaveDataManager _current;
    public static SaveDataManager current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveDataManager();
            }
            return _current;
        }
        set
        {
            if (value != null)
            {
                _current = value;
            }
        }
    }

    public void SetupData()
    {
        SaveDataManager save = (SaveDataManager)SavingSerialization.loadPlayerData(Application.persistentDataPath + "/savedata/data.txt");
        if (save == null)
        {
            Debug.Log("Previous Save Data not found!");
            Debug.Log("Creating Fresh Data...");
            saveData = new SaveData();
            current.newGame = true;
        }
        else
        {
            Debug.Log("Previous Save Data Found!");
            current = save;
            saveData = save.saveData;
            current.newGame = false;
        }
    }

    public bool LoadData()
    {
        SaveDataManager save = (SaveDataManager)SavingSerialization.loadPlayerData(Application.persistentDataPath + "/savedata/data.txt");
        if (save == null)
        {
            Debug.Log("Unable to load save file!");
            return false;
        }
        else
        {
            Debug.Log("Loading Saved Data...");
            current = save;
            current.newGame = false;
            return true;
        }
    }

    public void SaveData()
    {
        SavingSerialization.SavePlayerData("data", current);
    }

    public void AddRunData(int id, KindleRun run)
    {
        saveData.levelData[id].AddRun(run);
    }


}