using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject bulletPrefab;
    public AudioSource audioSource;
    public AudioClip[] clips;
    private Rigidbody2D _rb;
    private float _speed = 200;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // gestion des déplacements flèches directionnelles
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _rb.AddForce(_rb.transform.up * _speed * Time.deltaTime);
            Debug.Log("on avance");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _rb.AddForce(-_rb.transform.up * _speed * Time.deltaTime);
            Debug.Log("on recule");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
            Debug.Log("on tourne à gauche");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.back * _speed * Time.deltaTime);
            Debug.Log("on tourne à droite");
        }

        // gestion des limites
        CheckBoundaries();

        // tir clic gauche souris ou barre d'espace
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // si collision avec un asteroid
        // désactive la vélocité pour l'immobiliser après réapparition
        // désactive l'objet et active le trigger pour ne pas entrer en collision dès la réapparition
        if (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "MiniAsteroid" 
            || collision.gameObject.tag == "MiniMiniAsteroid")
        {
            _rb.velocity = Vector3.zero;
            audioSource.PlayOneShot(clips[0]);
            gameObject.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameManager.PlayerExploded();
            gameManager.UpdateLives();
        }
    }

    private void Fire()
    {
        // tir le projectile
        var bullet = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        bullet.GameObject().GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * 200);
        Destroy(bullet, 2.0f);
        audioSource.PlayOneShot(clips[0]);
    }

    //gestion des limites
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
