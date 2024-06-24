using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextKindleLevelControl : MonoBehaviour
{
    public GameObject SoulflameTorch;
    public GameObject WeakKindle;
    public GameObject NormalKindle;
    public GameObject StrongKindle;
    public GameObject EternalKindle;
    public GameObject MaxKindleText;
    public GameObject DevTimeText;

    public void KindleLevel(int kindleLevel)
    {
        switch (kindleLevel)
        {
            case 5:
                SoulflameTorch.SetActive(false);
                WeakKindle.SetActive(false);
                NormalKindle.SetActive(false);
                StrongKindle.SetActive(false);
                EternalKindle.SetActive(false);
                MaxKindleText.SetActive(true);
                DevTimeText.SetActive(true);
                break;
            case 4:
                SoulflameTorch.SetActive(false);
                WeakKindle.SetActive(false);
                NormalKindle.SetActive(false);
                StrongKindle.SetActive(false);
                EternalKindle.SetActive(false);
                MaxKindleText.SetActive(true);
                DevTimeText.SetActive(false);
                break;
            case 3:
                SoulflameTorch.SetActive(true);
                WeakKindle.SetActive(true);
                NormalKindle.SetActive(true);
                StrongKindle.SetActive(true);
                EternalKindle.SetActive(true);
                MaxKindleText.SetActive(false);
                DevTimeText.SetActive(false);
                break;
            case 2:
                SoulflameTorch.SetActive(true);
                WeakKindle.SetActive(true);
                NormalKindle.SetActive(true);
                StrongKindle.SetActive(true);
                EternalKindle.SetActive(false);
                MaxKindleText.SetActive(false);
                DevTimeText.SetActive(false);
                break;
            case 1:
                SoulflameTorch.SetActive(true);
                WeakKindle.SetActive(true);
                NormalKindle.SetActive(true);
                StrongKindle.SetActive(false);
                EternalKindle.SetActive(false);
                MaxKindleText.SetActive(false);
                DevTimeText.SetActive(false);
                break;
            case 0:
                SoulflameTorch.SetActive(true);
                WeakKindle.SetActive(true);
                NormalKindle.SetActive(false);
                StrongKindle.SetActive(false);
                EternalKindle.SetActive(false);
                MaxKindleText.SetActive(false);
                DevTimeText.SetActive(false);
                break;
            default:
                SoulflameTorch.SetActive(false);
                WeakKindle.SetActive(false);
                NormalKindle.SetActive(false);
                StrongKindle.SetActive(false);
                EternalKindle.SetActive(false);
                MaxKindleText.SetActive(false);
                DevTimeText.SetActive(false);
                break;
        }
    }


}
