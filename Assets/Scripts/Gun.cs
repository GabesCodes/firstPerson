using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour //general class for a hitscan gun.
{
    [SerializeField]
    public bool canShoot;
    public int currentAmmoInClip;
    public int ammoInReserve;

    [SerializeField]
    private GunData data;

    [SerializeField] 
    private LayerMask enemy;

    public Vector3 defaultPosition;
    public Vector3 aimingPosition;

    public float aimSmoothing;

    public ParticleSystem muzzleFlash;

    public float thrust = 1.0f;
   

    //public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {

        currentAmmoInClip = data.clipSize;
        ammoInReserve = data.reservedAmmoCapacity;
        canShoot = true;
    }

    // Update is called once per frame
    private void Update()
    {

        //initiate shoot 
        if (Input.GetMouseButton(0) && canShoot && currentAmmoInClip > 0)
        {
            canShoot = false;
            currentAmmoInClip--;
            StartCoroutine(ShootHitscanGun());
        }

       
            AimDownSights();
        

        //reload code
        if (Input.GetKeyDown(KeyCode.R) && currentAmmoInClip < data.clipSize && ammoInReserve > 0)
        {
            int amountNeeded = data.clipSize - currentAmmoInClip; //calculate how much to take from reserves

            if (amountNeeded >= ammoInReserve)
            {
                currentAmmoInClip += ammoInReserve;
                ammoInReserve -= amountNeeded;
            }
            else
            {
                currentAmmoInClip = data.clipSize;
                ammoInReserve -= amountNeeded; //math is hard
            }

        }

        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            canShoot = true;
            Debug.Log("gun jam fixed");
        }
    }

    void Hitscan()
    {
        muzzleFlash.Play();
        RaycastHit hit;

        //shoot a raycast, if we hit something return true, if true, get whatever object it hit and store info to target var, if it has health, dmg it 
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, data.range, enemy))
        {
            Health target = hit.transform.GetComponent<Health>();
            if (target != null) //  only do this if component is found, target is not null (nothing)
            {
                target.TakeDamage(damage: data.damage);
            }
            Debug.DrawLine(hit.point, hit.normal);
        }
        

    }
    IEnumerator ShootHitscanGun()
    {
        Hitscan();
        yield return new WaitForSeconds(data.fireRate);
        canShoot = true;
    }

    void AimDownSights()
    {
        Vector3 target = defaultPosition;
        if (Input.GetMouseButton(1))
        
            target = aimingPosition;

            Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);
            transform.localPosition = desiredPosition;
       
    }
}