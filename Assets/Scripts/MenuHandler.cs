using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public static string lvlNumber;

    public void LoadGame(int LvlNum)
    {
        if(LvlNum == 1)
        {
            PlayerPrefs.SetInt(lvlNumber, 0);
        }
        else if(LvlNum == 2)
        {
            PlayerPrefs.SetInt(lvlNumber, 1);
        }

        SceneManager.LoadScene("Game");
    }
}
