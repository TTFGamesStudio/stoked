using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class clockManager : MonoBehaviour
{
    public string hour;

    public int minute;

    public float secondsPerMinute = 1;

    public float counter;

    public TextMeshProUGUI text;

    public Animator chompAnim;

    public levelData data;

    private gameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<gameManager>();
        chompAnim = GameObject.FindObjectOfType<chompHelper>().GetComponent<Animator>();
        data = GameObject.FindObjectOfType<levelData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.gamePaused)
        {
            updateTime();
            string minuteText = "" + minute;
            if (minute < 10)
            {
                minuteText = "0" + minuteText;
            }

            if (!data.isAm)
            {
                text.text = hour + ":" + minuteText + " p.m.";
            }
            else
            {
                text.text = hour + ":" + minuteText + " a.m.";
            }
        }
    }

    void updateTime()
    {
        counter += Time.deltaTime;
        if (counter >= secondsPerMinute)
        {
            counter -= secondsPerMinute;
            minute++;
            if (minute >= 60)
            {
                minute = 0;
                //end of level logic here
                chompAnim.SetTrigger("endLevel");
            }
        }
    }
}

