using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [SerializeField]
    public bool canShoot;
    public int currentAmmoInClip;
    public int ammoInReserve;

    [SerializeField]
    private GunData data;

    public ParticleSystem muzzleFlash;

    public float thrust = 1.0f;

    //public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmoInClip = data.clipSize;
        ammoInReserve = data.reservedAmmoCapacity;
        canShoot = true;
        GunType(data.gunType);
        //compare current gunType against our gunDATA class string
    }

    // Update is called once per frame
    private void Update()
    {
         

    }

    void GunType(string gunType) //complete braindead monkey code B), idk if this is genius or stupid but im proud of it!
    {
        switch (data.gunType) 
        {
            case "hitscan": //shoots hitscan using raycasts if gunData gunType string is "hitscan". 

                void Hitscan()
                {
                    muzzleFlash.Play();
                    RaycastHit hit;

                    //shoot a raycast, if we hit something return true, if true, get whatever object it hit and store info to target var, if it has health, dmg it 

                    if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, data.range))
                    {
                        Debug.Log(hit.transform.name);
                        Health target = hit.transform.GetComponent<Health>();
                        if (target != null) //  only do this if component is found, target is not null (nothing)
                        {
                            target.TakeDamage(damage: data.damage);
                        }
                    }

                }

                //still struggling on learning ienumerators, but its for repeating things.
                IEnumerator ShootHitscanGun()
                {
                    Hitscan();
                    yield return new WaitForSeconds(data.fireRate);
                    canShoot = true;
                }

                if (Input.GetMouseButtonDown(0) && canShoot && currentAmmoInClip > 0)
                {
                    canShoot = false;
                    currentAmmoInClip--;
                    StartCoroutine(ShootHitscanGun());
                }

                else if (Input.GetKeyDown(KeyCode.R) && currentAmmoInClip < data.clipSize && ammoInReserve > 0)
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

            break;



            case "projectile":

                break;
        }
    }
}