using UnityEngine;



[System.Serializable]
public class SaveData
{
    public LevelData[] levelData;
    public PlayerData playerData;

    public SaveData()
    {
        playerData = new PlayerData();

        Level[] levels;
        levels = Resources.LoadAll<Level>("Levels");
        levelData = new LevelData[levels.Length];
        int x = 0;
        while (x < levels.Length)
        {
            levelData[x] = new LevelData();
            x++;
        }
    }
}