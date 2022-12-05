using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    public int damage = 1;
    //public Rigidbody arrow;

    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude >= 0.2f) //snipped
            transform.rotation = Quaternion.LookRotation(rb.velocity); //Make arrow rotation follow velocity(so it looks more like an arrow and curves upwards/downwards)
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Health>())
        {
            Debug.Log("foo");
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
        }
        Destroy(gameObject); //destroys arrow if it hits something that doesn't have health
    }
}
