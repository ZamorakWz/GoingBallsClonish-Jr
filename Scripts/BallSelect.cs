using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallSelect : MonoBehaviour
{
    public GameObject[] balls;
    public int selectedBall;
    public Transform selectedBallTransform;
    public Ball[] characters;
    public Button unlockButton;
    public TMP_Text coinsTextMainMenuUI, coinsTextInGameUI, coinsTextChangeBallUI;
    public Button continueButton;
    public Button backButton;

    private void Awake()
    {
        selectedBall = PlayerPrefs.GetInt("selectedBall", 0);
        selectedBallTransform = balls[selectedBall].transform;
        foreach (GameObject player in balls)
        {
            player.SetActive(false);
        }
        balls[selectedBall].SetActive(true);

        foreach (Ball b in characters)
        {
            if (b.price == 0)
            {
                b.isUnlocked = true;
            }
            else
            {
                b.isUnlocked = PlayerPrefs.GetInt(b.name, 0) == 0 ? false : true; // Eðer "PlayerPrefs.GetInt(b.name, 0) == 0" bu koþul saðlanýyorsa, "b.isUnlocked" bu bool'u false yap, else ise true yap
            }
        }
        UpdateUI();
    }

    private void LateUpdate()
    {
        coinsTextInGameUI.text = PlayerPrefs.GetInt("numberOfCoins", 0).ToString();
    }

    public void NextBall()
    {
        balls[selectedBall].SetActive(false);
        selectedBall++;
        if (selectedBall == balls.Length)
        {
            selectedBall = 0;
        }
        balls[selectedBall].SetActive(true);
        selectedBallTransform = balls[selectedBall].transform;
        if (characters[selectedBall].isUnlocked)
        {
            PlayerPrefs.SetInt("selectedBall", selectedBall);
            PlayerPrefs.Save();
        }
        UpdateUI();
    }

    public void PreviousBall()
    {
        balls[selectedBall].SetActive(false);
        selectedBall--;
        if (selectedBall == - 1)
        {
            selectedBall = balls.Length - 1;
        }
        balls[selectedBall].SetActive(true);
        selectedBallTransform = balls[selectedBall].transform;
        if (characters[selectedBall].isUnlocked)
        {
            PlayerPrefs.SetInt("selectedBall", selectedBall);
            PlayerPrefs.Save();
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        coinsTextChangeBallUI.text = PlayerPrefs.GetInt("numberOfCoins", 0).ToString();
        coinsTextMainMenuUI.text = PlayerPrefs.GetInt("numberOfCoins", 0).ToString();
        if (characters[selectedBall].isUnlocked)
        {
            unlockButton.gameObject.SetActive(false);
            continueButton.interactable = true;
            backButton.interactable = true;
        }
        else
        {
            unlockButton.GetComponentInChildren<TextMeshProUGUI>().text = characters[selectedBall].price.ToString();
            unlockButton.gameObject.SetActive(true);
            continueButton.interactable = false;
            backButton.interactable = false;

            // Kontrol eklenen kýsým
            int coins = PlayerPrefs.GetInt("numberOfCoins", 0);
            int price = characters[selectedBall].price;
            if (coins < price)
            {
                unlockButton.interactable = false;
                continueButton.interactable = false;
                backButton.interactable = false;
            }
            else
            {
                unlockButton.interactable = true;
            }
        }
    }

    public void Unlock()
    {
        int coins = PlayerPrefs.GetInt("numberOfCoins", 0);
        int price = characters[selectedBall].price;
        PlayerPrefs.SetInt("numberOfCoins", coins - price);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt(characters[selectedBall].name, 1);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("selectedBall", selectedBall);
        PlayerPrefs.Save();
        characters[selectedBall].isUnlocked = true;
        continueButton.interactable = true;
        UpdateUI();
    }
}
