using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBonusLevelTrigger : MonoBehaviour
{
    private Player _player;
    private Vector3 bonusLevelStartPosition;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        bonusLevelStartPosition = new Vector3(195.5f, 0.5f, 1.1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.transform.position = bonusLevelStartPosition;
            gameObject.SetActive(false);
            transform.parent.gameObject.SetActive(false);
        }
    }
}
