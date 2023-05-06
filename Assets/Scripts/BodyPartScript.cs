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


        if(collected && other.gameObject.CompareTag("Slot")){

              if(other.gameObject.GetComponent<SlotScript>().name.Equals(name)){
            Debug.Log("correct slot!!!");

        gameObject.transform.parent = other.gameObject.transform;

        gameObject.transform.position = other.gameObject.transform.position;

    }else{
        Debug.Log("error");
    }
    }
        

    //      if(other.gameObject.CompareTag("PickupBox") && collected){
    //         GameObject.Find("Player").GetComponent<PlayerScript>().setHolding(false);
    //         gameObject.GetComponent<Rigidbody>().isKinematic=false;
    //         collected = false;
    // }

        if(other.gameObject.CompareTag("PickupBox") && !collected){
Debug.Log("hey");

            gameObject.transform.parent = other.gameObject.transform;

            gameObject.GetComponent<Rigidbody>().isKinematic=true;

            gameObject.transform.localPosition = new Vector3 (0.0f, 0.5f, 0.0f);

         stats.bodyPartCollected(name.ToString());

         collected = true;

         GameObject.Find("Player").GetComponent<PlayerScript>().setHolding(true);

         stats.DisplayMessage(name.ToString() + " was picked up!", 1.5f);
         
         Debug.Log("item was collected: " + name.ToString());
        }

       
}}
