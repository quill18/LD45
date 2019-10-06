using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        virtualCameras = GameObject.FindObjectsOfType<CinemachineVirtualCamera>();
        TurnOffCameras();

        SpawnLevel();
        SpawnPlayer();
    }

    public GameObject[] Levels;
    int currLevel;

    CinemachineVirtualCamera[] virtualCameras;
    int currCamera = 0;

    public GameObject PlayerPrefab;    // The basic brain
    GameObject activePlayer;

    public int levelWaffles;
    public int levelWafflesGot;

    public int globalWaffles;
    public int globalWafflesGot;

    public bool isLoading = false;

    // Update is called once per frame
    void Update()
    {
        if(activePlayer == null && isLoading == false)
        {
            // Might have switched bodies?
            PlayerPlatformerController ppc = GameObject.FindObjectOfType<PlayerPlatformerController>();
            if(ppc != null)
            {
                activePlayer = ppc.gameObject;
            }
            else
            {
                // Player must be dead
                RestartLevel();
            }
        }
    }

    public void RestartLevel()
    {
        StartCoroutine(Co_RestartLevel());
    }

    IEnumerator Co_RestartLevel()
    {
        isLoading = true;
        yield return new WaitForSeconds(2f);

        SpawnLevel();
        SpawnPlayer();
        isLoading = false;
    }

    public void FinishLevel()
    {
        StartCoroutine(Co_FinishLevel());
    }

    IEnumerator Co_FinishLevel()
    {
        isLoading = true;
        yield return new WaitForSeconds(2f);

        currLevel++;
        SpawnLevel();
        MovePlayerToStart();
        isLoading = false;
    }

    void SpawnPlayer()
    {
        if(activePlayer != null)
        {
            Debug.LogError("Spawning a second player?!?!?");
            return;
        }


        UpdatePlayerTo(PlayerPrefab);

        MovePlayerToStart();
    }

    public void UpdatePlayerTo(GameObject newPlayerPrefab)
    {
        if(newPlayerPrefab== null)
        {
            Debug.Log("wah?");
        }
        GameObject playerGO = Instantiate(newPlayerPrefab);

        if (activePlayer != null)
        {
            playerGO.transform.position = activePlayer.transform.position;
            Destroy(activePlayer.gameObject);
        }

        activePlayer = playerGO;

        PlayerPrefab = newPlayerPrefab;

        TurnOffCameras();
        currCamera = (currCamera + 1) % virtualCameras.Length;
        virtualCameras[currCamera].Follow = playerGO.transform;
        virtualCameras[currCamera].gameObject.SetActive(true);
    }

    void TurnOffCameras()
    {
        foreach (CinemachineVirtualCamera cvc in virtualCameras)
        {
            cvc.gameObject.SetActive(false);
        }
    }

    Transform checkpointLocation;

    public void SetCheckpoint(Transform t)
    {
        checkpointLocation = t;
    }

    void MovePlayerToStart()
    {
        Vector3 pos;

        if (checkpointLocation != null)
        {
            pos = checkpointLocation.position;
        }
        else
        {
            PlayerStart ps = GameObject.FindObjectOfType<PlayerStart>();
            pos = ps.transform.position;
        }

        activePlayer.transform.position = pos;
    }

    void SpawnLevel()
    {
        for (int i = 0; i < Levels.Length; i++)
        {
            Levels[i].SetActive(i == currLevel);
        }

        levelWaffles = GameObject.FindObjectsOfType<PickupWaffle>().Length;
        levelWafflesGot = 0;

        globalWaffles += levelWaffles;
    }

    public void GotWaffle()
    {
        levelWafflesGot++;
        globalWafflesGot++;
    }
}
