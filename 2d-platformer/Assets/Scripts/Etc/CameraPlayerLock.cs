using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerLock : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.Find("Player");

        cam.Follow = player.transform;
    }

}
