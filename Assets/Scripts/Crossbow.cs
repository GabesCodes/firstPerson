using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform ArrowSpawn;
    public float arrowSpeed;

    public int arrowDamage;
    public int currentArrowAmmo;
    public int reserveArrowAmmo;

    [SerializeField]
    private bool canShoot;
    

    [SerializeField]
    private GunData data;


    // Start is called before the first frame update
    void Start()
    {
        arrowSpeed = data.range;
        arrowDamage = data.damage;
        currentArrowAmmo = data.clipSize;
        reserveArrowAmmo = data.reservedAmmoCapacity;
        canShoot = true;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && currentArrowAmmo > 0)
        {
            canShoot = false;
            currentArrowAmmo--;
            StartCoroutine(ShootArrowIE());

        }

        if (Input.GetKeyDown(KeyCode.R) && currentArrowAmmo < data.clipSize && reserveArrowAmmo > 0)
        {
            int amountNeeded = data.clipSize - currentArrowAmmo; //calculate how much to take from reserves

            if (amountNeeded >= reserveArrowAmmo)
            {
                currentArrowAmmo += reserveArrowAmmo;
                reserveArrowAmmo -= amountNeeded;
            }
            else
            {
                currentArrowAmmo = data.clipSize;
                reserveArrowAmmo -= amountNeeded; //math is hard
            }

        }
    }
   
    IEnumerator ShootArrowIE()
    {
        ShootArrow();
        yield return new WaitForSeconds(data.fireRate);
        canShoot = true;
    }
    void ShootArrow()
    {
        currentArrowAmmo--;
        GameObject arrowObj = Instantiate(arrowPrefab, ArrowSpawn.transform.position, Camera.main.transform.rotation);
        arrowObj.GetComponent<Rigidbody>().velocity = ArrowSpawn.forward * arrowSpeed;
        arrowObj.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, arrowSpeed), ForceMode.Force);
        Destroy(arrowObj, 3f);
    }
  
}
