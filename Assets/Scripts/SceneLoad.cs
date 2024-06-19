using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject creditMenu;
    public GameObject settingsMenu;

    public TextMeshProUGUI winsL;
    public TextMeshProUGUI winsR;

    private float winsLFloat;
    private float winsRFloat;

    public void Awake()
    {
        winsRFloat = PlayerPrefs.GetFloat("WinsL");
        winsLFloat = PlayerPrefs.GetFloat("WinsR");

        winsL.text = winsLFloat.ToString();
        winsR.text = winsRFloat.ToString();
    }

    public void Start()
    {
        winsRFloat = PlayerPrefs.GetFloat("WinsL");
        winsLFloat = PlayerPrefs.GetFloat("WinsR");

        winsL.text = winsLFloat.ToString();
        winsR.text = winsRFloat.ToString();
    }

    public void ToPong(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitAplication()
    {
        Application.Quit();
    }

    public void ToCredits()
    {
        startMenu.SetActive(false);
        creditMenu.SetActive(true);
    }

    public void ToSettings()
    {
        startMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void ToMainMenu()
    {
        startMenu.SetActive(true);
        creditMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
