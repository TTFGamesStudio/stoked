using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "hour_",menuName = "data/hour",order = 1)]
public class hourData : ScriptableObject
{
   public float monsterSpeed=0.8f;     //how fast the monster walks
   public int logs=7; //how many logs you have this level
   public float logBurnSpeed=1f; //a multiplier for how fast the logs burn
   public float timeScale = 1f; //a multiplier for how fast seconds tick by
   public float diffucultyAdjustment = 1f; //an overall global difficulty manager
    public bool isAM = false;
    public string hourString = "10";
}
