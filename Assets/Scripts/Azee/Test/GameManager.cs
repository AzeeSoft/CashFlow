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
    public GameObject winScreen;
    public GameObject loseScreen;

    public float initTimer = 60;
    public float cashTarget = 200;
    public bool autoIncrementCash = false;

    State curState = State.Playing;

    float timeLeft = 0;
    public int curCash = 0;

    void Awake()
    {
        timeLeft = initTimer;
        curState = State.Playing;
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
    }


    void refreshTimerText()
    {
        timerText.text = "Time Left: " + (int) (timeLeft) + "s";
    }

    void refreshCashDisplay()
    {
        cashText.text = "Cash: $" + curCash + " / $" + cashTarget;
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