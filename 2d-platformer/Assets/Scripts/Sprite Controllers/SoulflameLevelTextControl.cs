using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulflameLevelTextControl : MonoBehaviour
{

    public GameObject WeakKindle;
    public GameObject NormalKindle;
    public GameObject StrongKindle;
    public GameObject EternalKindle;
    public GameObject NotKindled;
    public GameObject NewBestTimeText;

    public void NewBest()
    {
        NewBestTimeText.SetActive(true);
    }
    public void KindleLevel(int kindleLevel)
    {

        switch(kindleLevel)
        {
            case 5:
                NotKindled.SetActive(false);
                WeakKindle.SetActive(false);
                NormalKindle.SetActive(false);
                StrongKindle.SetActive(false);
                EternalKindle.SetActive(true);
                break;
            case 4:
                NotKindled.SetActive(false);
                WeakKindle.SetActive(false);
                NormalKindle.SetActive(false);
                StrongKindle.SetActive(false);
                EternalKindle.SetActive(true);
                break;
            case 3:
                NotKindled.SetActive(false);
                WeakKindle.SetActive(false);
                NormalKindle.SetActive(false);
                StrongKindle.SetActive(true);
                EternalKindle.SetActive(false);
                break;
            case 2:
                NotKindled.SetActive(false);
                WeakKindle.SetActive(false);
                NormalKindle.SetActive(true);
                StrongKindle.SetActive(false);
                EternalKindle.SetActive(false);
                break;
            case 1:
                NotKindled.SetActive(false);
                WeakKindle.SetActive(true);
                NormalKindle.SetActive(false);
                StrongKindle.SetActive(false);
                EternalKindle.SetActive(false);
                break;
            case 0:
                NotKindled.SetActive(true);
                WeakKindle.SetActive(false);
                NormalKindle.SetActive(false);
                StrongKindle.SetActive(false);
                EternalKindle.SetActive(false);
                break;
            default:
                NotKindled.SetActive(false);
                WeakKindle.SetActive(false);
                NormalKindle.SetActive(false);
                StrongKindle.SetActive(false);
                EternalKindle.SetActive(false);
                break;
        }
       
    }


}
