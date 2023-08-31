using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterWalkSounds : MonoBehaviour
{
    public List<AudioClip> footstepSounds;
    [SerializeField] private AudioSource footstepSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound()
    {
        footstepSource.clip = footstepSounds[Random.Range(0, footstepSounds.Count - 1)];
        footstepSource.Play();
    }
}
