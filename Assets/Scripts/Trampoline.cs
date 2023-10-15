using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Trampoline : MonoBehaviour
{

    private float jumpForce = 700f;
    
    private Vector3 startPos;
    private Vector3 jumpPos;

    private Animator anim;
    private void Start()
    {
        //startPos = transform.position;
        //jumpPos = new Vector3(startPos.x - 0.49615f, startPos.y, 0);

        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            Rigidbody2D character = collision.GetComponent<Rigidbody2D>();
            character.velocity = new Vector2(character.velocity.x, 0);
            character.AddForce(new Vector2(0, jumpForce));
           
            
            anim.SetTrigger("Jump");
           

        }
    }

    //private void Update()
    //{
    //    AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
    //    if (currentState.IsTag("Idle"))
    //    {
            
    //        transform.position = startPos;
    //    }else
    //    {
    //        Debug.Log("IsJumping");
    //        transform.position = jumpPos;
    //    }
    //}

}
