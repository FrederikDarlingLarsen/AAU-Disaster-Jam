using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Animator playerAnim;
    public float speed;
    private bool holdingItem;
   //  public GameObject followTransform;
   //  public Vector2 _look;
   //  public float rotationPower = 3f;
    public GameObject wColor;
    public GameObject aColor;
    public GameObject sColor;
    public GameObject dColor;
    public GameObject eColor;
    public GameObject skum;
    private bool isShooting;
    private float timer;

    Animator elevatorAnim;
    Material elevatorButton;
    Collider elevatorCollider;


    StatsManagerScript stats;


    public GameObject otherBrain;


    bool hasFireEstinguish  = false;

    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();

        elevatorAnim = GameObject.Find("Elevator").GetComponent<Animator>();

        elevatorCollider = GameObject.Find("Elevator").GetComponent<Collider>();

        elevatorButton = GameObject.Find("ElevatorButton").GetComponent<Renderer>().materials[1];

        stats = GameObject.Find("StatsManagerObject").GetComponent<StatsManagerScript>();
    }

    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Escape)){
         Application.Quit();

      }
       float horizontalInput = Input.GetAxis("Horizontal");
       float verticalInput = Input.GetAxis("Vertical");
       transform.eulerAngles += new Vector3(0,horizontalInput*100,0)*Time.deltaTime;
       transform.position += transform.right * verticalInput*speed*Time.deltaTime;

       if(isShooting){
         timer += Time.deltaTime;
         if(timer > 0.001f){
         GameObject obj = skum;
         obj.transform.parent = transform;
         float randomY = Random.Range(0.4f,0.6f);
         float randomZ = Random.Range(-0.35f,-0.15f);
         var objPos = new Vector3(transform.position.x+randomZ,transform.position.y+randomY,transform.position.z+0.5f);
         Instantiate(obj, objPos, transform.rotation);
         timer= 0;
         }
       }

       if(Input.GetKeyDown(KeyCode.Q) &&  hasFireEstinguish){
         isShooting=true;
       }
       if(Input.GetKeyUp(KeyCode.Q)){
         isShooting=false;
       }

        if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Keypad0)) && !holdingItem && !stats.allPartsCollected){
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
    public void OnTriggerStay(Collider other){
      if(other.gameObject.CompareTag("BrainSpot")){
         if(Input.GetKeyDown(KeyCode.E)){
            StartCoroutine(AddBrain());
         }
      }
      if(other.gameObject.CompareTag("Elevator")){
         if(Input.GetKeyDown(KeyCode.R)){
            elevatorAnim.Play("elevator");
         Color emisColor = Color.green * 400000.0f;    
         elevatorButton.SetColor("_EmissiveColor", emisColor);
         }
      }
    }
    public void OnTriggerEnter(Collider other){
      if(other.gameObject.CompareTag("FireEs")){
         other.gameObject.transform.parent = transform;
         other.gameObject.transform.localPosition = new Vector3(0.2f, 0.6f, 0.72f);
         other.gameObject.transform.localEulerAngles = new Vector3(-90, 90, 0);
         hasFireEstinguish = true;

         stats.DisplayMessage("You got the fire estinguisher! Use it by pressing Q!", 1.5f);
    }
    }

    public IEnumerator AddBrain(){
      playerAnim.Play("addtheBRAIN");

      yield return new WaitForSeconds(1.2f);

      otherBrain.SetActive(true);

      StartCoroutine(stats.EndCutscene());
      
            //elevatorCollider.enabled = true;
    
}
}