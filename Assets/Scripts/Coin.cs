using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Player.numberOfCoins++;
            PlayerPrefs.SetInt("numberOfCoins", Player.numberOfCoins);
            PlayerPrefs.Save();
            Destroy(gameObject);
        }
    }
}
