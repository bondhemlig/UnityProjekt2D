using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private bool Destroyed = false;
    public delegate void JumpedOnAnRock();
    public static event JumpedOnAnRock jumpedOnARock;
    //change this

    public Transform leftFoot, rightFoot, rightSide, leftSide;
    public float rayDistance = 0.25f;
    public LayerMask whatIsGround;
    public LayerMask whatIsCollidable;
    public float moveSpeed = 60f;
    private int playerDamage = 20;
    public ContactFilter2D contactFilter2D;

    private bool canMove = true;

    //private BoxCollider2D enemyCollider;
    private Rigidbody2D rigidBodyComponent;
    private SpriteRenderer enemySpriteRenderer;
    


    // Start is called before the first frame update
    void Start()
    {
        //enemyCollider = GetComponent<BoxCollider2D>();
        rigidBodyComponent = GetComponent<Rigidbody2D>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public int GetDamage()
    {
        return playerDamage;
    }

    private void checkDirection()
    {

        bool isRightGrounded = CheckIfRightLegIsGrounded();
        bool isLeftGrounded = CheckIsLeftLegIsGrounded();
        bool hitSomethingOnTheRight = CheckDidHitSomethingOnTheRightSide();
        bool hitSomethingOnTheLeft = CheckDidHitSomethingOnTheLeftSide();
        
        
        
        //print("Hit Something on the Right" + hitSomethingOnTheRight);
        //print("Hit something on the Left" + hitSomethingOnTheLeft);


        //One leg will become ungrounded before the other leg

        if (isRightGrounded == false)
        {
            //if the right one is ungrounded, then
            if (moveSpeed > 0) 
            {
                moveSpeed = -moveSpeed;
                enemySpriteRenderer.flipX = false;
            }

            return;
        }


        if (isLeftGrounded == false)
        {
            if (moveSpeed < 0)
            {
                moveSpeed = -moveSpeed;
                enemySpriteRenderer.flipX = true;
            }

            return;
        }


        if (hitSomethingOnTheRight == true)
        {
            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed;
                enemySpriteRenderer.flipX = false;
            }

            return;
        }

        if (hitSomethingOnTheLeft == true)
        {
            if (moveSpeed < 0)
            {
                moveSpeed = -moveSpeed;
                enemySpriteRenderer.flipX = true;
            }

            return;
        }

        return;
    }


    private void Update()
    {
        checkDirection();
        //CheckDidHitSomethingSideways();

        

    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

        rigidBodyComponent.velocity = new Vector2(moveSpeed * Time.deltaTime, rigidBodyComponent.velocity.y);
    }

    private bool HasGroundTag(RaycastHit2D hit)
    {
        bool result = hit.collider.CompareTag("Ground");
        if (result == false)
        {
            //print("Hit has ground tag:" + result);
        }
        return result;
    }

    private bool CheckIfRightLegIsGrounded()
    {
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayDistance, whatIsGround);
        Debug.DrawRay(rightFoot.position, Vector2.down * rayDistance, Color.black, 0.25f);
        if (rightHit.collider != null && HasGroundTag(rightHit))
        {
            return true;
        }
        return false;
    }

    private bool CheckIsLeftLegIsGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);
        Debug.DrawRay(leftFoot.position, Vector2.down * rayDistance, Color.red, 0.25f);
        if (leftHit.collider != null && HasGroundTag(leftHit))
        {
            return true;
        }


        return false;
    }

    private bool CheckDidHitSomethingOnTheLeftSide()
    {

        RaycastHit2D leftHit = Physics2D.Raycast(leftSide.position, Vector2.left, 0.25f, whatIsCollidable);

        //RaycastHit2D leftHit = Physics2D.BoxCast(transform.position - new Vector3(0, -0.5f, 0), collider.size, 0, Vector2.left);
        Debug.DrawRay(transform.position - new Vector3(0, -0.5f, 0), Vector2.left, Color.black, 0.25f);
        
        

        if (leftHit.collider != null)
        {
            if (leftHit.collider.CompareTag("Player"))
            {
                print("Found Player!");
                //leftHit.collider.GetComponent<PlayerState>().TakeDamage(GetDamage());
            }
            if (leftHit.collider.CompareTag("Enemy")) 
            {
                //print("Left side hit Enemy");
                return true;
            }
            if (leftHit.collider.CompareTag("Ground"))
            {
                //print("Left Side Hit Ground");
                return true;
            }


        }

        //print("Hit nothing on the left side");

        return false;

    }

    private bool CheckDidHitSomethingOnTheRightSide()
    {

        RaycastHit2D rightHit = Physics2D.Raycast(rightSide.position, Vector2.right, 0.25f, whatIsCollidable);
        //RaycastHit2D rightHit = Physics2D.BoxCast(transform.position - new Vector3(0, -0.5f, 0), new Vector2(0.5f, 0.4f), 0, Vector2.right);
        Debug.DrawRay(transform.position - new Vector3(0, -0.5f, 0), Vector2.right, Color.black, 0.25f);



        if (rightHit.collider != null)
        {
            if (rightHit.collider.CompareTag("Enemy") || HasGroundTag(rightHit))
            {
                return true;
            }
        }

        return false;
    }

    private bool compareTag(GameObject gameObject, string tag)
    {
        bool result = gameObject.CompareTag(tag);
        if (result == false)
        {
            //print(gameObject.name + "does not have" + tag);
        }
        else
        {
            //print(gameObject.name + " has " + tag);
        }

        return result;
    }

    private void OnTriggerEnter2D(Collider2D otherCollision)
    {
        if (compareTag(otherCollision.gameObject, "Player"))
        {
            if (Destroyed == false)
            {
                Destroyed = true;
                canMove = false;
                jumpedOnARock();
                GetComponent<Animator>().SetTrigger("Killed");
                GetComponent<BoxCollider2D>().enabled = false;
                rigidBodyComponent.gravityScale = 0;
                rigidBodyComponent.velocity = Vector2.zero;
                Destroy(gameObject, 0.5f);
            }

        }


    }

}
