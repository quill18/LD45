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

        //SpawnLevel();
        SpawnPlayer();
    }

    public GameObject[] Levels;
    int currLevel;

    CinemachineVirtualCamera[] virtualCameras;
    int currCamera = 0;

    public GameObject PlayerPrefab;    // The basic brain
    GameObject activePlayer;

    public int WaffleCount;

    // Update is called once per frame
    void Update()
    {
    }

    public void RestartLevel()
    {
        StartCoroutine(Co_RestartLevel());
    }

    IEnumerator Co_RestartLevel()
    {
        yield return new WaitForSeconds(2f);

        SpawnLevel();
        SpawnPlayer();
    }

    public void FinishLevel()
    {
        StartCoroutine(Co_FinishLevel());
    }

    IEnumerator Co_FinishLevel()
    {
        yield return new WaitForSeconds(2f);

        currLevel++;
        SpawnLevel();
        MovePlayerToStart();
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
        // Spawn level


    }
}
