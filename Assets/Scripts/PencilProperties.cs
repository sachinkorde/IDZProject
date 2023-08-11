using UnityEngine;

public class PencilProperties : MonoBehaviour
{
    public PencilDirection pencilDirection;
    public Vector3 startPos;
    public PencilMovement pencilMovement;
    public GameObject parentOfPath;

    private void Start()
    {
        startPos = transform.localPosition;
        SetDirectin();
    }

    void SetDirectin()
    {
        switch (transform.name)
        {
            case "Up":
                pencilDirection = PencilDirection.Up;
                break;

            case "Right":
                pencilDirection = PencilDirection.Right;
                break;

            case "Left":
                pencilDirection = PencilDirection.Left;
                break;

            case "Down":
                pencilDirection = PencilDirection.Down;
                break;
        }
    }
}
