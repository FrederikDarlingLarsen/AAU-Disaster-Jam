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
        float horizontalInput = Input.GetAxis("Vertical");

        float verticalInput = Input.GetAxis("Horizontal");

        Vector3 moveDirection = new Vector3(verticalInput, 0, horizontalInput);

        if (moveDirection.sqrMagnitude > 0.001f)
    {
        var desiredRotation = Quaternion.LookRotation(moveDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 10);
    }

    var direction = new Vector3(horizontalInput, 0, -verticalInput);

    direction.Normalize();

    transform.position += direction*speed*Time.deltaTime;
    
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
