using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartScript : MonoBehaviour
{
    public enum BodyPartNames {rightLeg, leftLeg, rightArm, leftArm, torso, head};
    public BodyPartNames name;
    private StatsManagerScript stats;
    private bool collected = false;
    void Start()
    {
        stats = GameObject.Find("StatsManagerObject").GetComponent<StatsManagerScript>();
    }
    public void OnTriggerEnter(Collider other){

        if(other.gameObject.CompareTag("Slot")){

           if(other.gameObject.GetComponent<SlotScript>().name.ToString().Equals(name.ToString())){

          
              
              gameObject.transform.parent = other.gameObject.transform;
              
              gameObject.transform.position = other.gameObject.transform.position;
              gameObject.transform.rotation = Quaternion.identity;
              GameObject.Find("Player").GetComponent<PlayerScript>().setHolding(false);

             }
    }
        if(other.gameObject.CompareTag("PickupBox") && !collected){

        

            gameObject.transform.parent = other.gameObject.transform;

            gameObject.GetComponent<Rigidbody>().isKinematic=true;

            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), GameObject.Find("RobotPickupTrigger").GetComponent<Collider>(), true);

            gameObject.transform.localPosition = new Vector3 (0.0f, 0.3f, 0.0f);
            gameObject.transform.rotation = Quaternion.identity;

            stats.bodyPartCollected(name.ToString());

            collected = true;

            GameObject.Find("Player").GetComponent<PlayerScript>().setHolding(true);

           // stats.DisplayMessage(name.ToString() + " was picked up!", 1.5f);
         
            Debug.Log("item was collected: " + name.ToString());
        }

       
}}
