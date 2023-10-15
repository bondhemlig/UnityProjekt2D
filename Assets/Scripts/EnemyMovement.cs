using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

//raycasts, layers:
//Vilka Objekt som kolliderar med vilka lager



public class EnemyMovement : MonoBehaviour
{
    [Header("How fast the enemy moves")]
    public float moveSpeed = 1f;
    public int playerDamage = 1;



    private bool Destroyed = false;

    public delegate void mushroomKillAction();
    public static event mushroomKillAction JumpedOnMushroom;

    private SpriteRenderer enemyRenderer;
    
    private void Start()
    {
        enemyRenderer = GetComponent<SpriteRenderer>();
    }
    public int GetDamage()
    {
        return playerDamage;
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector2 (moveSpeed, 0) * Time.deltaTime);



        if (moveSpeed > 0)
        {
            enemyRenderer.flipX = true;
        } 

        if (moveSpeed < 0) 
        {
            enemyRenderer.flipX = false;
        }
        
        

    }

    private bool compareTag (GameObject gameObject, string tag)
    {
        bool result = gameObject.CompareTag(tag);
        if (result == false) 
        {
            //print(gameObject.name + "does not have" + tag);
        }else
        {
            //print(gameObject.name + " has " + tag);
        }

        return result;
    }

    private void OnCollisionEnter2D(Collision2D collisionData)
    {
        GameObject collidedGameObject = collisionData.gameObject;
        
       if (compareTag(collidedGameObject, "EnemyCollider"))
        {
            ToggleMovementDirection();
        }

       if (compareTag(collidedGameObject, "Enemy"))
        {
            ToggleMovementDirection();
        }

       if (collidedGameObject.CompareTag("Player"))
        {
            //collidedGameObject.GetComponent<>
        }

    }

    private void ToggleMovementDirection()
    {
        moveSpeed = -moveSpeed;
        

    }



    private void OnTriggerEnter2D(Collider2D otherCollision)
    {
        //print("collided with otherCollision:" + otherCollision);
        if (compareTag(otherCollision.gameObject, "Player"))
        {
            if (Destroyed == false)
            {
                Destroyed = true;
                JumpedOnMushroom(); //Fire the event
            }

            Destroy(gameObject);
        }

        if (compareTag(otherCollision.gameObject, "EnemyCollider"))
        {
            ToggleMovementDirection();
        }


    }

}
