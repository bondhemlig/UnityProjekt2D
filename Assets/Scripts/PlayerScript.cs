using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class PlayerState : MonoBehaviour
{
    public Transform leftFoot, rightFoot;
    public float rayDistance = 0.35f;
    private bool isGrounded = false; //fråga eller påstående
    private enum groundMaterials
    {
        Grass,
        Wood
    }
    private Enum currentGround;
    public LayerMask whatIsGround;

    public Transform respawnPosition;
    public int startingHealth = 100;
    public int currentHealth = 0;
    public Slider healthSlider;
    public Image healthFillColor;
    public Color GreenHealth;
    public Color RedHealth;

    public GameObject coinParticles, dustParticles;

    public float moveSpeed = 150f;
    public float jumpForce = 300f;

    public TMP_Text coinsText; 
    public int coinsCollected = 0;

    public float enemyMushRoomBounciness = 300f;

    private float horizontalValue = 0f;
    private bool canMove;

    private Rigidbody2D rigidBodyComponent;
    private SpriteRenderer playerRenderer;
    private Animator anim;

    public AudioClip jumpSound, coinPickupSound, singleFootstepSoundFoley, killRockSound, killMushroomSound, takeDamageSound, restoreHealth;
    private AudioSource audioSource;

    //Event for doing a cool effect!






    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        currentHealth = startingHealth;
        coinsText.text = "" + coinsCollected; //eller tostring() ?
        rigidBodyComponent = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");
        
        if (horizontalValue < 0) 
        {
            FlipSprite(true);
        }
        if (horizontalValue > 0)
        {
            FlipSprite(false);
        }

        //print("Vertical Value:" + Input.GetAxis("Vertical"));

        anim.SetFloat("MoveSpeed", Mathf.Abs(rigidBodyComponent.velocity.x));
        anim.SetFloat("VerticalSpeed", rigidBodyComponent.velocity.y);

        //Funkar utan!
        //anim.SetBool("IsGrounded", CheckIfGrounded());

        isGrounded = CheckIfGrounded();
        anim.SetBool("BefinnerSigSpelarenPåMarken", isGrounded);


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            DoJump();
        }

    }

    private void FixedUpdate ()
    {
        if (!canMove)
        {
            return;
        }

        rigidBodyComponent.velocity = new Vector2(horizontalValue * moveSpeed * Time.fixedDeltaTime, rigidBodyComponent.velocity.y);

    }

    //void -> no return value
    private void FlipSprite (bool direction)
    {
        playerRenderer.flipX = direction;
    }

    private void DoJump(float jumpBoost = 0)
    {
        rigidBodyComponent.AddForce(new Vector2(0, jumpForce + jumpBoost));
        audioSource.PlayOneShot(jumpSound, 0.5f);

        Instantiate(dustParticles, transform.position, dustParticles.transform.localRotation);
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

    private bool CheckIfGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayDistance, whatIsGround);

        //draw the raycast on the screen.
        Debug.DrawRay(leftFoot.position, Vector2.down * rayDistance, Color.red, 0.25f);
        Debug.DrawRay(rightFoot.position, Vector2.down * rayDistance, Color.black, 0.25f);

        if (leftHit.collider != null && HasGroundTag(leftHit) || (rightHit.collider != null && HasGroundTag(rightHit)))
        {
            //check ground material


            return true;
        }
        else
        {
            return false;
        }


    }


    private void OnEnable()
    {
        //subscribe to events
        EnemyMovement.JumpedOnMushroom += OnJumpedOnMushroom;
        Rock.jumpedOnARock += OnJumpOnRock;




    }

    private void OnDisable() //called whenever a object is disabled or destroyed in a scene
    {
        //unsubscribe from events (important)
        EnemyMovement.JumpedOnMushroom -= OnJumpedOnMushroom;
        Rock.jumpedOnARock -= OnJumpOnRock;
    }

    void OnJumpOnEnemy()
    {
        //Freeze the character against the gravity forces
        Vector2 playerRigidBodyVelocity = rigidBodyComponent.velocity;
        rigidBodyComponent.velocity = new Vector2(playerRigidBodyVelocity.x, 0);
        rigidBodyComponent.AddForce(new Vector2(0, enemyMushRoomBounciness));
        //DoJump(enemyMushRoomBounciness);
    }

    void OnJumpOnRock()
    {
        print("Player jumped on a rock");
        audioSource.PlayOneShot(killRockSound, 0.5f);
        OnJumpOnEnemy();

    }


    void OnJumpedOnMushroom()
    {
        print("Player jumped on a mushroom.");
        audioSource.PlayOneShot(killMushroomSound, 0.5f);
        OnJumpOnEnemy();
    }

    private void TakeKnockback(float KnockBackForce, float upForce)
    {
        if (KnockBackForce != 0) //
        {
            canMove = false;
            
        }
        rigidBodyComponent.AddForce(new Vector2(KnockBackForce, upForce));
        Invoke("CanMove", 0.25f);
    }

    private void CanMove()
    {
        canMove = true;
    }


    private void ShouldTakeKnockback(GameObject collidedGameObject)
    {
        GameObjectData gameObjectAttributes = collidedGameObject.GetComponent<GameObjectData>();
        if (gameObjectAttributes != null)
        {


            float knockBackForce, upForce;
            knockBackForce = gameObjectAttributes.horizontalKnockBackForce;
            upForce = gameObjectAttributes.verticalKnockbackForce;
            
            //gameObjectAttributes.horizontalKnockBackForce, gameObjectAttributes.verticalKnockbackForce;
            
            if (transform.position.x > collidedGameObject.transform.position.x) //Spelaren står på höger sida om det kolliderade spelobjektet
            {
                TakeKnockback(knockBackForce, upForce);

            }
            else //Spelaren står på vänster sida om det kolliderade spelobjektet
            {
                TakeKnockback(-knockBackForce, upForce);
            }

            //PushKnockback();


        }

    }

    private void RestoreHealth(GameObject HealthPickup)
    {
        if (currentHealth >= startingHealth)
        {
            return;
        }
        else
        {
            int healthToRestore = HealthPickup.GetComponent<HealthPickup>().healthAmount;
            audioSource.PlayOneShot(restoreHealth, 0.5f);
            Destroy(HealthPickup);
            currentHealth = Math.Clamp(currentHealth + healthToRestore, 0, startingHealth);
            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;
        if (healthSlider.value > startingHealth / 2)
        {
            healthFillColor.color = GreenHealth;
        }
        else
        {
            healthFillColor.color = RedHealth;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        audioSource.PlayOneShot(takeDamageSound, 0.5f);
        currentHealth = currentHealth - damageAmount;
        //print(CurrentHealth);
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            currentHealth = startingHealth;
            UpdateHealthBar();
            Respawn();
        }

    }



    private void Respawn()
    {
        transform.position = respawnPosition.position;
        rigidBodyComponent.velocity = Vector2.zero;

    }

    private void OnCollisionEnter2D(Collision2D collisionData)
    {
        if (collisionData.gameObject.CompareTag("Enemy"))
        {
            GameObject Enemy = collisionData.gameObject;

            int damage = 0;
            if (Enemy.GetComponent<EnemyMovement>())
            {
                damage = Enemy.GetComponent<EnemyMovement>().GetDamage();
            }
            if (Enemy.GetComponent<Rock>())
            {
                damage = Enemy.GetComponent<Rock>().GetDamage();
            }

            if (damage > 0)
            {
                TakeDamage(damage);
            }
        }

        ShouldTakeKnockback(collisionData.gameObject); //wheter there should be a knockback effect

    }


    public void Frame (string direction) //Object obj, float d
    {
        if (!isGrounded)
        {
            return;
        }
        
        if (direction == "Right")
        {
            audioSource.PlayOneShot(singleFootstepSoundFoley, 0.5f);
        }
        else //Left
        {
            audioSource.PlayOneShot(singleFootstepSoundFoley, 0.5f);

        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin")){
            Destroy(other.gameObject);
            Instantiate(coinParticles, other.transform.position, Quaternion.identity);
            coinsCollected++;
            coinsText.text = "" + coinsCollected;

            audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(coinPickupSound, 0.5f);



        }

        if (other.CompareTag("Health"))
        {
            RestoreHealth(other.gameObject);
        }

        ShouldTakeKnockback(other.gameObject);

    }


}
