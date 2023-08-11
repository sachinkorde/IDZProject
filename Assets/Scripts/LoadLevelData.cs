using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevelData : MonoBehaviour
{
    public List<GameObject> levelInGameObjects = new();
    public LevelCreation level;
    int currentLevel;
    public GameObject pathPlacesForLevelOne;
    public GameObject pathPlacesForLevelTwo;
    public GameObject pathAndGoBtnHolder;
    public GameObject loopBtn;
    public GameObject level2Bottom;
    public GameObject playerPos;

    private RectTransform rectTransform;

    private bool IsIPad(float aspectRatio)
    {
        float iPadAspectRatio = 4.0f / 3.0f;
        float aspectRatioTolerance = 0.05f;
        return Mathf.Abs(aspectRatio - iPadAspectRatio) < aspectRatioTolerance;
    }

    private void Start()
    {
        currentLevel = PlayerPrefs.GetInt(MenuHandler.lvlNumber);
        LoadLevel(currentLevel);
    }

    void LoadLevel(int lvl)
    {
        for (int i = 0; i < level.levels[lvl].linesActiveInLevel.Count; i++)
        {
            int x = level.levels[lvl].linesActiveInLevel[i];

            if (levelInGameObjects.Count > 0
                && levelInGameObjects.Count > level.levels[lvl].linesActiveInLevel.Count)
            {
                levelInGameObjects[x].GetComponent<Image>().enabled = true;
            }
        }

        playerPos.transform.localPosition = level.levels[lvl].playerPos;

        Debug.Log(playerPos.transform.localPosition);
        Debug.Log(level.levels[lvl].playerPos);

        Debug.Log("currentLevel : " + lvl);
        Debug.Log(level.levels[lvl] + " setLevel Here");
        Debug.Log(level.levels[lvl].levelNum + " Manuuaaly Set Level Num");

        float aspectRatio = (float)Screen.width / Screen.height;
        rectTransform = pathAndGoBtnHolder.GetComponent<RectTransform>();

        if (level.levels[lvl].levelNum == 1)
        {
            Debug.Log("Set Level 1 called Sequencing");

            pathPlacesForLevelOne.SetActive(true);
            pathPlacesForLevelTwo.SetActive(false);
            loopBtn.SetActive(false);
            level2Bottom.SetActive(false);

            if (IsIPad(aspectRatio))
            {
                pathAndGoBtnHolder.transform.localPosition = new Vector3(320.0f, -335.0f, 0.0f);
            }
            else
            {
                pathAndGoBtnHolder.transform.localPosition = new Vector3(320.0f, -235.0f, 0.0f);
            }
        }
        else if(level.levels[lvl].levelNum == 2)
        {
            Debug.Log("Set Level 2 called Looping");

            pathPlacesForLevelOne.SetActive(false);
            pathPlacesForLevelTwo.SetActive(true);
            loopBtn.SetActive(true);
            level2Bottom.SetActive(true);

            if (IsIPad(aspectRatio))
            {
                pathAndGoBtnHolder.transform.localPosition = new Vector3(0.0f, -480.0f, 0.0f);
            }
            else
            {
                pathAndGoBtnHolder.transform.localPosition = new Vector3(0.0f, -375.0f, 0.0f);
            }
        }
        rectTransform.anchoredPosition = pathAndGoBtnHolder.transform.localPosition;
    }
}
