﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        Playing,
        Won,
        Lost
    }

    public Text timerText;
    public Text cashText;
    public Image cashJarImage;
    public GameObject winScreen;
    public GameObject loseScreen;

    public float initTimer = 60;
    public int cashTarget = 200;
    public bool autoIncrementCash = false;
    public float initialContrast = 1.2f;
    public float initialHueShift = 0f;

    public Sprite[] cashJarSprites;

    State curState = State.Playing;
    PlayerController playerController;
    PostProcessingBehaviour postProcessingBehaviour;

    float timeLeft = 0;

    public int curCash = 0;

    void Awake()
    {
        timeLeft = initTimer;
        curState = State.Playing;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        postProcessingBehaviour = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>();

        ColorGradingModel.Settings settings = postProcessingBehaviour.profile.colorGrading.settings;
        settings.basic.contrast = initialContrast;
        settings.basic.hueShift = initialHueShift;
        postProcessingBehaviour.profile.colorGrading.settings = settings;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (curState)
        {
            case State.Playing:
                timeLeft -= Time.deltaTime;
                if (timeLeft < 0)
                    timeLeft = 0;

                if (autoIncrementCash)
                {
                    addCash(1);
                }

                refreshTimerText();
                refreshCashDisplay();

                if (curCash >= cashTarget)
                {
                    onCashTargetReached();
                } else if (timeLeft <= 0)
                {
                    onTimeOut();
                }

                break;
            case State.Won:

                break;
            case State.Lost:

                break;
        }
    }


    public void addCash(int cash)
    {
        curCash += cash;
        curCash = Mathf.Clamp(curCash, 0, cashTarget);
    }


    void refreshTimerText()
    {
        timerText.text = "Time Left: " + (int) (timeLeft) + "s";
    }

    void refreshCashDisplay()
    {
        cashText.text = "Cash: $" + curCash + " / $" + cashTarget;
        refreshCashJar();
    }

    void refreshCashJar()
    {
        int cashJarSpriteIndex = (int)StaticTools.Remap(curCash, 0, cashTarget, 0, cashJarSprites.Length - 1);

        if (cashJarSpriteIndex == 0 && curCash > 0 && cashJarSprites.Length>1)
        {
            cashJarSpriteIndex = 1;
        }

        cashJarImage.sprite = cashJarSprites[cashJarSpriteIndex];
    }

    public void onCashStolen()
    {
        StartCoroutine(animateOnCashStolen());
    }

    IEnumerator animateOnCashStolen()
    {
        Debug.Log("Animating on cash stolen");
        ColorGradingModel.Settings settings =  postProcessingBehaviour.profile.colorGrading.settings;
        float deltaContrast = 0.2f;
        float deltaHueShift = 20f;

        float origContrast = 1.2f;
        float origHueShift = 0;

        while (settings.basic.contrast < 2 && settings.basic.hueShift > -80)
        {
            settings.basic.contrast += deltaContrast;
            settings.basic.hueShift -= deltaHueShift;

            if (settings.basic.contrast > 2)
            {
                settings.basic.contrast = 2;
            }
            if (settings.basic.hueShift < -80)
            {
                settings.basic.hueShift = -80;
            }

            postProcessingBehaviour.profile.colorGrading.settings = settings;
            Debug.Log("Changing contrast to " + settings.basic.contrast);
            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSeconds(0.05f);

        while (settings.basic.contrast > origContrast && settings.basic.hueShift < origHueShift)
        {
            settings.basic.contrast -= deltaContrast;
            settings.basic.hueShift += deltaHueShift;

            if (settings.basic.contrast < origContrast)
            {
                settings.basic.contrast = origContrast;
            }
            if (settings.basic.hueShift > origHueShift)
            {
                settings.basic.hueShift = origHueShift;
            }

            postProcessingBehaviour.profile.colorGrading.settings = settings;
            yield return new WaitForSeconds(0.025f);
        }
    }

    void onCashTargetReached()
    {
        win();
    }

    void onTimeOut()
    {
        lose();
    }

    void win()
    {
        curState = State.Won;
        winScreen.SetActive(true);
    }

    void lose()
    {
        curState = State.Lost;
        loseScreen.SetActive(true);
    }
}