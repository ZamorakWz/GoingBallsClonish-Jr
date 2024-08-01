using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public Level[] levels;
    public int currentLevel;
    private int savedLevel;
    public Vector3 playerFirstPos;
    private Player _player;
    private ProgressBar _progressBar;
    private GameManager _gameManager;
    private CheckpointManager _checkpointManager;
    BallSelect _ballSelect;
    private float posX, posY, posZ;

    [SerializeField]
    private TMP_Text[] levelTexts = new TMP_Text[4];

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        savedLevel = PlayerPrefs.GetInt("currentLevel", currentLevel);
        currentLevel = savedLevel;

        posX = PlayerPrefs.GetFloat("PlayerPosX", playerFirstPos.x);
        posY = PlayerPrefs.GetFloat("PlayerPosY", playerFirstPos.y);
        posZ = PlayerPrefs.GetFloat("PlayerPosZ", playerFirstPos.z);

        if (currentLevel == 0)
        {

            playerFirstPos = new Vector3(0.6f, 5.5f, -7f);
        }
        else
        {
            playerFirstPos = new Vector3(posX, posY, posZ);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _ballSelect = FindObjectOfType<BallSelect>();
        _progressBar = gameObject.GetComponent<ProgressBar>();
        _gameManager = gameObject.GetComponent<GameManager>();
        _checkpointManager = FindObjectOfType<CheckpointManager>();

        _player.transform.position = playerFirstPos;

        levelTexts[0].text = currentLevel.ToString();
        levelTexts[1].text = (currentLevel + 1).ToString();
        levelTexts[2].text = (currentLevel + 2).ToString();
        levelTexts[3].text = (currentLevel + 3).ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartLevel()
    {

    }

    public void NextLevel()
    {
        currentLevel++;
        _progressBar.fillImg.fillAmount = 0f;
        _progressBar.SetLevelTexts();
        playerFirstPos = _player.transform.position;
        _checkpointManager.spawnPoint = playerFirstPos;
        _checkpointManager.isPlayerPassCheckpoint = false;
        SavePlayerPos();
        //playerFirstPos = new Vector3(posX, posY, posZ);
    }

    public void EndLevel()
    {
        _gameManager.EndGame();
        _gameManager.inGameUI.SetActive(false);
        _gameManager.endLevelUI.SetActive(true);
    }

    private void SavePlayerPos()
    {
        PlayerPrefs.SetInt("currentLevel", currentLevel);
        PlayerPrefs.SetFloat("PlayerPosX", playerFirstPos.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerFirstPos.y);
        PlayerPrefs.SetFloat("PlayerPosZ", playerFirstPos.z);
        PlayerPrefs.Save();
    }
}
