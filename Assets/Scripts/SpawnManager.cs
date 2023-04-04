using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int _nbSpawn;
    public GameObject _asteroid;

    // Start is called before the first frame update
    void Start()
    {
        // spawn asteroids
        for (int i = 0; i < _nbSpawn; i++)
        {
            Spawn(_asteroid);
        }
    }

    public void Spawn(GameObject objectToSpawn)
    {
        Vector3 spawnPosition = new Vector3();

        while (true)
        {
            // Génère une position de spawn aléatoire
            float x = Random.Range(-5, 5);
            float y = Random.Range(-3, 3);
            spawnPosition = new Vector3(x, y, 0);

            // Vérifie si la position de spawn est en dehors de la zone exclue
            if (Vector3.Distance(spawnPosition, Vector3.zero) > 2)
            {
                break;
            }

        }

        // calcul une direction et une vitesse aléatoires
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomSpeed = Random.Range(1.0f, 2.0f);
        Rigidbody2D rb = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity).GetComponent<Rigidbody2D>();
        rb.velocity = randomDirection * randomSpeed;
    }
}
