using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject optionsMenu;

    public void Pause()
    {
        optionsMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        optionsMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}