using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    
    [Header("Scares")]
    [SerializeField] private bool leanScareDone=false;
    [SerializeField] private PlayableDirector leanScare;
    [SerializeField] private bool runScaleDone=false;
    [SerializeField] private PlayableDirector runScare;
    [SerializeField] private bool crowDone=false;
    [SerializeField] private PlayableDirector crowAnim;
    

    [Header("Flashlight")] 
    [SerializeField] public bool flashLightOn = false;

    [SerializeField] private Image flashLightIcon;
    
    [SerializeField] private float flashlightBattery = 1.0f;

    [SerializeField] private float flashlightBatteryDrain = 0.1f;
    [SerializeField] private Light flashLight;
    [SerializeField] private float flashlightIntensity;
    
    public bool gamePaused;

    public int endingSceneID = 4;

    public TextMeshProUGUI hourText;
    
    // Start is called before the first frame update
    void Start()
    {
        flashlightIntensity = flashLight.intensity;
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
        FlashLight();
        
        //do all the scares
        if (monster.progress < 0.8f)
        {
            
                int chance = (int)Random.Range(0f, 10000f);
                if (chance < 10 && !leanScareDone)
                {
                    Debug.Log(chance);
                    leanScareDone = true;
                    leanScare.Play();
                }
                else
                {
                    if (!runScaleDone && chance < 10)
                    {
                        runScaleDone = true;
                        runScare.Play();
                    }
                }
            
        }

        if (Random.Range(0f, 12000) < 12)
        {
            if (!crowDone)
            {
                crowDone = true;
                crowAnim.Play();
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

    private void FlashLight()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (flashlightBattery > 0)
            {
                if (flashLightOn)
                {
                    flashLightOn = false;
                }
                else
                {
                    flashLightOn = true;
                }
            }
        }
        
        if (flashLightOn)
        {
            flashLightIcon.fillAmount = flashlightBattery;
            flashLightIcon.color = new Color(1, 1, 1, 1);
            flashLight.intensity = flashlightIntensity;
            if (flashlightBattery < .1f)
            {
                flashLight.intensity = Random.Range(0, flashlightIntensity);
            }

            flashlightBattery -= Time.deltaTime * flashlightBatteryDrain;
            if (flashlightBattery <= 0)
            {
                flashLightOn = false;
            }
        }

        if (flashLightOn == false)
        {
            flashLightIcon.color = new Color(0, 0, 0, 0);
            flashLight.intensity = 0;
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
         flashlightBattery = 1f;
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

        if (flashLightIcon == null)
        {
            flashLightIcon = GameObject.Find("FlashLightIcon").GetComponent<Image>();
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
