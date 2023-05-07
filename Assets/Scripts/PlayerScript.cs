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
    public GameObject skum;
    private bool isShooting;
    private float timer;

    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
       float horizontalInput = Input.GetAxis("Horizontal");
       float verticalInput = Input.GetAxis("Vertical");
       transform.eulerAngles += new Vector3(0,horizontalInput*100,0)*Time.deltaTime;
       transform.position += transform.right * verticalInput*speed*Time.deltaTime;

       if(isShooting){
         timer += Time.deltaTime;
         if(timer > 0.001f){
         GameObject obj = skum;
         obj.transform.parent = transform;
         float randomY = Random.Range(0.1f,0.3f);
         float randomZ = Random.Range(-0.1f,0.1f);
         var objPos = new Vector3(transform.position.x+randomZ,transform.position.y+randomY,transform.position.z);
         Instantiate(obj, objPos, transform.rotation);
         timer= 0;
         }
       }

       if(Input.GetKeyDown(KeyCode.Q)){
         isShooting=true;
       }
       if(Input.GetKeyUp(KeyCode.Q)){
         isShooting=false;
       }


        if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Keypad0)) && !holdingItem){
            playerAnim.Play("pick up");
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