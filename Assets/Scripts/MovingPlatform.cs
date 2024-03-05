using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class MovingPlatform : MonoBehaviour
{
    public Transform starting_moveTowardsTarget, finishMoveTowardsTargetStart;
    private Vector3 StartTargetStart, FinishTargetFinish;
    public float moveSpeed = 10f; 
    private Vector3 CurrentTargetVector3;

    // Start is called before the first frame update
    void Start()
    {
        StartTargetStart = starting_moveTowardsTarget.position;
        FinishTargetFinish = finishMoveTowardsTargetStart.position;

        //print(StartTargetStart);
        //print(FinishTargetFinish);
        CurrentTargetVector3 = StartTargetStart;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position == StartTargetStart)
        {
            CurrentTargetVector3 = FinishTargetFinish;
        }

        if (transform.position == FinishTargetFinish)
        {
            CurrentTargetVector3 = StartTargetStart;
        }


        transform.position = Vector2.MoveTowards(transform.position, CurrentTargetVector3, moveSpeed * Time.fixedDeltaTime);

    }

    private void OnCollisionEnter2D(Collision2D otherCollision)
    {
        if (otherCollision.gameObject.CompareTag("Player") && otherCollision.transform.position.y > transform.position.y)
        {
            otherCollision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D otherCollision)
    {
        if (otherCollision.gameObject.CompareTag("Player"))
        {
            otherCollision.transform.SetParent(null);
        }
    }
}