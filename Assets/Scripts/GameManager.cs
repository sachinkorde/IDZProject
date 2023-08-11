using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PencilState
{
    Idle,
    Moving
}

public enum PencilDirection
{
    None,
    Up,
    Down,
    Left,
    Right
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform dragParent;
    public GameObject leftPath;
    public GameObject rightPath;
    public GameObject upPath;
    public GameObject downPath;
    public Canvas canvas;

    public GameObject popUp;
    public TMP_Text popUpText;

    public GameObject loopGameObjectHoder;
    public GameObject nextLevelBtn;

    private void Awake()
    {
        instance = this;
    }

    void SetMessage(string message)
    {
        popUpText.text = message;
    }

    public void PopUpForWin()
    {
        popUp.SetActive(true);
        nextLevelBtn.SetActive(true);
        SetMessage("Level Cleared");
    }

    public void PopUpForLoose()
    {
        popUp.SetActive(true);
        nextLevelBtn.SetActive(false);
        SetMessage("Play Again");
    }

    public void ClosePopUp()
    {
        popUp.SetActive(false);
        SetMessage("");
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CompareLists(List<PencilDirection> list1, List<PencilDirection> list2)
    {
        if (list1.Count != list2.Count)
        {
            Invoke(nameof(PopUpForLoose), 0.35f);
            return;
        }

        bool listsAreEqual = true;

        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i])
            {
                listsAreEqual = false;
                break;
            }
        }

        if (listsAreEqual)
        {
            Invoke(nameof(PopUpForWin), 0.35f);
            Debug.Log("Player win");
        }
        else
        {
            Invoke(nameof(PopUpForLoose), 0.35f);
            Debug.Log("Player loose");
        }
    }

    public void SetNextLevel()
    {
        if(PlayerPrefs.GetInt(MenuHandler.lvlNumber) == 0)
        {
            PlayerPrefs.SetInt(MenuHandler.lvlNumber, 1);
        }
        else if(PlayerPrefs.GetInt(MenuHandler.lvlNumber) == 1)
        {
            PlayerPrefs.SetInt(MenuHandler.lvlNumber, 0);
        }

        ReloadScene();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}