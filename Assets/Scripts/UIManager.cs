using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
    public GameObject gamePlayingPanel;
    public GameObject gamePausePanel;

    public LevelManager levelManager;
    
    public Text scoreText;

    private int _currentScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        scoreText.text = _currentScore.ToString();
        gamePlayingPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        levelCompletedPanel.SetActive(false);
    }
    public void GlassPassed(int currentScore)
    {
        scoreText.text = currentScore.ToString();
    }
    
    public void GameEnded()
    {
        gameOverPanel.SetActive(true);
    }
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void GamePaused()
    {
         gamePausePanel.SetActive(true); 
         GameManager.Instance.isGamePaused = true;
         PlayerController.Instance.right.interactable = false;
         PlayerController.Instance.left.interactable = false;
    }

    public void ResumeGame ()
    {
        gamePausePanel.SetActive(false); 
        GameManager.Instance.isGamePaused = false;
        PlayerController.Instance.right.interactable = true;
        PlayerController.Instance.left.interactable = true;
    }

    public void LevelCompletedScene()
    {
        levelCompletedPanel.SetActive(true);

    }
    
    public void LoadScene()
    {
        SceneManager.LoadScene(GameManager.Instance.levelManager.nextLevel);
        GameManager.Instance.StartGame();
    }

 
}
