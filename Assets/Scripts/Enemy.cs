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
    public float enemySpeed;

    public float lookRadius;

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
        float enemyHealh = GetComponent<Health>().health;
        

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
        lookRadius = data.lookRadius;
    }

    private void ChaseAlways()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
           // agent.speed = enemySpeed;
            //agent.SetDestination(target.position);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, enemySpeed * Time.deltaTime);

            if (distance <= agent.stoppingDistance)
            {
                //attack

                FaceTarget();

            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("touching player");
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
   
        }

    }
}
