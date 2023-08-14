using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetLoopingDirection : MonoBehaviour
{
    public static SetLoopingDirection instance;
    public Button incrementBtn;
    public int incrementCounter = 2;
    public Button resetBtn;
    public List<PencilDirection> directions;
    public GameObject place;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        directions = new List<PencilDirection>();
        incrementCounter = 2;
        incrementBtn.transform.GetChild(0).GetComponent<TMP_Text>().text = incrementCounter.ToString();
    }

    public void SetDirectionForGame()
    {
        directions.Add(place.transform.GetChild(0).GetComponent<PencilProperties>().pencilDirection);
        directions.Add(place.transform.GetChild(0).GetComponent<PencilProperties>().pencilDirection);
        incrementCounter = 2;
        incrementBtn.transform.GetChild(0).GetComponent<TMP_Text>().text = incrementCounter.ToString();

        foreach (PencilDirection dir in directions)
        {
            Debug.Log("Value in list: " + dir);
        }
    }

    public void IncrementDirectionInList()
    {
        if (place.transform.childCount > 0)
        {
            directions.Add(place.transform.GetChild(0).GetComponent<PencilProperties>().pencilDirection);
            incrementCounter++;
            incrementBtn.transform.GetChild(0).GetComponent<TMP_Text>().text = incrementCounter.ToString();
        }
    }

    public void ResetAllDirectionInList() 
    {
        if(directions.Count > 0)
        {
            directions.Clear();
        }
        
        SetDirectionForGame();
    }
}
