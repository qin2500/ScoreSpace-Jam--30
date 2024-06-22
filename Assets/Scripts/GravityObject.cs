using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    [SerializeField] private bool rotateToCenter = true;
    [SerializeField] GravityFeild currentAttractor;
    [SerializeField] LayerMask GravityFeildLayer;

    [SerializeField]private bool onSurface = false;

    Transform m_transform;
    Collider2D m_collider;
    Rigidbody2D m_rigidbody;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
        m_collider = GetComponent<Collider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (currentAttractor != null)
        {
            if (!currentAttractor.attractedObjects.Contains(m_collider)) currentAttractor = null;
            if (rotateToCenter) RotateToCenter();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        int collidedLayer = collision.gameObject.layer;
        if (((1 << collidedLayer) & GravityFeildLayer) != 0 )
        {
            onSurface = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        int collidedLayer = collision.gameObject.layer;
        if (((1 << collidedLayer) & GravityFeildLayer) != 0)
        {
            onSurface = false;
        }
    }
    

    public void Attract(GravityFeild attractor)
    {
        Vector2 attractionDir = (Vector2)attractor.planetTransform.position - m_rigidbody.position;
        float distance = attractionDir.magnitude;
        Vector2 normalizedAttractionDir = attractionDir.normalized;

        float gravitationalForce = -attractor.gravity * 100 * Time.fixedDeltaTime / distance;
        m_rigidbody.AddForce(normalizedAttractionDir * gravitationalForce);

        if (currentAttractor == null)
        {
            currentAttractor = attractor;
        }
        else
        {
            float prevDistance = ((Vector2)currentAttractor.planetTransform.position - m_rigidbody.position).magnitude;
            if(distance < prevDistance)
                currentAttractor = attractor;
        }

    }

    public GravityFeild getCurrentAttractor()
    {
        return currentAttractor;
    }

    void RotateToCenter()
    {
        Vector2 distanceVector = (Vector2)currentAttractor.planetTransform.position - (Vector2)m_transform.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        m_transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
}
