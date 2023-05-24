using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    private float speed = 2.5f;
    [SerializeField] private int powerUpId;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //script communcation - IMPORTANT!!
            PlayerScript P = collision.GetComponent<PlayerScript>();
            // look at what we collided with
            // look at components in the inspector
            // find the one called Player (the script)
            // link to it

            if (P != null)
            {
                if (powerUpId == 1)
                    P.RifleShotOn();
                else if (powerUpId == 2)
                    P.GreenShotOn();
                else if (powerUpId == 3)
                    P.SpeedBoostPowerUpOn();
                else if (powerUpId == 4)
                    P.SpeedDownPowerUpOn();
                else if (powerUpId == 5)
                    P.LifePowerUp();
            }


            Destroy(this.gameObject);
        }
    }
    
}
