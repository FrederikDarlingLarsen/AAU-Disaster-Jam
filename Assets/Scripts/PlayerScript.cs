using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Animator playerAnim;
    public float speed;

   private bool holdingItem;

    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

       transform.eulerAngles += new Vector3(0,horizontalInput*100,0);
    

   

    transform.localPosition += new Vector3(verticalInput,0,0)*speed*Time.deltaTime;
    
        if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Keypad0)) && !holdingItem){
            playerAnim.Play("pick up");
            GameObject.Find("RobotPickupTrigger").GetComponent<BoxCollider>().enabled=true;
        }
    }

    public void OnPickupAnimEnd(){
        GameObject.Find("RobotPickupTrigger").GetComponent<BoxCollider>().enabled=false;
    }

    public void setHolding(bool isHolding){
        holdingItem = isHolding;
        Debug.Log("Holding is now: " + holdingItem);
    }
}
