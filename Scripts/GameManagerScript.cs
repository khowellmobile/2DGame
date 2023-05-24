using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public bool gameOver = true;
    private bool paused = false;
    [SerializeField] private GameObject player;

    UIScript UI = null;
    SpawnManagerScript SM = null;


    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("Canvas").GetComponent<UIScript>();
        SM = GameObject.Find("SpawnManager").GetComponent<SpawnManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!gameOver && Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                Time.timeScale = 1;
                paused = false;
                UI.HidePauseScreen();
            }
            else
            {
                // game is running
                Time.timeScale = 0; // % fps, pauses game
                paused = true;
                UI.ShowPauseScreen();
            }
        }

        if (gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            // Adds the player
            Instantiate(player, new Vector3(-9.2f, -2.5f, 0), Quaternion.identity);

            if (UI != null)
                UI.HideTitleScreen();

            gameOver = false;

            if (SM != null)
                SM.StartSpawn();
        }

    }
}
