using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class HighscoreTable : MonoBehaviour
{
    private Level level;
    private Transform entryContainer;
    private Transform entryTemplate;

    private List<KindleRun> kindleHistory;

    private void Awake()
    {
        SaveDataManager.current.LoadData();
        string filepath = SceneManager.GetActiveScene().name;
        level = Resources.Load<Level>("Levels/" + filepath);

        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        kindleHistory = SaveDataManager.current.saveData.levelData[level.levelNumber].kindleHistory;
        kindleHistory.Sort((a, b) => a.time.CompareTo(b.time));
        float templateHeight = 25f;
        int x = 0;
        for (int i = 1; i < kindleHistory.Count; i++) 
        {
            if(x < kindleHistory.Count)
            {
                Transform entryTransform = Instantiate(entryTemplate, entryContainer);
                RectTransform rectTransform = entryTransform.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(0, -templateHeight * x);
                entryTransform.gameObject.SetActive(true);

                int rank = x + 1;
                string rankString;
                switch (rank)
                {
                    default:
                        rankString = rank + "TH";
                        break;
                    case 1: rankString = "1ST"; break;
                    case 2: rankString = "2ND"; break;
                    case 3: rankString = "3RD"; break;
                }

                entryTransform.Find("Rank").GetComponent<TextMeshProUGUI>().text = rankString;

                float time = kindleHistory[i].time;
                entryTransform.Find("Time").GetComponent<TextMeshProUGUI>().text = time.ToString("F2");

                int kindleLevel = kindleHistory[i].kindleLevel;
                string kindle;
                switch(kindleLevel)
                {
                    case 5:
                        kindle = "MAX";
                        break;
                    case 4:
                        kindle = "4";
                        break;
                    case 3:
                        kindle = "3";
                        break;
                    case 2:
                        kindle = "2";
                        break;
                    case 1:
                        kindle = "1";
                        break;
                    case 0:
                        kindle = "0";
                        break;
                    default:
                        kindle = "0";
                        break;
                }
                entryTransform.Find("Kindle").GetComponent<TextMeshProUGUI>().text = kindle;
                x++;
            }

        }

    }

}
