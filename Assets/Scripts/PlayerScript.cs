using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Animator playerAnim;
    public float speed;
    private bool holdingItem;

    public GameObject followTransform;

    public Vector2 _look;

    public float rotationPower = 3f;

    public GameObject wColor;
    public GameObject aColor;
    public GameObject sColor;
    public GameObject dColor;
    public GameObject eColor;

    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        // #region Player Based Rotation
        
        // //Move the player based on the X input on the controller
        // transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        // #endregion

        // #region Follow Transform Rotation

        // //Rotate the Follow Target transform based on the input
        // followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        // #endregion

        // #region Vertical Rotation
        // followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.forward);

        // var angles = followTransform.transform.localEulerAngles;
        // angles.z = 0;

        // var angle = followTransform.transform.localEulerAngles.x;

        // //Clamp the Up/Down rotation
        // if (angle > 180 && angle < 340)
        // {
        //     angles.x = 340;
        // }
        // else if(angle < 180 && angle > 40)
        // {
        //     angles.x = 40;
        // }


        // followTransform.transform.localEulerAngles = angles;
        // #endregion

        




       float horizontalInput = Input.GetAxis("Horizontal");

       float verticalInput = Input.GetAxis("Vertical");

       transform.eulerAngles += new Vector3(0,horizontalInput*100,0)*Time.deltaTime;
    
       transform.position += transform.right * verticalInput*speed*Time.deltaTime;
    
        if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Keypad0)) && !holdingItem){
           // playerAnim.Play("pick up");
           playerAnim.Play("addtheBRAIN");
             eColor.SetActive(true);



StartCoroutine(PickupStuff());
           
        }

         if(horizontalInput > 0){
            dColor.SetActive(true);
         }
         if(horizontalInput < 0){
            aColor.SetActive(true);
         }
         if(verticalInput > 0){
            wColor.SetActive(true);
         }
         if(verticalInput < 0){
            sColor.SetActive(true);
         }
         if(horizontalInput == 0){
            aColor.SetActive(false);
            dColor.SetActive(false);
         }
         if(verticalInput == 0){
            sColor.SetActive(false);
            wColor.SetActive(false);
         }

         //Set the player rotation based on the look transform
      //  transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
        //reset the y rotation of the look transform
       // followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

    public void OnPickupAnimEnd(){
        GameObject.Find("RobotPickupTrigger").GetComponent<BoxCollider>().enabled=false;
    }

    public void setHolding(bool isHolding){
        holdingItem = isHolding;
        Debug.Log("Holding is now: " + holdingItem);
    }

    public bool getHolding(){
        return holdingItem;
    }

    public IEnumerator PickupStuff(){
        yield return new WaitForSeconds(0.9f);
      GameObject.Find("RobotPickupTrigger").GetComponent<BoxCollider>().enabled=true;
           eColor.SetActive(false);
    }
}