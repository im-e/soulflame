using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreTableControl : MonoBehaviour
{
    public GameObject highscoreTable;

    public void Activate()
    {
        highscoreTable.SetActive(true);
    }
}
