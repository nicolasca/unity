using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Caracteristics")]
    [SerializeField] float health = 100f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] int scorePoints;

    [Header("Shooting")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 2f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 7f;

    [Header("Sound Effects")]
    [SerializeField] AudioClip audioClipDeath;
    [SerializeField] [Range(0, 1f)] float volumeDeath = 0.1f;

    GameState gameState;


    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShot();
    }

    private void CountDownAndShot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(
            laserPrefab,
            transform.position,
            laserPrefab.transform.rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        // Add points to score
        gameState.AddScore(scorePoints);

        // Play the sound of death
        AudioSource.PlayClipAtPoint(audioClipDeath, Camera.main.transform.position, volumeDeath);

        Destroy(gameObject);
        GameObject explosion = Instantiate(
            deathVFX,
            transform.position,
            Quaternion.identity);
        Destroy(explosion, 1f);
    }
}
