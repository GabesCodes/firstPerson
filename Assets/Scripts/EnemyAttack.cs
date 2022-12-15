using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;
    public Transform tankBulletSpawn;

    public GameObject tankBulletPrefab;

    [SerializeField]
    private LayerMask playerMask;

    [SerializeField]
    private EnemyData data;

    public Vector3 playerPosition;


    public float bulletSpeed;
    public float enemySpeed;
    public float fireRate;

    public float lookRadius;
    public float attackRadius;
    public float turnSmoothing;

    public bool canSee;
    public bool canShoot;
    public bool inAttackRange;

    float distanceFromPlayer;


    private void Start()
    {
        player = PlayerManager.instance.player.transform;
        enemySpeed = data.enemySpeed;
        lookRadius = data.lookRadius;
        turnSmoothing = 1.5f;
        inAttackRange = false;
        attackRadius = 55f;
        fireRate = 5f;
        canShoot = true;
        
    }

    private void Update()
    {
        SearchTarget();
        playerPosition = player.position;
        Debug.DrawLine(tankBulletSpawn.position, playerPosition);
    }

    private void SearchTarget()
    {
        distanceFromPlayer = Vector3.Distance(player.position, transform.position);


        if(distanceFromPlayer <= lookRadius)
        {
            FaceTarget();
            transform.position = Vector3.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
            canSee = true;
        }
        else
        {
            canSee = false;
        }

        if(distanceFromPlayer <= attackRadius)
        {
            inAttackRange = true;
            AttemptAttack();
        }
        else
        {
            inAttackRange = false;
        }
    }


    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSmoothing);
    }


    private void AttemptAttack()
    {
        if (inAttackRange && canShoot)
        {
            StartCoroutine(StartShoot());
        }
    }


    IEnumerator StartShoot()
    {
        Shoot();
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    void Shoot()
    {
        Debug.Log("shoot");
        GameObject tankShot = Instantiate(tankBulletPrefab, tankBulletSpawn.transform.position, tankBulletSpawn.rotation);
        tankShot.GetComponent<Rigidbody>().velocity = tankBulletSpawn.forward * bulletSpeed;
        Destroy(tankShot, 3f);
        //coolingdown = true;
    }

    
    

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }














}
