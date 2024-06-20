using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackgroundSelect : MonoBehaviour
{
    public GameObject[] backgrounds;
    public int selectedBackground;
    public Image selectedBackgroundImage;
    public Background[] backgroundDetails;
    public Button buyButton;
    public TMP_Text coinsTextMainMenuUI, coinsTextChangeBackgroundUI;
    public Button continueButton;
    public Button backButton;

    private void Awake()
    {
        selectedBackground = PlayerPrefs.GetInt("selectedBackground", 0);
        selectedBackgroundImage = backgrounds[selectedBackground].GetComponent<Image>();
        foreach (GameObject background in backgrounds)
        {
            background.SetActive(false);
        }
        backgrounds[selectedBackground].SetActive(true);

        foreach (Background b in backgroundDetails)
        {
            if (b.price == 0)
            {
                b.isUnlocked = true;
            }
            else
            {
                b.isUnlocked = PlayerPrefs.GetInt(b.name, 0) == 0 ? false : true;
            }
        }
        UpdateUI();
    }
    public void NextBackground()
    {
        backgrounds[selectedBackground].SetActive(false);
        selectedBackground++;
        if (selectedBackground == backgrounds.Length)
        {
            selectedBackground = 0;
        }
        backgrounds[selectedBackground].SetActive(true);
        selectedBackgroundImage = backgrounds[selectedBackground].GetComponent<Image>();
        if (backgroundDetails[selectedBackground].isUnlocked)
        {
            PlayerPrefs.SetInt("selectedBackground", selectedBackground);
            PlayerPrefs.Save();
        }
        UpdateUI();
    }

    public void PreviousBackground()
    {
        backgrounds[selectedBackground].SetActive(false);
        selectedBackground--;
        if (selectedBackground == -1)
        {
            selectedBackground = backgrounds.Length - 1;
        }
        backgrounds[selectedBackground].SetActive(true);
        selectedBackgroundImage = backgrounds[selectedBackground].GetComponent<Image>();
        if (backgroundDetails[selectedBackground].isUnlocked)
        {
            PlayerPrefs.SetInt("selectedBackground", selectedBackground);
            PlayerPrefs.Save();
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        coinsTextChangeBackgroundUI.text = PlayerPrefs.GetInt("numberOfCoins", 0).ToString();
        coinsTextMainMenuUI.text = PlayerPrefs.GetInt("numberOfCoins", 0).ToString();
        if (backgroundDetails[selectedBackground].isUnlocked)
        {
            buyButton.gameObject.SetActive(false);
            continueButton.interactable = true;
            backButton.interactable = true;
        }
        else
        {
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = backgroundDetails[selectedBackground].price.ToString();
            buyButton.gameObject.SetActive(true);
            continueButton.interactable = false;
            backButton.interactable = false;

            // Kontrol eklenen kýsým
            int coins = PlayerPrefs.GetInt("numberOfCoins", 0);
            int price = backgroundDetails[selectedBackground].price;
            if (coins < price && backgroundDetails[selectedBackground].isUnlocked == false)
            {
                buyButton.interactable = false;
                continueButton.interactable = false;
                backButton.interactable = false;
            }
            else
            {
                buyButton.interactable = true;
            }
        }
    }

    public void BuyBackground()
    {
        int coins = PlayerPrefs.GetInt("numberOfCoins", 0);
        int price = backgroundDetails[selectedBackground].price;

        if (coins >= price)
        {
            PlayerPrefs.SetInt("numberOfCoins", coins - price);
            PlayerPrefs.SetInt(backgroundDetails[selectedBackground].name, 1);
            PlayerPrefs.SetInt("selectedBackground", selectedBackground);
            PlayerPrefs.Save();
            backgroundDetails[selectedBackground].isUnlocked = true;
            continueButton.interactable = true;
            UpdateUI();
        }
    }
}