using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField]
    public float health;

    private int max_health = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetHealth(int maxHealth, int health)
    {
        this.max_health = maxHealth;
        this.health = health;
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("I am Dead!");
        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
