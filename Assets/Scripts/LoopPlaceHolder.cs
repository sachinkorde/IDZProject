using System.Collections.Generic;
using UnityEngine;

public class LoopPlaceHolder : MonoBehaviour
{
    public static LoopPlaceHolder Instance { get; private set; }
    public List<GameObject> loopPlaces;
    public List<PencilDirection> combinedDirections;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < loopPlaces.Count; i++) 
        {
            loopPlaces[i].SetActive(false);
        }
        loopPlaces[0].SetActive(true);
    }

    public void ActivateLoopPlace()
    {
        for (int i = 0; i < loopPlaces.Count; i++)
        {
            if (loopPlaces[i].transform.childCount > 1)
            {
                Destroy(loopPlaces[i].transform.GetChild(1).gameObject);
            }

            if (loopPlaces[i].transform.childCount > 0)
            {
                if (i < loopPlaces.Count)
                {
                    loopPlaces[i].SetActive(true);
                    loopPlaces[i + 1].SetActive(true);
                    Debug.Log(i + " childcount" + loopPlaces[i].transform.childCount);
                }
                //break;
            }
        }        
    }

    public void CombineAllDirection()
    {
        for (int i = 0; i < loopPlaces.Count; i++)
        {
            if(loopPlaces[i].transform.childCount > 0)
            {
                SetLoopingDirection loopingDirectionComponent = loopPlaces[i].transform.GetChild(0).GetComponent<SetLoopingDirection>();
                
                if (loopingDirectionComponent != null)
                {
                    combinedDirections.AddRange(loopingDirectionComponent.directions);
                }
            }
        }
    }
}
