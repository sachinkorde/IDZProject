using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount <= 0)
        {
            if (DragPath.itemBeingDragged != null)
            {
                DragPath.itemBeingDragged.transform.SetParent(transform);
                DragPath.itemBeingDragged.transform.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            }
        }
        else
        {
            return;
        }
    }
}