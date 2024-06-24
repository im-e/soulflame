using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteTextControl : MonoBehaviour
{
    public TMPro.TextMeshProUGUI finishedTimer;        //text reference to the ingame timer
    public TMPro.TextMeshProUGUI nextKindleTime;
    public TMPro.TextMeshProUGUI bestKindleTime;
    public TMPro.TextMeshProUGUI totalSoulflameText;
    public TMPro.TextMeshProUGUI levelNameText;
    public TMPro.TextMeshProUGUI levelNumberText;

    public void UpdateFinishedTimer(string text)
    {
        finishedTimer.text = text;
    }

    public void UpdateNextKindleTime(string text)
    {
        nextKindleTime.text = text;
    }

    public void UpdateBestKindleTime(string text)
    {
        bestKindleTime.text = text;
    }

    public void UpdateTotalSoulflames(string text)
    {
        totalSoulflameText.text = text;
    }

    public void UpdateLevelName(string text)
    {
        levelNameText.text = text;
    }

    public void UpdateLevelNumber(string text)
    {
        levelNumberText.text = text;
    }

}
