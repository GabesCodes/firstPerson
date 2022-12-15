using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private GunData data;

    public GameObject crossbow;




    //public Rigidbody arrow;

    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        crossbow = WeaponManager.instance.crossbow.gameObject;

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
            Debug.Log("hit enemy");
            Destroy(gameObject);
            if (collision.gameObject.CompareTag("Player"))
            {
                return;
            }
            else
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(crossbow.GetComponent<Crossbow>().arrowDamage);
                //seriously need to stop using GetComponent, like down the line this is going to fk you up on performance probably
                //from research, getcomponent searches A LIST EVERY TIME you call it to find what you're looking for

            }
        }

        if(collision.gameObject != null) //just reports what we're hitting if its not an enemy for debugging
        {
            Debug.Log("This is a " + collision.gameObject.name);
            Destroy(this.gameObject);
        }
        
    }
}
