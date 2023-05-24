using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private float speed = 1.5f;
    private float prevTime = 0.0f;
    private bool isGoingLeft = true;
    private UIScript UI = null;
    private GameManagerScript GM = null;
    private PlayerScript player = null;

    [SerializeField] private GameObject enemyDeathPrefab = null;
    [SerializeField] private GameObject enemyDamagePrefab = null;
    [SerializeField] private int enemyId;
    [SerializeField] private int lives;


    // Start is called before the first frame update
    void Start()
    {

        GM = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        UI = GameObject.Find("Canvas").GetComponent<UIScript>();
        if (enemyId == 0)
        {
            speed = 1.5f;
            lives = 0;
        } 
        else if (enemyId == 1)
        {
            speed = 2.0f;
            lives = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GM.gameOver)
        {
            Destroy(this.gameObject);
        }

        if (isGoingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        } 
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (Time.time > prevTime + 1.0f)
        {
            if (Random.Range(0, 10) > 5)
            {
                isGoingLeft = !isGoingLeft;
            }
            prevTime = Time.time;
        }


        if (transform.position.x >= 8.2f)
        {
            isGoingLeft = true;
        } 
        else if (transform.position.x <= -8.2f) 
        {
            isGoingLeft = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser"))
        {
            if (collision.transform.parent != null)
                Destroy(collision.transform.parent.gameObject);
            else
                Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            //lives?
            PlayerScript P = collision.GetComponent<PlayerScript>();

            if (P != null) P.Damage();
        }

        if (enemyId == 1 && lives >= 1)
        {
            Instantiate(enemyDamagePrefab, transform.position, Quaternion.identity);
            lives--;
        } 
        else
        {
            if (enemyId == 0)
            {
                UI.UpdateScore(10);
            } 
            else
            {
                UI.UpdateScore(15);
            }
            Instantiate(enemyDeathPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
