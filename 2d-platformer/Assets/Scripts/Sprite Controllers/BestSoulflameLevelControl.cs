using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestSoulflameLevelControl : MonoBehaviour
{

    public GameObject WeakKindle;
    public GameObject NormalKindle;
    public GameObject StrongKindle;
    public GameObject EternalKindle;


    public void KindleLevel(int kindleLevel)
    {
        switch (kindleLevel)
        {
            case 5:
                WeakKindle.SetActive(true);
                NormalKindle.SetActive(true);
                StrongKindle.SetActive(true);
                EternalKindle.SetActive(true);
                break;
            case 4:
                WeakKindle.SetActive(true);
                NormalKindle.SetActive(true);
                StrongKindle.SetActive(true);
                EternalKindle.SetActive(true);
                break;
            case 3:
                WeakKindle.SetActive(true);
                NormalKindle.SetActive(true);
                StrongKindle.SetActive(true);
                EternalKindle.SetActive(false);
                break;
            case 2:
                WeakKindle.SetActive(true);
                NormalKindle.SetActive(true);
                StrongKindle.SetActive(false);
                EternalKindle.SetActive(false);
                break;
            case 1:
                WeakKindle.SetActive(true);
                NormalKindle.SetActive(false);
                StrongKindle.SetActive(false);
                EternalKindle.SetActive(false);
                break;
            case 0:
                WeakKindle.SetActive(false);
                NormalKindle.SetActive(false);
                StrongKindle.SetActive(false);
                EternalKindle.SetActive(false);
                break;


            default: break;
        }
    }


}
