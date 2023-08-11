using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DragPath : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public PencilMovement pencilMovement;
    public static GameObject itemBeingDragged;
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
        startParent = transform.GetComponent<PencilProperties>().parentOfPath.transform;
        startPos = transform.GetComponent<PencilProperties>().startPos;

        transform.GetComponent<DragPath>().enabled = true;
        transform.GetComponent<DragDuplicatePaths>().enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemBeingDragged == null)
        {
            isdrag = true;
            transform.localScale = Vector3.one;
            itemBeingDragged = gameObject;
            SetTappingOff();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (gameObject == itemBeingDragged)
        {
            SetTappingOff();

            isdrag = true;
            rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (gameObject == itemBeingDragged)
        {
            SetPropertiesAccordingAtPlace();
            itemBeingDragged = null;
            isdrag = false;
            SetTappingOn();
        }
    }

    void SetPropertiesAccordingAtPlace()
    {
        if (transform.parent.name == "Place")
        {
            GameObject tmp;
            tmp = Instantiate(gameObject, startParent);
            tmp.name = transform.name;
            tmp.GetComponent<CanvasGroup>().blocksRaycasts = true;
            tmp.GetComponent<DragPath>().enabled = true;
            tmp.GetComponent<DragDuplicatePaths>().enabled = false;
            tmp.transform.localScale = Vector3.one;

            if (PlayerPrefs.GetInt(MenuHandler.lvlNumber) == 0)
            {
                pencilMovement.directionsList.Add(transform.GetComponent<PencilProperties>().pencilDirection);
                pencilMovement.directionsListForCompare.Add(transform.GetComponent<PencilProperties>().pencilDirection);
                Debug.Log("reached here for Level 1 => Sequencing Level");
            }
            else if (PlayerPrefs.GetInt(MenuHandler.lvlNumber) == 1)
            {
                SetLoopingDirection.instance.ResetAllDirectionInList();
                transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
                LoopPlaceHolder.Instance.ActivateLoopPlace();
                Debug.Log("reached here for Level 2 => Looping Level");
            }

            transform.GetComponent<DragPath>().enabled = false;
            transform.gameObject.GetComponent<DragDuplicatePaths>().enabled = true;
            transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            transform.DOLocalMove(startPos, 0.25f);
            transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
            Debug.Log("Not Placed Correctly");
        }

        Debug.Log(itemBeingDragged.name);
        itemBeingDragged = null;
    }

    void SetTappingOff()
    {
        Debug.Log(itemBeingDragged.name);
        transform.localScale = Vector3.one;
        transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.GetComponent<BoxCollider2D>().enabled = false;
        //transform.SetParent(GameManager.instance.dragParent);
    }

    void SetTappingOn()
    {
        transform.localScale = Vector3.one;
        transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.GetComponent<BoxCollider2D>().enabled = true;
    }
}
