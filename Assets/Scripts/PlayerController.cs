using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] GameObject feet;
    [SerializeField] LayerMask planetLayer;
    [SerializeField] Animator animator;
    [SerializeField] GameObject playerSprite;
    bool isGrounded;

    Rigidbody2D m_rigidbody;
    GravityObject gravityObject;

    bool facingRight = true;
    bool jumpRequested;


    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        gravityObject = GetComponent<GravityObject>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (gravityObject.getCurrentAttractor() != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(feet.transform.position, (Vector2)gravityObject.getCurrentAttractor().planetTransform.position - m_rigidbody.position, 10, planetLayer);
            if (hit.collider != null && hit.distance <= 0.05f)
            {
                isGrounded = true;
            }
            else isGrounded = false;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
        }


    }

    private void FixedUpdate()
    {
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
                m_rigidbody.velocity = Vector2.zero;
                m_rigidbody.velocity += movementVelocity;
                animator.Play("walk");
            }
            else
            {
                m_rigidbody.velocity = Vector2.zero;
                animator.Play("Idle");
            }


            if (jumpRequested)
            {
                Vector2 jumpDirection = -toPlanetCenter.normalized;
                m_rigidbody.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
                jumpRequested = false;
                animator.Play("jump");
            }

        }
        else if (!isGrounded)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            m_rigidbody.AddForce(new Vector2(x, y).normalized * speed * 0.2f);
        }
    }
    private void flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = playerSprite.transform.localScale;
        scaler.x *= -1;
        playerSprite.transform.localScale = scaler;

    }
}

    
