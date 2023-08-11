using UnityEngine;
using UnityEngine.UI;

public class PlayerEnterOnLine : MonoBehaviour
{
    public PencilMovement pencilMovement;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            gameObject.GetComponent<Image>().enabled = true;

            Debug.Log("Reaced on Lines");
        }
    }
}

