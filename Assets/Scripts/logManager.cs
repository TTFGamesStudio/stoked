using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logManager : MonoBehaviour
{
    public int logs = 7;
    public fireController f;
    public AudioSource logOnFireSound;
    public chompHelper chomp;

    public GameObject fireBurstPrefab;

    [SerializeField] private gameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<gameManager>();
        f = GameObject.FindObjectOfType<fireController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.gamePaused)
        {
            if (Input.GetKeyUp(KeyCode.Q) && !chomp.gameOver)
            {
                if (logs > 0 && f.addLog())
                {
                    logs--;
                    logOnFireSound.Play();
                    GameObject.FindObjectOfType<monsterController>().progress -= 0.2f;
                    Instantiate(fireBurstPrefab, f.transform.position, f.transform.rotation);
                }
            }
        }
    }
}
