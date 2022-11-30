using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private float enemySpeed;

    public float lookRadius = 10f;

    NavMeshAgent agent;
    Transform target;


    [SerializeField]
    private EnemyData data;


    // Start is called before the first frame update
    void Start()
    {
        SetEnemyValues();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
         ChaseAlways();
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void SetEnemyValues()
    {
        GetComponent<Health>().SetHealth(data.hp, data.hp);
        damage = data.damage;
        enemySpeed = data.enemySpeed;
    }

    private void ChaseAlways()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.speed = enemySpeed;
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                //attack

                FaceTarget();

            }
        }
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, enemySpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider) 
    {
        //if (collider.gameObject.CompareTag("Player"))
        {
            if (collider.GetComponent<Health>() != null)
            {
                collider.GetComponent<Health>().TakeDamage(damage);
                // this.GetComponent<Health>().Damage(5);
            }

        }
    }
}
