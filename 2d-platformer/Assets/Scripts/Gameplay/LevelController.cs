using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelController : MonoBehaviour
{
    public bool levelCompleted; //control bool for when a level has been completed
    public bool levelStarted; //control bool for when a level has been completed

    private Level level;
    private GameObject ingameUI;       //reference to the player's HUD
    private GameObject completeUI;
    private GameObject levelSoulflame;
    private IngameTextControl ingameTextController;        //text reference to the ingame timer
    private LevelCompleteTextControl levelCompleteTextController;
    private SoulflameLevelControl levelSoulflameController;
    private SoulflameLevelControl CompleteSoulflameController;
    private SoulflameLevelTextControl CompleteSoulflameTextController;
    private BestSoulflameLevelControl BestKindleController;
    private NextKindleLevelControl NextKindleController;
    private HighscoreTableControl highscoreTableControl;

    private float startedTime;        //float which holds the time when a level loads
    private float currentTime;          //holds the unformatted raw timer value 
    private float finishedTime;         //finished time
    private int finishedKindleLevel;
    private float bestTime;
    private int bestKindleLevel;
    private int soulFlameTotal;


    // Start is called before the first frame update
    void Start()
    {
        if(OverworldController.gameFirstStart)
        {
            SaveDataManager.current.LoadData();
            OverworldController.gameFirstStart = false;
        }
        string filepath = SceneManager.GetActiveScene().name;
        level = Resources.Load<Level>("Levels/" + filepath);

        ingameUI = GameObject.Find("Ingame UI");
        completeUI = GameObject.Find("Level Complete UI");
        completeUI.SetActive(false);
        levelSoulflame = GameObject.Find("Level Soulflame");

        ingameTextController = ingameUI.GetComponent<IngameTextControl>();
        levelCompleteTextController = completeUI.GetComponent<LevelCompleteTextControl>();

        levelSoulflameController = levelSoulflame.GetComponent<SoulflameLevelControl>();
        CompleteSoulflameController = completeUI.GetComponent<SoulflameLevelControl>();
        CompleteSoulflameTextController = completeUI.GetComponent<SoulflameLevelTextControl>();
        BestKindleController = completeUI.GetComponent<BestSoulflameLevelControl>();
        NextKindleController = completeUI.GetComponent<NextKindleLevelControl>();
        highscoreTableControl = completeUI.GetComponent<HighscoreTableControl>();

        ingameTextController.ingameLevelName.text = level.title;

        levelStarted = false;
        levelCompleted = false;

        bestTime = SaveDataManager.current.saveData.levelData[level.levelNumber].bestKindle.time;
        bestKindleLevel = SaveDataManager.current.saveData.levelData[level.levelNumber].bestKindle.kindleLevel;
        soulFlameTotal = SaveDataManager.current.saveData.playerData.playerSoulFlameTotal;

        levelSoulflameController.KindleLevel(bestKindleLevel);
    } 


    // Update is called once per frame
    void Update()
    {
        if (!levelStarted)
        {
            if (Input.GetButtonDown("Cancel")) GoToOverworld();
        }
        else if(!levelCompleted && levelStarted) //if the level hasnt been completed
        {

            if (Input.GetButtonDown("Restart")) Restart();
            if (Input.GetButtonDown("Cancel")) GoToOverworld();
            currentTime = Time.time - startedTime;    //get the current level time by taking the current global time, and taking it away from the global time we referenced in Start()
            ingameTextController.UpdateTimer(currentTime.ToString("F2")); //make/update the timer with our formatted variable
        }
        else
        {
            if (Input.GetButtonDown("Jump")) NextLevel();
            if (Input.GetButtonDown("Restart")) Restart();
            if (Input.GetButtonDown("Cancel")) GoToOverworld();
        }
        
    }

    public void LevelStarted()
    {
        startedTime = Time.time;
        ingameTextController.DisableLevelName();
        levelStarted = true;
    }

    public void LevelComplete()
    {
        levelCompleted = true;   //set the control bool to true

        //currently finished time
        finishedTime = currentTime;
        levelCompleteTextController.UpdateLevelName(level.title);
        levelCompleteTextController.UpdateFinishedTimer(finishedTime.ToString(""));
        levelCompleteTextController.UpdateFinishedTimer(finishedTime.ToString("F2")); //make/update the timer with our formatted variable

        //currently finished kindle level
        if (finishedTime <= level.developerKindledTime) finishedKindleLevel = 5;
        else if (finishedTime <= level.eternalKindledTime) finishedKindleLevel = 4;
        else if (finishedTime <= level.strongKindledTime) finishedKindleLevel = 3;
        else if (finishedTime <= level.normalKindledTime) finishedKindleLevel = 2;
        else if (finishedTime <= level.weakKindledTime) finishedKindleLevel = 1;
        else finishedKindleLevel = 0;
        CompleteSoulflameController.KindleLevel(finishedKindleLevel);
        CompleteSoulflameTextController.KindleLevel(finishedKindleLevel);

        KindleRun finishedRun = new KindleRun(finishedTime, finishedKindleLevel);
        SaveDataManager.current.saveData.levelData[level.levelNumber].AddRun(finishedRun);

        //best kindle
        if (finishedTime < bestTime)
        {
            bestTime = finishedTime;
            CompleteSoulflameTextController.NewBest();
        }
        if (finishedKindleLevel > bestKindleLevel)
        {
            soulFlameTotal += finishedKindleLevel - bestKindleLevel;
            bestKindleLevel = finishedKindleLevel;
            //kindle upgrade animation / sound
        }
        BestKindleController.KindleLevel(bestKindleLevel);
        levelCompleteTextController.UpdateBestKindleTime(bestTime.ToString("F2"));


        //next kindle goal
        NextKindleController.KindleLevel(bestKindleLevel);
        levelCompleteTextController.UpdateNextKindleTime(NextKindleTime(bestKindleLevel));

        //total kindles
        levelCompleteTextController.UpdateTotalSoulflames(soulFlameTotal.ToString("0"));
        SaveDataManager.current.saveData.playerData.playerSoulFlameTotal = soulFlameTotal;
        SaveDataManager.current.SaveData();

        levelCompleteTextController.UpdateLevelNumber((level.levelNumber + 1).ToString()
            + " / " + (SceneManager.sceneCountInBuildSettings - 1).ToString());

        highscoreTableControl.Activate();

        ingameUI.SetActive(false);
        completeUI.SetActive(true);

    }


    public string NextKindleTime(int nextKindle)
    {
        switch(nextKindle)
        {
            case 0:
                return level.weakKindledTime.ToString("F2");
            case 1:
                return level.normalKindledTime.ToString("F2");
            case 2:
                return level.strongKindledTime.ToString("F2");
            case 3:
                return level.eternalKindledTime.ToString("F2");
            case 4:
                return level.developerKindledTime.ToString("F2");
            case 5:
                return level.developerKindledTime.ToString("F2");
            default: return "0.00";
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   //reload the current scene
    }

    public void NextLevel()
    {
        int totalLevels = SceneManager.sceneCountInBuildSettings - 1;
        int currentLevel = level.levelNumber + 1;
        if (currentLevel == totalLevels) SceneManager.LoadScene("Overworld");
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //loads the next scene in the build index queue
    }

    public void PreviousLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //loads the next scene in the build index queue
    }

    public void GoToOverworld()
    {
        SceneManager.LoadScene("Overworld"); //Load the mainmenu scene
    }

}
