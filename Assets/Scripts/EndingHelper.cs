using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingHelper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("endGame",10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endGame()
    {
        SceneManager.LoadScene(0);
    }
}
