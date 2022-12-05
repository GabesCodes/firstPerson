using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GUI : MonoBehaviour
{
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI isGrounded;
    
    public TextMeshProUGUI bulletDisplay;
    public TextMeshProUGUI arrowDisplay;

    private string slash = "/";

    public GameObject player;
    public GameObject crossbow;
    public GameObject pistol;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player.gameObject;
        crossbow = WeaponManager.instance.crossbow.gameObject;
        pistol = WeaponManager.instance.pistol.gameObject;

        //gun = player.GetComponent<Gun>();
        //crossbow = GetComponent<Crossbow>();
        //arrowDisplay.text = crossbow.currentArrowAmmo.ToString();
        


    }

    // Update is called once per frame
    void Update()
    {
        arrowDisplay.text = crossbow.GetComponent<Crossbow>().currentArrowAmmo.ToString() + slash + crossbow.GetComponent<Crossbow>().reserveArrowAmmo.ToString();
        bulletDisplay.text = pistol.GetComponent<Gun>().currentAmmoInClip.ToString() + slash + pistol.GetComponent<Gun>().ammoInReserve.ToString();
        healthDisplay.text = "Current health: " + player.GetComponent<Health>().health.ToString();
        //{
          //  if(player.GetComponent<Health>().health <= 0)
            //{
              //  healthDisplay.text = "You are dead! X.X";
            //}
       // }

        //isGrounded.text = "Is Grounded: " + isGroundedInt.ToString();
        
    }
}
