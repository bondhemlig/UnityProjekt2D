using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public GameObject textPopUp;
    private void OnTriggerEnter2D(Collider2D otherCollision)
    {
        if (otherCollision.CompareTag("Player"))
        {
            textPopUp.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollision)
    {
        if (otherCollision.CompareTag("Player"))
        {
            textPopUp.SetActive(false);
        }
    }
     
}

