using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class titleButtons : MonoBehaviour
{
    public PlayableDirector playGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playClicked()
    {
        playGame.Play();
    }

    public void CreditsClicked()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitClicked()
    {
        Application.Quit();
    }
}
