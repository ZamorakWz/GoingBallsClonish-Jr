using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private LevelManager _levelManager;
    private ProgressBar _progressBar;

    public GameObject MenuUI;
    public GameObject inGameUI;
    public GameObject endLevelUI;
    public GameObject ChangeBallUI;

    public enum GameState
    {
        Start, Pause, End
    }

    public GameState currentGameState;

    // Start is called before the first frame update
    void Start()
    {
        currentGameState = GameState.Pause;
        _levelManager = FindObjectOfType<LevelManager>();
        _progressBar = FindObjectOfType<ProgressBar>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MainMenu()
    {
        currentGameState = GameState.Pause;
        MenuUI.SetActive(true);
    }

    public void StartGame()
    {
        currentGameState = GameState.Start;
        MenuUI.SetActive(false);
        endLevelUI.SetActive(false);
        ChangeBallUI.SetActive(false);
        inGameUI.SetActive(true);
        if (_progressBar == null)
        {
            _progressBar = FindObjectOfType<ProgressBar>();
        }

        if (_progressBar == null)
        {
            Debug.LogError("Progress Bar is null");
        }
        else
        {
            _progressBar.SetLevelTexts();
        }
    }

    public void StartNextLevel()
    {
        currentGameState = GameState.Start;
        inGameUI.SetActive(true);
    }

    public void EndGame()
    {
        currentGameState = GameState.End;
    }
}
