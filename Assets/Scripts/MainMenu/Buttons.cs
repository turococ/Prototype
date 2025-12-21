using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public GameObject Credits;
    public GameObject Volume;
    public GameObject LevelMenu;
    public GameObject selectedFrame;
    private GameObject currentSelectedButton;
    private string selectedLevelName;

    private LimbUI PLUI;

    private void Start()
    {
        PLUI = GetComponent<LimbUI>();
    }

    public void OpenLevelMenu()
    {
        LevelMenu.SetActive(true);
    }

    public void CloseLevelMenu()
    {
        LevelMenu.SetActive(false);
    }

    public void OpenCredits()
    {
        Credits.SetActive(true);
        Volume.SetActive(true);
    }

    public void CloseCredits()
    {
        Credits.SetActive(false);
        Volume.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SelectLevel(GameObject levelButton)
    {
        currentSelectedButton = levelButton;

        selectedFrame.SetActive(true);
        selectedFrame.transform.position = levelButton.transform.position;

        selectedLevelName = levelButton.name;
    }

    public void PlaySelectedLevel()
    {
        if (!string.IsNullOrEmpty(selectedLevelName))
        {
            SceneManager.LoadScene(selectedLevelName);
        }
        else
        {
            Debug.LogWarning("Уровень не выбран!");
        }
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("menu");
    }

    public void Resume()
    {
        PLUI.isPaused = false;
    }
}
