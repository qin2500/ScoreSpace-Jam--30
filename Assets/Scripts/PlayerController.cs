using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] GameObject feet;
    [SerializeField] LayerMask planetLayer;
    [SerializeField] Animator animator;
    [SerializeField] GameObject playerSprite;
    [SerializeField] GameObject death;
    [SerializeField] float freeMoveLimit;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip landedSound;

    public UnityEvent onDeath;
    bool isGrounded;
    bool playedSound = false;
    public bool isMoving = false;
    public bool freeMove = true;

    Rigidbody2D m_rigidbody;
    GravityObject gravityObject;

    bool facingRight = true;
    bool jumpRequested;

    private Vector2 paused_velocity = Vector2.zero;


    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        gravityObject = GetComponent<GravityObject>();
        animator = GetComponent<Animator>();

        transform.position = GlobalReferences.PLAYER.startPosition;
    }

    private void Update()
    {
        if (GlobalEvents.PlayerPause.Invoked()) { return; }

        if (gravityObject.getCurrentAttractor() != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(feet.transform.position, (Vector2)gravityObject.getCurrentAttractor().planetTransform.position - m_rigidbody.position, 10, planetLayer);
            if (hit.collider != null && hit.distance <= 0.05f )
            {
                if(playedSound == false){
                    SoundFXManager.instance.PlaySoundFXClip(landedSound, transform, 1f);
                    playedSound = true;
                }
                
                isGrounded = true;
            }
            else isGrounded = false;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
        }

        if ((gravityObject.getCurrentAttractor() == null)) {
            freeMove = false;
            return;
        }

        float distance = ((Vector2)gravityObject.getCurrentAttractor().planetTransform.position - m_rigidbody.position).magnitude;
        if (distance > freeMoveLimit)
        {
            freeMove = false;
        }
        else freeMove = true;
    }

    private void FixedUpdate()
    {
        if (GlobalEvents.PlayerPause.Invoked()) 
        {
            if (paused_velocity == Vector2.zero) paused_velocity = m_rigidbody.velocity;
            m_rigidbody.velocity = Vector2.zero;

            return; 
        }
        if (paused_velocity != Vector2.zero)
        {
            m_rigidbody.velocity = paused_velocity;
            paused_velocity = Vector2.zero;
        }
        if (gravityObject.getCurrentAttractor() != null && isGrounded)
        {
            float x = Input.GetAxisRaw("Horizontal");

            // Calculate the direction from the player to the planet's center
            Vector2 toPlanetCenter = (Vector2)gravityObject.getCurrentAttractor().planetTransform.position - m_rigidbody.position;

            // Calculate the tangential direction for circular movement
            Vector2 tangentialDirection = Vector2.Perpendicular(toPlanetCenter).normalized;

            // Apply horizontal movement
            Vector2 movementVelocity = tangentialDirection * speed * x;

            if (x != 0)
            {
                if (x > 0 && !facingRight) flip();
                else if (x < 0 && facingRight) flip();
                if(!isMoving)
                {
                    m_rigidbody.velocity = Vector2.zero;
                    m_rigidbody.velocity += movementVelocity;
                    isMoving= true;
                }
                animator.Play("walk");
                
                GlobalEvents.PlayerStartedMoving.invoke();
            }
            else
            {
                m_rigidbody.velocity = Vector2.zero;
                isMoving = false;
                animator.Play("Idle");
            }


            if (jumpRequested)
            {
                Vector2 jumpDirection = -toPlanetCenter.normalized;
                m_rigidbody.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
                jumpRequested = false;
                playedSound = false;
                animator.Play("jump");
                SoundFXManager.instance.PlaySoundFXClip(jumpSound, transform, 1f);
                GlobalEvents.PlayerStartedMoving.invoke();

            }

        }
        else if (!isGrounded)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            if(freeMove)m_rigidbody.AddForce(new Vector2(x, y).normalized * speed * 0.5f);

            isMoving = false;
        }

  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Death Zone"))
        {
            this.gameObject.SetActive(false);
            Instantiate(death, transform.position, transform.rotation);
            if(onDeath != null)
            {    
                SoundFXManager.instance.PlaySoundFXClip(deathSound, transform, 1f);

                Invoke("incokeDeath", 1.0f);
            }
        }
    }
    private void flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = playerSprite.transform.localScale;
        scaler.x *= -1;
        playerSprite.transform.localScale = scaler;

    }

    void incokeDeath()
    {
        onDeath.Invoke();
    }

    public void shineKudasai()
    {
        GlobalEvents.PlayerDeath.invoke();
    }
}

    
