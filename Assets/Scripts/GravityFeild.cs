using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravityFeild : MonoBehaviour
{
    public LayerMask attractionLayer;
    public float gravity = -10;
    [SerializeField] private float effectionRadius = 10;
    [HideInInspector] public Transform planetTransform;

    public List<Collider2D> attractedObjects = new List<Collider2D>();

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, effectionRadius);
    }

    private void Awake()
    {
        planetTransform = GetComponent<Transform>();
    }
    public void Update()
    {
        attractedObjects = Physics2D.OverlapCircleAll(planetTransform.position, effectionRadius, attractionLayer).ToList();
    }

    private void FixedUpdate()
    {
        for(int i=0; i<attractedObjects.Count; i++)
        {
            attractedObjects[i].GetComponent<GravityObject>().Attract(this);
        }
    }
}
