using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class DragDuplicatePaths : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemDragged;
    public RectTransform rect;
    public bool isdragDuplicate = false;
    public Image image;
    public Color color;
    public Color endColor;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        color = image.color;
        endColor = new Color(color.r, color.g, color.b, 0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemDragged == null)
        {
            Debug.Log("Here is drag start");
            isdragDuplicate = true;
            itemDragged = gameObject;
            SetTappingOff();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (gameObject == itemDragged)
        {
            SetTappingOff();
            rect.anchoredPosition += eventData.delta / GameManager.instance.canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent.name == "Place")
        {
            Debug.Log("reached again in slot");
            SetTappingOn();
        }
        else
        {
            StartCoroutine(FadeImage(endColor, 0.3f, image));
            Debug.Log("Destroyed Peacefully");
        }
        itemDragged = null;
    }

    void SetTappingOff()
    {
        Debug.Log(itemDragged.name);
        transform.localScale = Vector3.one;
        transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.GetComponent<BoxCollider2D>().enabled = false;
        transform.SetParent(GameManager.instance.dragParent);
    }

    void SetTappingOn()
    {
        transform.localScale = Vector3.one;
        transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.GetComponent<BoxCollider2D>().enabled = true;
        isdragDuplicate = false;
    }

    IEnumerator FadeImage(Color targetColor, float duration, Image imageToFade)
    {
        float timer = 0f;
        Color startColor = imageToFade.color;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            imageToFade.color = Color.Lerp(startColor, targetColor, timer / duration);
            yield return null;
        }

        imageToFade.color = targetColor;
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);

        if (PlayerPrefs.GetInt(MenuHandler.lvlNumber) == 0)
        {
            SetSequencingDirection.instance.ResetAllDirectionInList();
            Debug.Log("Reset Sequence Level 1 => Sequencing Level");
        }
    }
}
