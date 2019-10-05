using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();

        //SpawnLevel();
        SpawnPlayer();
    }

    public GameObject[] Levels;
    int currLevel;

    CinemachineVirtualCamera virtualCamera;

    public GameObject PlayerPrefab;    // The basic brain
    PlayerController activePlayer;

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPlayer()
    {
        if(activePlayer != null)
        {
            Debug.LogError("Spawning a second player?!?!?");
            return;
        }

        PlayerStart ps = GameObject.FindObjectOfType<PlayerStart>();

        GameObject playerGO = Instantiate(PlayerPrefab, ps.transform.position, Quaternion.identity);

        activePlayer = playerGO.GetComponent<PlayerController>();

        virtualCamera.Follow = playerGO.transform;
    }

    void SpawnLevel()
    {

    }
}
