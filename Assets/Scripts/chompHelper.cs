using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chompHelper : MonoBehaviour
{
    public GameObject blackOut;
    public bool gameStarted;

    public AudioSource bonechompSound;
    public AudioSource nextLevelChime;

    public bool gameOver;

    public int nextLevelId = 3;

    public gameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                SceneManager.LoadScene(3);
            }
        }
    }

    public void chompEnd()
    {
        gameOver = true;
        manager.pauseGame();
        blackOut.SetActive(true);
    }

    public void chompSound()
    {
        
        bonechompSound.Play();
    }

    public void startGame()
    {
        manager.unpauseGame();
    }

    public void loadNextLevel()
    {
        manager.nextLevel();
    }

    public void playChime()
    {
        //if we are on the last hour, progress to the ending
        if (nextLevelId == manager.endingSceneID)
        {
            //destroy the game manager in case the player wants to play again
            int id = manager.endingSceneID;
            Destroy(manager.gameObject);
            gameManager.instance = null;

            //load the ending scene
            SceneManager.LoadScene(id);
        }

        manager.unpauseGame();
        nextLevelChime.Play();
    }
}
