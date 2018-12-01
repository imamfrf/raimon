﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

    public int pointsToAdd;

    public GameObject coinParticles;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            ScoreManager.AddPoints(pointsToAdd);
           // Instantiate(coinParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
