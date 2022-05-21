using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MainMenu_UI : MonoBehaviour
{
    [Header("Name Picker")]
    public GameObject namePickerPanel;
    public InputField nameInput;
    [Header("Start Game")]
    public GameObject startGamePanel;
    public Text highScore;
    public Text nameDisplay;

    private void OnEnable()
    {
        SaveSystem.OnGameLoaded += LoadInterface;
    }

    private void Start()
    {
        SaveSystem.Instance.LoadGameData();
    }

    public void AssignUsername()
    {
        if (SaveSystem.Instance != null)
        {
            SaveSystem.Instance.username = nameInput.text;
            SaveSystem.Instance.highScore = 0;
            SaveSystem.Instance.SaveGameData();
            LoadInterface(nameInput.text, 0);
        }
    }

    public void LoadInterface(string v_username, int v_highScore)
    {
        namePickerPanel.SetActive(false);
        nameDisplay.text = "Welcome " + v_username;
        highScore.text = "High Score : " + v_highScore;
        startGamePanel.SetActive(true);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        SaveSystem.Instance.SaveGameData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void OnDisable()
    {
        SaveSystem.OnGameLoaded -= LoadInterface;
    }
}
