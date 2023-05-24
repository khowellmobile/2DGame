using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    // Instance Variables
    [SerializeField] private float speed = 10.0f;
    private bool faceRight;
    private PlayerScript player = null;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        faceRight = (player.transform.localScale.x < 0 ? false : true);

        if (!faceRight)
        {
            Vector3 newDirection = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            transform.localScale = newDirection;
            transform.position = new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!faceRight)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }


        // Deletes object when beyond a certain bounds
        if (transform.position.x > 9 || transform.position.x < -9)
        {
            Destroy(this.gameObject);
        }
    }
}
