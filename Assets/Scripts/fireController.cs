using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class fireController : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireSystem;
    [SerializeField] private Light firelight;
    private float lightIntensityMax;
    private float fireEmissionMax;
    [SerializeField] private float woodBurnTime = 60;
    [SerializeField] private float fireStrength;
    [SerializeField] private int maxLog = 5;
    [SerializeField] private float logTime = 20;
    [SerializeField] private AnimationCurve burnCurve;
    [SerializeField] public float burnRateMult;
    [SerializeField] private Slider strengthBar;
    [SerializeField] private AudioSource fireSound;
    [SerializeField] private gameManager manager;
    public chompHelper chomp;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<gameManager>();
        chomp = GameObject.FindObjectOfType<chompHelper>();
        fireEmissionMax = fireSystem.emission.rateOverTime.constantMax;
        lightIntensityMax = firelight.intensity;
        manager.updateData();
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.gamePaused)
        {
            if (woodBurnTime > 0)
            {
                fireStrength = getStrength();
                burnRateMult = burnCurve.Evaluate(fireStrength);
                woodBurnTime -= Time.deltaTime * burnRateMult;
                updateFire();
            }
            else
            {
                woodBurnTime = 0;
            }

            strengthBar.value = fireStrength;
            fireSound.volume = fireStrength;
        }
    }

    private float getStrength()
    {
        float maxTime = maxLog * logTime;
        float s = woodBurnTime / maxTime;
        return s;
    }
    
    public bool addLog()
    {
        if(woodBurnTime < ((maxLog * logTime)-logTime))
        {
            woodBurnTime += logTime;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void updateFire()
    {
        var emission = fireSystem.emission;
        emission.rateOverTime = fireEmissionMax * fireStrength;
        firelight.intensity = lightIntensityMax * fireStrength * (Random.Range(0.9f, 1.1f));
    }

    public float fireStr()
    {
        return fireStrength;
    }
}
