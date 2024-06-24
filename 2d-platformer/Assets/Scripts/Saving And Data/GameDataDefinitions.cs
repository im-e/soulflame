using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KindleRun
{
    public float time;
    public int kindleLevel;

    public KindleRun(float time, int kindleLevel)
    {
        this.time = time;
        this.kindleLevel = kindleLevel;
    }
}

[System.Serializable]
public class LevelData
{

    public List<KindleRun> kindleHistory;
    public KindleRun bestKindle;

    public LevelData()
    {
        KindleRun run = new KindleRun(999, 0);
        kindleHistory = new List<KindleRun> { run };
        bestKindle = run;
    }

    public void AddRun(KindleRun run)
    {
        kindleHistory.Add(run);
        if (run.time < bestKindle.time) bestKindle.time = run.time;
        if (run.kindleLevel > bestKindle.kindleLevel) bestKindle.kindleLevel = run.kindleLevel;
    }

}

[System.Serializable]
public class PlayerData
{
    public int playerSoulFlameTotal;
    public PlayerData()
    {
        playerSoulFlameTotal = 0;
    }

    public void NewTotal(int total)
    {
        playerSoulFlameTotal = total;
    }
}
