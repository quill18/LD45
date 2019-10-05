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

        GameObject playerGO = Instantiate(newPlayerPrefab);

        if (activePlayer != null)
        {
            playerGO.transform.position = activePlayer.transform.position;
            Destroy(activePlayer.gameObject);
        }

        activePlayer = playerGO;

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

    void MovePlayerToStart()
    {
        PlayerStart ps = GameObject.FindObjectOfType<PlayerStart>();
        activePlayer.transform.position = ps.transform.position;
    }

    void SpawnLevel()
    {
        // Spawn level


    }
}
