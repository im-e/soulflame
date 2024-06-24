using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public int levelNumber;
    public string title;

    public float weakKindledTime;
    public float normalKindledTime;
    public float strongKindledTime;
    public float eternalKindledTime;
    public float developerKindledTime;
}
