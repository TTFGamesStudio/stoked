using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterController : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public float progress;
    public float speed;

    public fireController fire;
    public bool readyForDive;

    public Vector3 cameraPosition;
    public Vector3 cameraOffset;

    public Animator chompPlayer;
    public AudioSource roarSound;

    public AudioSource growl1;
    public bool growl1Played;
    public AudioSource growl2;
    public bool growl2Played;
    public AudioSource growl3;
    public bool growl3Played;

    [SerializeField] private gameManager manager;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<gameManager>();
        cameraPosition = Camera.main.transform.position + cameraOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.gamePaused)
        {
            if (progress < 0)
            {
                progress = 0;
            }

            if (progress > 1)
            {
                progress = -1;
            }

            updatePosition();

            if (progress > .25f && !growl1Played)
            {
                growl1Played = true;
                growl1.Play();
            }

            if (progress > .5f && !growl2Played)
            {
                growl2Played = true;
                growl2.Play();
            }

            if (progress > .75f && !growl3Played)
            {
                growl3Played = true;
                growl3.Play();

            }
        }
    }

    void updatePosition()
    {
        if (!readyForDive)
        {
            updateProgress();
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, progress);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, cameraPosition, Time.deltaTime * 3);
        }
    }

    void updateProgress()
    {
        progress += Time.deltaTime * (speed * (1-fire.fireStr()));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("monsterTrigger"))
        {
            roarSound.Play();
            gameObject.GetComponent<Animator>().SetTrigger("dive");
            readyForDive = true;
            chompPlayer.SetTrigger("chomp");
        }
        
    }
}

