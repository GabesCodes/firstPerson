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
        GunType(data.gunType);
    }

    void GunType(string gunType) //complete braindead monkey code B)
    {
        switch (data.gunType)
        {
            case "hitscan":

                void Hitscan()
                {
                    muzzleFlash.Play();
                    RaycastHit hit;

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
                    if (data.gunType == "Projectile")
                    {
                        Debug.Log("projectile");
                    }
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