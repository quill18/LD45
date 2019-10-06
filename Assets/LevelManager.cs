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

    public GameObject LevelEndScreen;

    CinemachineVirtualCamera[] virtualCameras;
    int currCamera = 0;

    public GameObject PlayerPrefab;    // The basic brain
    GameObject activePlayer;

    public int levelWaffles;
    public int levelWafflesGot;

    public int globalWaffles;
    public int globalWafflesGot;

    public bool isLoading = false;

    public CanvasGroup EndScreen;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Input.GetKey(KeyCode.LeftShift))
        {
            FinishLevel();
        }
        if (Input.GetKeyDown(KeyCode.B) && Input.GetKey(KeyCode.LeftShift))
        {
            PickupBodyPart b = GameObject.FindObjectOfType<PickupBodyPart>();
            if(b != null)
            {
                UpdatePlayerTo(b.NewPlayerPrefab);
            }
        }

        if (activePlayer == null && isLoading == false)
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

        //SpawnLevel();
        SpawnPlayer();
        isLoading = false;
    }

    public void FinishLevel()
    {
        checkpointLocation = null;
        StartCoroutine(Co_FinishLevel());
    }

    IEnumerator Co_FinishLevel()
    {
        isLoading = true;
        LevelEndScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        LevelEndScreen.SetActive(false);

        currLevel++;

        if (currLevel >= Levels.Length)
        {
            EndGame();
        }
        else
        {
            SpawnLevel();
            MovePlayerToStart();
            isLoading = false;
        }
    }

    void EndGame()
    {
        Animator[] anims = EndScreen.GetComponentsInChildren<Animator>();

        foreach(Animator anim in anims)
        {
            anim.SetTrigger("FadeIn");
        }
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

    public bool isOutdoors()
    {
        return currLevel != 1;
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
