using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] GameObject feet;
    [SerializeField] LayerMask planetLayer;
    [SerializeField] bool isGrounded;

    Rigidbody2D m_rigidbody;
    GravityObject gravityObject;

    [SerializeField] bool moving;
    [SerializeField] bool facingRight;
    bool jumpRequested;


    private void Awake()
    {
        m_rigidbody= GetComponent<Rigidbody2D>();
        gravityObject = GetComponent<GravityObject>();
    }

    private void Update()
    {
        if (GlobalEvents.PlayerPause.Invoked()) { return; }

        if (gravityObject.getCurrentAttractor() != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(feet.transform.position, (Vector2)gravityObject.getCurrentAttractor().planetTransform.position - m_rigidbody.position, 10, planetLayer);
            if (hit.collider != null && hit.distance <= 0.05f)
            {
                isGrounded = true;
            }
            else isGrounded = false;
        }

        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
        }
        

    }

    private void FixedUpdate()
    {
        if (GlobalEvents.PlayerPause.Invoked()) { return; }
        if (gravityObject.getCurrentAttractor() != null && isGrounded)
        {
            float x = Input.GetAxisRaw("Horizontal");

            // Calculate the direction from the player to the planet's center
            Vector2 toPlanetCenter = (Vector2)gravityObject.getCurrentAttractor().planetTransform.position - m_rigidbody.position;

            // Calculate the tangential direction for circular movement
            Vector2 tangentialDirection = Vector2.Perpendicular(toPlanetCenter).normalized;

            // Apply horizontal movement
            Vector2 movementVelocity = tangentialDirection * speed * x;

            // Preserve the vertical component of the velocity
            if(x > 0)
            {
                if(!moving)
                {
                    m_rigidbody.velocity += movementVelocity;
                    moving = true;
                }
                else
                {
                    //Currently Traveling in toward the left
                    if(!facingRight)
                    {
                        facingRight = true;
                        m_rigidbody.velocity = Vector2.zero;
                        m_rigidbody.velocity += movementVelocity;
                    }
                }
                
            }
            else if(x < 0)
            {
                if (!moving)
                {
                    m_rigidbody.velocity += movementVelocity;
                    moving = true;
                }
                //Currently Traveling in toward the right
                if (facingRight)
                {
                    facingRight = false;
                    m_rigidbody.velocity = Vector2.zero;
                    m_rigidbody.velocity += movementVelocity;
                }
            }
            else
            {
                m_rigidbody.velocity = Vector2.zero;
                moving = false;
            }


            if (jumpRequested)
            {
                Vector2 jumpDirection = -toPlanetCenter.normalized;
                m_rigidbody.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
                jumpRequested = false;
            }

        }
        else if(!isGrounded)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            m_rigidbody.AddForce(new Vector2(x, y).normalized * speed * 0.2f);
        }
    }
}
