using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firewoodStack : MonoBehaviour
{
    public logManager logMan;

    [SerializeField] private int neededAmmount;
    private MeshRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        logMan=GameObject.FindObjectOfType<logManager>();
        _renderer = GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (logMan.logs >= neededAmmount)
        {
            _renderer.enabled = true;
        }
        else
        {
            _renderer.enabled = false;
        }
    }
}
