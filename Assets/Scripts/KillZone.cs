using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public Transform respawnPosition;

    private void OnTriggerEnter2D(Collider2D otherObjectCollidedWith)
    {
        //När ett objekt träffar volymen körs denna kod.
        if (otherObjectCollidedWith.CompareTag("Player"))
        {
            otherObjectCollidedWith.transform.position = respawnPosition.position;




            otherObjectCollidedWith.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        }


    }
}
