using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static groundCheck;


public class GUI : MonoBehaviour
{
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI isGrounded;
    
    public TextMeshProUGUI ammoDisplay;
    private string slash = "/";

    public GameObject player;
    Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player.gameObject;
        gun = player.GetComponent<Gun>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ammoDisplay.text = gun.currentAmmoInClip.ToString() + slash + gun.ammoInReserve.ToString();
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
