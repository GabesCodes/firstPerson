using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tankBullet : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject tankBulletExplosion;

    public float explosionRadius;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude >= 0.2f) //snipped
            transform.rotation = Quaternion.LookRotation(rb.velocity); //Make arrow rotation follow velocity(so it looks more like an arrow and curves upwards/downwards)
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject != null)
        {
            GameObject explosion =  Instantiate(tankBulletExplosion, transform.position, Quaternion.identity);
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(1);
            }

            Debug.Log("explosion");
            Destroy(explosion, 3f);
            Destroy(gameObject);

        }
    }
}
