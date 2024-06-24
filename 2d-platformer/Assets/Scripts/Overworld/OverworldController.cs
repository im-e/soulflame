using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverworldController : MonoBehaviour
{
    static public bool gameFirstStart = true;
    public bool playerHasControl = false;

    private GameObject tutorialUI;
    private GameObject virtualCam;

    public ObjID signRightTrigger;
    public ObjID signLeftTrigger;



    // Start is called before the first frame update
    void Start()
    { 
        Cursor.visible = false;

        virtualCam = GameObject.Find("Virtual Camera");
        tutorialUI = GameObject.Find("Tutorial UI");

        if (gameFirstStart)
        {
            SaveDataManager.current.SetupData();
        }
        else
        {
            SaveDataManager.current.LoadData();
            playerHasControl = true;
        }

        if(SaveDataManager.current.newGame)
        {
            tutorialUI.SetActive(true);
        }
        else
        {
            tutorialUI.SetActive(false);
            playerHasControl = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(tutorialUI.activeInHierarchy)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                tutorialUI.SetActive(false);
                playerHasControl = true;
            }
        }    
    }

    public void ShowTutorial()
    {
        tutorialUI.SetActive(true);
        playerHasControl = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void goToWorld1(string sign)
    {
        virtualCam.transform.position = new Vector3(41.5f, virtualCam.transform.position.y, virtualCam.transform.position.z);
        Moved(sign);
    }

    public void goToWorld2(string sign)
    {
        virtualCam.transform.position = new Vector3(88f, virtualCam.transform.position.y, virtualCam.transform.position.z);
        Moved(sign);
    }

    public void goToWorld3(string sign)
    {
        virtualCam.transform.position = new Vector3(134.5f, virtualCam.transform.position.y, virtualCam.transform.position.z);
        Moved(sign);
    }

    public void MainMenu(string sign)
    {
        virtualCam.transform.position = new Vector3(-5, virtualCam.transform.position.y, virtualCam.transform.position.z);
        Moved(sign);
    }

    public void Moved(string sign)
    {
        if(sign == "SignTriggerRight")
        {
            signLeftTrigger.id += 1;
            signRightTrigger.id += 1;
        }
        else if (sign == "SignTriggerLeft")
        {
            signLeftTrigger.id -= 1;
            signRightTrigger.id -= 1;
        }
    }
}
