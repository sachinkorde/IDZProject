using UnityEngine;
using UnityEngine.EventSystems;

public class LoopDragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject loopBtnDragged;
    private RectTransform rect;
    [SerializeField] private Canvas canvas;
    public bool isdrag;
    Transform startParent;
    Vector3 startPos;

    private void Awake()
    {
        rect = transform.gameObject.GetComponent<RectTransform>();
    }

    private void Start()
    {
        startParent = transform.parent;
        Debug.Log(startParent + "   parent holder");

        startPos = transform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (loopBtnDragged == null)
        {
            isdrag = true;
            transform.localScale = Vector3.one;
            loopBtnDragged = gameObject;
            SetTappingOff();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (gameObject == loopBtnDragged)
        {
            SetTappingOff();
            isdrag = true;
            rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(gameObject == loopBtnDragged)
        {
            if(transform.parent.name != "LoopPlace")
            {
                SetTappingOn();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "LoopPlace")
        {
            if (gameObject == loopBtnDragged)
            {
                loopBtnDragged = null;
                isdrag = false;
                SetTappingOn();
            }

            GameObject tmp = Instantiate(GameManager.instance.loopGameObjectHoder, collision.transform);
            tmp.transform.localScale = Vector3.one;
        }
    }

    void SetTappingOff()
    {
        transform.localScale = Vector3.one;
        transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
        //transform.SetParent(GameManager.instance.dragParent);
    }

    void SetTappingOn()
    {
        transform.SetParent(startParent);
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;

        transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.GetComponent<BoxCollider2D>().enabled = true;
        transform.localPosition = startPos;
    }
}
