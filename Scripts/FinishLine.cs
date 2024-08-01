using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private Player _player;
    private Rigidbody playerRb;
    private LevelManager _levelManager;
    private GameManager _gameManager;
    private ProgressBar _progressBar;
    public GameObject doorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _levelManager = FindObjectOfType<LevelManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _progressBar = FindObjectOfType<ProgressBar>();
        playerRb = _player.gameObject.GetComponent<Rigidbody>();
        StartCoroutine(FinishLineDelay());
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FinishLineDelay();
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;

            InstantiateDoor();

            _progressBar.finishLineTransforms.RemoveAt(0);
            Destroy(gameObject);
            _progressBar.ResetProgressBar();
            _levelManager.NextLevel();
            _gameManager.StartNextLevel();
        }
    }

    private void InstantiateDoor()
    {
        // Kapý nesnesini oluþturun
        GameObject door = Instantiate(doorPrefab, transform.position, Quaternion.identity);
        // Kapýyý seviye nesnesinin çocuðu yapýn
        door.transform.parent = transform.parent;
        door.SetActive(true);
    }

    private IEnumerator FinishLineDelay()
    {
        yield return new WaitForSeconds(0.5f);
    }
}