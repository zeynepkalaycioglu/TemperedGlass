using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    public Text  timeCounter;

    private float gameTime = 30f;
    private float waitTime = 3f;
    float startTime;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        { 
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isGamePaused == false)
        {
            waitTime = waitTime - Time.deltaTime;
            if (waitTime <= 0)
            {
                gameTime = gameTime - Time.deltaTime;
            }
            timeCounter.text = "Time:00: " + gameTime.ToString("0");;
        
        }
        else
        {
            
        }
        if (gameTime <= 0)
        { 
            timeCounter.text = "Time: 0";
            GameManager.Instance.GameOver();
        }
    }

   
    
}
