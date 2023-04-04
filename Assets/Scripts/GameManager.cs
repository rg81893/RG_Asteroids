using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Canvas gameOverCanvas;
    public RawImage[] ship;
    public PlayerManager player;
    public int lives = 3;
    public int score = 0;
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlayerExploded()
    {
        lives--;
        if (lives == 0)
        {
            GameOver();
        }
        else
        {
            Invoke("Respawn", 3.0f);
        }
    }

    public void UpdateLives()
    {
        ship[lives].gameObject.SetActive(false);
    }
    public void UpdateScore(int points)
    {
        score += points;
        scoreText.SetText(score.ToString());
    }

    private void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.transform.rotation = Quaternion.identity;
        player.gameObject.SetActive(true);
        StartCoroutine(ActivateColliderAfterDelay(3f));
    }

    // pour réactiver les collisions
    IEnumerator ActivateColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
     
    }

    private void GameOver()
    {
        gameOverCanvas.gameObject.SetActive(true);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ApplicationQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
