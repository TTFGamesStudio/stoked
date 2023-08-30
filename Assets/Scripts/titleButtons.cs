using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleButtons : MonoBehaviour
{
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
        SceneManager.LoadScene(3);
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
