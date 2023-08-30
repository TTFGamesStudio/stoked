using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class splashScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("skip",7);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void skip()
    {
        SceneManager.LoadScene(1);
    }
}
