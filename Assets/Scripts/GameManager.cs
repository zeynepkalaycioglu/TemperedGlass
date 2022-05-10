using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public UIManager uiManager;
    public GlassManager glassManager;
    public LevelManager levelManager;

    public int score = 0 ;

    public bool isGameStarted = false;
    public bool isGamePaused = false;
    public bool isLevelCompleted = false;
    public bool isAdWatched = false;
    
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
    public void Start()
    {
        if (!isGameStarted)
        {
            StartGame();
        }
        SoundManager.Instance.PlayBgMusic(SoundManager.BgMusicTypes.MainBgMusic);

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void StartGame()
    {
        score = 0;
        isGameStarted = true;
        PlayerController.Instance.StartGame();
        uiManager.GameStart();
        
    }
    public void GameOver()
    {
        PlayerController.Instance.GameEnded();
        uiManager.GameEnded();
    }
    
   


    public void GlassPassed(GameObject TemperedGlass)
    {
        score++;
        uiManager.GlassPassed(score);
        glassManager.GlassPassed(TemperedGlass);
        if (PlayerController.Instance.playerAnimator == null)
        {
            PlayerController.Instance.playerAnimator = PlayerController.Instance.player.GetComponentInChildren<Animator>();
        }

        if (PlayerController.Instance.playerAnimator)
        {
             PlayerController.Instance.playerAnimator.SetBool("isLeftJumping", false);
             PlayerController.Instance.playerAnimator.SetBool("isRightJumping", false);
        }

    }

    public void Win()
    {
        PlayerController.Instance.player.transform.position = PlayerController.Instance.winPosition[0].transform.position;
        uiManager.LevelCompletedScene();
        isLevelCompleted = true;
        isGamePaused = true;
    }

    public void PlayAd()
    {
        //sdk
    }
    
    public void AdWatched()
    {
        
    }
}
