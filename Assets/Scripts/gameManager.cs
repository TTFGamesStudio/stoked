using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [SerializeField] private hourData[] data;
    [SerializeField] private int hour= 1;
    [SerializeField] private int levelIDOffset = 3;
    [SerializeField] private monsterController monster;
    [SerializeField] private logManager logs;
    [SerializeField] private fireController fire;

    [SerializeField] private clockManager clock;

    [SerializeField] private hourData currentData;

    [SerializeField] private TextMeshProUGUI hourdisplay;

    [SerializeField] private chompHelper chompHelper;
    
    [SerializeField] private bool leanScareDone=false;
    [SerializeField] private PlayableDirector leanScare;
    [SerializeField] private bool runScaleDone=false;
    [SerializeField] private PlayableDirector runScare;
    public bool gamePaused;

    public int endingSceneID = 4;

    public TextMeshProUGUI hourText;
    
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        currentData = data[0];
        updateData();
    }

    // Update is called once per frame
    void Update()
    {
        nullRefChecks();
        if (monster.progress < 0.8f)
        {
            if (!leanScareDone)
            {
                int chance = (int)Random.Range(0f, 10000f);
                Debug.Log(chance);
                if (chance < 50)
                {
                    leanScareDone = true;
                    leanScare.Play();
                }
            }
        }
    }

    public void nextLevel()
    {
        hour++;
        updateData();
        if (hour < data.Length)
        {
            loadLevel();
        }
        else
        {
            SceneManager.LoadScene(endingSceneID);
        }
    }

    public void pauseGame()
    {
        gamePaused = true;
    }

    public void unpauseGame()
    {
        gamePaused = false;
    }
    
    //actually load into the level
     void loadLevel()
    {
        SceneManager.LoadScene(3);
        updateData();
    }

    //used when changing levels
    public void updateData()
    {
        clock = GameObject.FindObjectOfType<clockManager>();
        monster = GameObject.FindObjectOfType<monsterController>();
        logs = GameObject.FindObjectOfType<logManager>();
        fire = GameObject.FindObjectOfType<fireController>();
        hourdisplay = GameObject.Find("HourIndicator").GetComponent<TextMeshProUGUI>();
        currentData = data[hour - 1];

        //actually pull the data from the current data object and use it
        logs.logs = currentData.logs;
        monster.speed = currentData.monsterSpeed * currentData.diffucultyAdjustment;
        fire.burnRateMult = currentData.logBurnSpeed;
        hourdisplay.text = "Hour " + hour;
        clock.secondsPerMinute = currentData.timeScale;
        
        //get the hour indicator and load its data it
       hourdisplay.text ="Hour " + hour;
    }

    void nullRefChecks()
    {
        if (clock == null)
        {
            clock = GameObject.FindObjectOfType<clockManager>();
        }

        if(clock !=null)
        {
            clock.data.isAm = currentData.isAM;
            clock.hour = currentData.hourString;
        }

        if (monster == null)
        {
            monster = GameObject.FindObjectOfType<monsterController>();
        }

        if (logs == null)
        {
            logs = GameObject.FindObjectOfType<logManager>();
        }

        if (fire == null)
        {
            fire = GameObject.FindObjectOfType<fireController>();
        }

        if(hourText == null)
        {
            hourText = GameObject.Find("Hour Display").GetComponent<TextMeshProUGUI>();
            if(hourText!=null)
            {
                switch(hour)
                {
                    case 1:
                        hourText.text = "First Hour";
                        break; 
                    case 2:
                        hourText.text = "Second Hour";
                        break; 
                    case 3:
                        hourText.text = "Third Hour";
                        break;
                    case 4:
                        hourText.text = "Fourth Hour";
                        break;
                    case 5:
                        hourText.text = "Fifth Hour";
                        break;
                    case 6:
                        hourText.text = "Sixth Hour";
                        break;
                    case 7:
                        hourText.text = "Seventh Hour";
                        break;
                }
            }
        }

        if (hourdisplay == null)
        {
            try
            {
                hourdisplay = GameObject.Find("HourIndicator").GetComponent<TextMeshProUGUI>();
            }
            catch
            {

            }
        }


        if (hourdisplay != null)
        {
            hourdisplay.text = "Hour " + hour;
        }

        //process the chomp helper this is required for going to the next level
        if (chompHelper == null)
        {
            chompHelper = GameObject.FindAnyObjectByType<chompHelper>();
        }
        if (hour == 7)
        {
            chompHelper.nextLevelId = 4;
        }
        else
            chompHelper.nextLevelId = 3;
    }
}
