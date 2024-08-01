using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMainGameTrigger : MonoBehaviour
{
    private Player _player;
    private Rigidbody playerRb;
    private CheckpointManager _checkpointManager;
    private LevelManager _levelManager;
    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _player = FindObjectOfType<Player>();
        _checkpointManager = FindObjectOfType<CheckpointManager>();
        playerRb = _player.GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            transform.parent.gameObject.SetActive(false);
            if (_checkpointManager.isPlayerPassCheckpoint == true)
            {
                _player.transform.position = _checkpointManager.spawnPoint;
                playerRb.velocity = Vector3.zero;
                playerRb.angularVelocity = Vector3.zero;
            }
            else
            {
                _player.transform.position = _levelManager.playerFirstPos;
                playerRb.velocity = Vector3.zero;
                playerRb.angularVelocity = Vector3.zero;
            }
        }
    }
}
