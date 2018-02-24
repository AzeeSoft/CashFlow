using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public Sprite[] cashJarSprites;

    State curState = State.Playing;
    PlayerController playerController;

    float timeLeft = 0;

    [HideInInspector]
    public int curCash = 0;

    void Awake()
    {
        timeLeft = initTimer;
        curState = State.Playing;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
        if (curCash > cashTarget)
            curCash = cashTarget;
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