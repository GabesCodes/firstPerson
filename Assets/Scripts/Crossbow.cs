using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform ArrowSpawn;
    public float arrowSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShootArrow();

    }

    public void ShootArrow()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject arrowObj = Instantiate(arrowPrefab, ArrowSpawn.transform.position, Camera.main.transform.rotation);
            arrowObj.GetComponent<Rigidbody>().velocity = ArrowSpawn.forward * arrowSpeed;
            arrowObj.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, arrowSpeed), ForceMode.Force);
            Destroy(arrowObj, 3f);
        }

    }
  
}
