using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PencilMovement : MonoBehaviour
{
    public LevelCreation level;

    //public PencilState pencilState = PencilState.Idle;
    public PencilDirection currentDirection;
    public List<PencilDirection> directionsList;
    public List<PencilDirection> directionsListForCompare;

    public bool isPlayerCanMove = false;
    public bool isLerpStop = false;

    [SerializeField] private float distanceCoveredX_axis = 95.0f;
    [SerializeField] private float distanceCoveredY_axis = 95.0f;
    [SerializeField] private float sec_Xaxis = 0.1f;
    [SerializeField] private float sec_Yaxis = 0.1f;

    public Transform leftBoundry, rightBoundry, upBoundry, downBoundry;

    public Image pathHider;

    int currentLevel;
    BoxCollider2D handleCollider;

    private void Awake()
    {
        pathHider.enabled = false;
        currentLevel = PlayerPrefs.GetInt(MenuHandler.lvlNumber);
        handleCollider = transform.GetComponent<BoxCollider2D>();

        Debug.Log("currentLevel : " + currentLevel);
    }

    private void Start()
    {
        handleCollider.enabled = false;
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.localPosition;

        while (time < duration)
        {
            pathHider.enabled = true;
            //pencilState = PencilState.Moving;
            if (!isLerpStop)
            {
                handleCollider.enabled = true;
                transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            }
            else
            {
                StopAllCoroutines();
                yield return null;
            }
            time += Time.deltaTime;
            yield return null;
        }

        if (transform.localPosition != targetPosition)
        {
            transform.localPosition = targetPosition;
        }

        GetPencilDirection();
        handleCollider.enabled = false;
        yield return new WaitForSeconds(0.25f);
    }

    public void LeftMovement()
    {
        transform.rotation = Quaternion.identity;
        Vector3 targetPos = transform.localPosition + new Vector3(-distanceCoveredX_axis, 0, 0);

        if (transform.GetComponent<RectTransform>().localPosition.x > leftBoundry.localPosition.x && isPlayerCanMove)
        {
            StartCoroutine(LerpPosition(targetPos, sec_Xaxis));
        }
    }

    public void RightMovement()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        Vector3 targetPos = transform.localPosition + new Vector3(distanceCoveredX_axis, 0, 0);

        if (transform.GetComponent<RectTransform>().localPosition.x < rightBoundry.localPosition.x && isPlayerCanMove)
        {
            StartCoroutine(LerpPosition(targetPos, sec_Xaxis));
        }
    }

    public void UpMovement()
    {
        Vector3 targetPos = transform.localPosition + new Vector3(0, distanceCoveredY_axis, 0);

        if (transform.GetComponent<RectTransform>().localPosition.y < upBoundry.localPosition.y && isPlayerCanMove)
        {
            StartCoroutine(LerpPosition(targetPos, sec_Yaxis));
        }
    }

    public void DownMovement()
    {
        Vector3 targetPos = transform.localPosition + new Vector3(0, -distanceCoveredY_axis, 0);

        if (transform.GetComponent<RectTransform>().localPosition.y > downBoundry.localPosition.y && isPlayerCanMove)
        {
            StartCoroutine(LerpPosition(targetPos, sec_Yaxis));
        }
    }

    public void OnStartBtnClicked()
    {
        StartCoroutine(SetMovementAccordingToLevel());
    }

    IEnumerator SetMovementAccordingToLevel()
    {
        isPlayerCanMove = true;

        if (currentLevel == 0)
        {
            directionsList.AddRange(SetSequencingDirection.instance.directions);
            directionsListForCompare.AddRange(SetSequencingDirection.instance.directions);
            yield return null;
        }
        else if (currentLevel == 1)
        {
            LoopPlaceHolder.Instance.CombineAllDirection();

            directionsList.AddRange(LoopPlaceHolder.Instance.combinedDirections);
            directionsListForCompare.AddRange(LoopPlaceHolder.Instance.combinedDirections);

            Debug.Log(" reached for level 1");

            Debug.Log(directionsList.Count + "  directionList value");
            Debug.Log(directionsListForCompare.Count + "  directionListForCompare value");
        }

        yield return new WaitForSeconds(0.25f);

        GetPencilDirection();
    }

    public void GetPencilDirection()
    {
        if (directionsList.Count > 0)
        {
            currentDirection = directionsList[0];
            switch (currentDirection)
            {
                case PencilDirection.Up:
                    UpMovement();
                    break;

                case PencilDirection.Down:
                    DownMovement();
                    break;

                case PencilDirection.Left:
                    LeftMovement();
                    break;

                case PencilDirection.Right:
                    RightMovement();
                    break;

                default:
                    break;
            }
            directionsList.RemoveAt(0);
        }
        else
        {
            /*if(currentLevel == 0)
            {

            }else if(currentLevel == 1)
            {

            }*/
            GameManager.instance.CompareLists(directionsListForCompare, level.levels[currentLevel].levelDirection);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Boundry")
        {
            isLerpStop = true;
            GameManager.instance.PopUpForLoose();
        }
    }
}
