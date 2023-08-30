using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class levelData : MonoBehaviour
{
    public int hourNumber = 1;
    public int hourTime = 10;
    public float monterSpeed = .15f;
    public int startLogs = 7;
    public int levelId = 1;
    public bool isAm = false;
    public float difficulty = 0.85f;
    public clockManager clock;
    public TextMeshProUGUI IntroDisplay;
    public monsterController monster;
    public logManager logs;
    public chompHelper chomp;
    private void Start()
    {
        chomp = GameObject.FindObjectOfType<chompHelper>();
        chomp.nextLevelId = levelId + 3;
    }
}
