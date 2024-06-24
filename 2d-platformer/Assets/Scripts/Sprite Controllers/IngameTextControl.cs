using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameTextControl : MonoBehaviour
{
    public TMPro.TextMeshProUGUI ingameTimer;        //text reference to the ingame timer
    public GameObject ingameLevelNameObject;
    public TMPro.TextMeshProUGUI ingameLevelName;        //text reference to the ingame timer

    public void UpdateTimer(string text)
    {
        ingameTimer.text = text;
    }

    public void UpdateLevelName(string text)
    {
        ingameLevelName.text = text;
    }

    public void DisableLevelName()
    {
        ingameLevelNameObject.SetActive(false);
    }
}
