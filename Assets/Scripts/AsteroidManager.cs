using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidManager : MonoBehaviour
{
    public GameObject miniAsteroid;
    public GameObject miniMiniAsteroid;
    public GameManager gameManager;
    public AudioClip[] clips;
    public AudioSource audioSource;
    private void Start()
    {
        gameManager = GameManager.instance;
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        CheckBoundaries();
    }

    [HideInInspector] new public Rigidbody2D rigidbody;
    private void Reset()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        int points = 0;
        if (coll.gameObject.tag == "Projectile")
        {
            // alors se divise en 2 ou 3 petits asteroids

            if (tag == "Asteroid" || tag == "MiniAsteroid" || tag == "MiniMiniAsteroid")
            {
                int nbMini = Random.Range(1, 4);
                for (int i = 0; i < nbMini; i++)
                {
                    if (tag == "Asteroid")
                    {
                        CreateMiniAsteroid(miniAsteroid, transform.position);
                        points = 20;
                        audioSource.PlayOneShot(clips[0]);
                        Debug.Log("Boum");
                    }
                    else if (tag == "MiniAsteroid")
                    {
                        CreateMiniAsteroid(miniMiniAsteroid, transform.position);
                        points = 50;
                        audioSource.PlayOneShot(clips[0]);
                        Debug.Log("MiniBoum");
                    }
                    else
                    {
                        points = 100;
                        audioSource.PlayOneShot(clips[0]);
                        Debug.Log("MiniMiniBoum");
                    }
                }
            }
            Destroy(gameObject);
            Destroy(coll.gameObject);
        }
        gameManager.UpdateScore(points);
    }

    // création des éclats d'asteroid
    public void CreateMiniAsteroid(GameObject objectToSpawn, Vector3 position)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomSpeed = Random.Range(0.5f, 2.0f);
        Rigidbody2D rb = Instantiate(objectToSpawn, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        rb.velocity = randomDirection * randomSpeed;
    }

    // gestion de la sortie du cadre de jeu
    public void CheckBoundaries()
    {
        if (transform.position.x < -5 || transform.position.x > 5
            || transform.position.y < -3 || transform.position.y > 3)
        {
            Vector2 newPos = transform.position;
            if (transform.position.x < -5)
            {
                newPos.x = 5;
            }
            else if (transform.position.x > 5)
            {
                newPos.x = -5;
            }
            if (transform.position.y < -3)
            {
                newPos.y = 3;
            }
            else if (transform.position.y > 3)
            {
                newPos.y = -3;
            }

            transform.position = newPos;
        }
    }
}
