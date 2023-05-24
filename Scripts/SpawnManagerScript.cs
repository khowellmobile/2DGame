using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{


    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] powerUps;
    private GameManagerScript GM = null;
    private PlayerScript player = null;

    bool gameOver = false;
    Vector3[] spawnSide = { new Vector3(-6.5f, -2.97f, 0), new Vector3(6.5f, -2.97f, 0) };

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawn()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (!GM.gameOver)
        { 
            int randomPowerUp = Random.Range(0, 2);
            Instantiate(enemyPrefabs[randomPowerUp], spawnSide[randomPowerUp], Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }



    IEnumerator PowerUpSpawnRoutine()
    {
        while (!GM.gameOver)
        {
            player = GameObject.Find("Player").GetComponent<PlayerScript>();
            float xPos = Random.Range(-6.5f, 6.5f);
            if (xPos < player.transform.position.x + 1f && xPos > player.transform.position.x - 1f && player != null)
            {
                xPos += 1f;
            }
            int randomPowerUp = Random.Range(0, 5); // inclusive, exclusive
            Instantiate(powerUps[randomPowerUp], new Vector3(xPos, 6.5f, 0),
            Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
