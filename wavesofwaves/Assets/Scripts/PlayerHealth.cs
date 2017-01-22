﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int enemiesInRange;
    public int maxEnemiesInRange;
    public int health;
    public int maxHealth;
    public GameObject explosionParticle;


	// Use this for initialization
	void Start () {
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void CheckDeath()
    {
        if(health <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Camera.main.GetComponent<CameraShaking>().Shake(0.3f, 0.3f);
        CheckDeath();
    }

    public void Death()
    {
        // Switch to ragdoll form
        GetComponent<BoxCollider>().enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name == "Scaler")
            {
                // De-activate scalar
                transform.GetChild(i).gameObject.SetActive(false);
            }
            if (transform.GetChild(i).gameObject.name == "Ragdoll")
            {
                // Activate ragdool
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        //Instantiate(explosionParticle, transform.position, Quaternion.identity);
        GameManager.Instance.GameOver();

        //TODO: DEATH STUFF
       // Destroy(gameObject);
    }
}
