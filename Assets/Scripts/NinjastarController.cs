using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjastarController : MonoBehaviour
{

    public float speed;

    public PlayerController player;

    public GameObject enemyDeathEffect;

    public GameObject impactEffect;

    public int pointsForKill;

    public float rotationSpeed;

    public int damageToGive;

    private float pos;

    public int range;
    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if (player.transform.localScale.x < 0)
        {
            speed = -speed;
            rotationSpeed = -rotationSpeed;
        }
        pos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().angularVelocity = rotationSpeed;

        float temp = pos - transform.position.x;
        Debug.Log(temp);
        if (temp < -range || temp > range)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            //Instantiate(enemyDeathEffect, other.transform.position, other.transform.rotation);
            //Destroy(other.gameObject);
            //ScoreManager.AddPoints(pointsForKill);

            other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
