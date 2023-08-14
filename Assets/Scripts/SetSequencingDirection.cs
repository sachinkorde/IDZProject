using System.Collections.Generic;
using UnityEngine;

public class SetSequencingDirection : MonoBehaviour
{
    public static SetSequencingDirection instance;
    public List<PencilDirection> directions = new();
    public List<GameObject> placeHolder = new();

    private void Awake()
    {
        instance = this;
    }

    public void ResetAllDirectionInList()
    {
        directions.Clear();
        for (int i = 0; i < placeHolder.Count; i++)
        {
            if (placeHolder[i].transform.childCount > 0)
            {
                directions.Add(placeHolder[i].transform.GetChild(0).transform.GetComponent<PencilProperties>().pencilDirection); //= directions[i];
            }
        }
    }
}
