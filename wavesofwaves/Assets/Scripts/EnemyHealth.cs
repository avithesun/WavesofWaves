﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public int health;
    public int maxHealth;

    // Use this for initialization
    void Start () {
        GameManager.Instance.enemies.Add(this);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        GameManager.Instance.AddScore(100);
        GameManager.Instance.enemies.Remove(this);
        GameManager.Instance.CheckEnemies();
        //Camera.main.GetComponent<CameraShaking>().Shake(0.3f, 0.3f);
        Destroy(gameObject);
    }
}
