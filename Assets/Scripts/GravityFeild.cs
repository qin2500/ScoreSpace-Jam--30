using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravityFeild : MonoBehaviour
{
    public LayerMask attractionLayer;
    public float gravity = -10;
    [SerializeField] private float effectionRadius = 10;
    public GameObject gravityFieldVisualizer;
    [HideInInspector] public Transform planetTransform;

    public List<Collider2D> attractedObjects = new List<Collider2D>();

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, effectionRadius);
    }

    private void Awake()
    {
        gravityFieldVisualizer.transform.localScale = new Vector3(effectionRadius * 2, effectionRadius *2, gravityFieldVisualizer.transform.localScale.z);

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
