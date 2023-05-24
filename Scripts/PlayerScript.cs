using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // instance variables
    [SerializeField] private float speed = 6.0f;
    [SerializeField] private bool canRifleShot = false;
    [SerializeField] private bool canGreenLaser = false;
    [SerializeField] private GameObject LaserPrefab = null;
    [SerializeField] private GameObject arrowPrefab = null;
    [SerializeField] private GameObject deathPrefab = null;
    [SerializeField] private GameObject PurpleLaserPrefab = null;
    [SerializeField] private GameObject GreenLaserPrefab = null;
    [SerializeField] private GameObject[] dirtObjects = null;
    private AudioSource laserAudio = null;
    private UIScript UI = null;
    private Animator anim = null;
    private GameManagerScript GM = null;
    private SpawnManagerScript SM = null;
    [SerializeField] private int lives = 3;
    private float laserfireRate = .1f;
    private float arrowfireRate = .3f;
    private float canFire = 0.05f;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        UI = GameObject.Find("Canvas").GetComponent<UIScript>();
        GM = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        SM = GameObject.Find("SpawnManager").GetComponent<SpawnManagerScript>();
        laserAudio = GetComponent<AudioSource>();

        if (SM != null)
            SM.StartSpawn();

        // Prints to the Log
        Debug.Log("Hello");
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        Move();
        Boundary();
    }

    private void Move()
    {
        // Horizontal Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float xScale = transform.localScale.x;

        if (Input.GetKeyDown(KeyCode.A))
        {
            xScale = (xScale < 0 ? xScale : xScale * -1);
            Vector3 leftFacingVector = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
            transform.localScale = leftFacingVector;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            xScale = (xScale < 0 ? xScale * -1 : xScale);
            Vector3 rightFacingVector = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
            transform.localScale = rightFacingVector;
        }

        // move right converted to clock seconds
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.position = new Vector3(transform.position.x + 2.5f, transform.position.y, 0);
        } else if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.position = new Vector3(transform.position.x - 2.5f, transform.position.y, 0);
        }

    }

    private void Boundary()
    {
        // Top/Bottom Bounds
        if (transform.position.y >= -3.2)
        {
            transform.position = new Vector3(transform.position.x, -3.2f, 0);
        }
        else if (transform.position.y <= -3.7)
        {
            transform.position = new Vector3(transform.position.x, -3.7f, 0);
        }

        // Left/Right Bounds
        if (transform.position.x >= 9.46f)
        {
            transform.position = new Vector3(-9.46f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9.46f)
        {
            transform.position = new Vector3(9.46f, transform.position.y, 0);
        }
    }

    // Shoots laser from player
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (Time.time > canFire)
            {
                laserAudio.Play();
                if (canRifleShot)
                {
                    Instantiate(PurpleLaserPrefab, transform.position + new Vector3(0.85f, 0.6f, 0), Quaternion.identity);
                }
                else if (canGreenLaser)
                {
                    Instantiate(GreenLaserPrefab, transform.position + new Vector3(0.85f, 0.6f, 0), Quaternion.identity);
                } 
                else
                {
                    Instantiate(LaserPrefab, transform.position + new Vector3(0.55f, 0.6f, 0), Quaternion.identity);
                }
            }
            canFire = Time.time + laserfireRate;
        } 
        else if (Input.GetKeyDown(KeyCode.J))
        {
            if (Time.time > canFire)
            {
                laserAudio.Play();
                Instantiate(arrowPrefab, transform.position + new Vector3(0.55f, 0.6f, 0), Quaternion.identity);
                canFire = Time.time + arrowfireRate;
            }
        }
    }


    public IEnumerator RifleShotOff()
    {
        yield return new WaitForSeconds(5.0f);

        canRifleShot = false;
    }

    public void RifleShotOn()
    {
        canRifleShot = true;
        StartCoroutine(RifleShotOff());
    }

    public IEnumerator GreenShotOff()
    {
        yield return new WaitForSeconds(5.0f);

        canGreenLaser = false;
    }

    public void GreenShotOn()
    {
        canGreenLaser = true;
        StartCoroutine(GreenShotOff());
    }

    public IEnumerator SpeedBoostPowerUpOff()
    {
        yield return new WaitForSeconds(7.0f);
        speed /= 2;
    }

    public IEnumerator FlashRed()
    {

        for (int i = 0; i < 3; i++)
        {
            foreach (Transform child in gameObject.transform)
            {
                child.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            }

            yield return new WaitForSeconds(0.2f);

            foreach (Transform child in gameObject.transform)
            {
                child.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    public void SpeedBoostPowerUpOn()
    {
        speed *= 2;
        StartCoroutine(SpeedBoostPowerUpOff());
    }

    public IEnumerator SpeedDownPowerUpOff()
    {
        yield return new WaitForSeconds(7.0f);

        speed *= 2;
    }

    public void SpeedDownPowerUpOn()
    {
        speed /= 2;
        StartCoroutine(SpeedDownPowerUpOff());
    }

    public void LifePowerUp()
    {
        lives++;
        if (UI != null)
            UI.UpdateLives(lives);
    }

    public void Damage()
    {


        StartCoroutine(FlashRed());

        lives--;

        dirtObjects[lives].SetActive(true);

        if (UI != null)
            UI.UpdateLives(lives);

        if (lives <= 0)
        {
            Instantiate(deathPrefab, transform.position, Quaternion.identity);
            GM.gameOver = true;
            UI.ShowTitleScreen();
            Destroy(transform.gameObject);
        }
    }
}

