using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtTime : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Kill",time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
