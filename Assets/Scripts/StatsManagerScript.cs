using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsManagerScript : MonoBehaviour
{
    float health = 101.0f;
    public TextMeshProUGUI healthText;
    public float healthDecreaseSpeed;
    public IDictionary<string, bool> bodyParts = new Dictionary<string, bool>();
    public GameObject UIMessage;

    private Transform batteryLifeTrans;

    private Renderer healthRenderer;

    public bool allPartsCollected = false;

    private GameObject brainSpot;

    private PlayerScript player;
    public GameObject mainCam;

    public GameObject otherCam;

    public GameObject fadeIn;

    public GameObject fadeToBlack;

    public GameObject nathan;

    bool done = false;

    void Start()
    {
        bodyParts.Add("leftArm", false);
        bodyParts.Add("rightArm", false);
        bodyParts.Add("leftLeg", false);
        bodyParts.Add("rightLeg", false);
        bodyParts.Add("torso", false);
        bodyPartCollected("leftArm");
           foreach (KeyValuePair<string, bool> kvp in bodyParts)
      {
         Debug.Log("Key = " + kvp.Key + "Value =" + kvp.Value);
     }

     batteryLifeTrans = GameObject.Find("BatteryLife").GetComponent<Transform>();
     healthRenderer = GameObject.Find("BatteryLife").GetComponent<Renderer>();

     brainSpot = GameObject.Find("Brain Spot");

     player = GameObject.Find("Player").GetComponent<PlayerScript>();


     //StartCoroutine(EndCutscene());

     
    }
    void Update()
    {
        health -= healthDecreaseSpeed * Time.deltaTime;

        batteryLifeTrans.localScale = new Vector3 (1,1,health/100); 


        var colorVector = new Vector3((100/health)/10.0f,health/200.0f,0);

        colorVector.Normalize();

        Color emisColor = new Color(colorVector.x,colorVector.y, 0) * 400000.0f;    

         healthRenderer.material.SetColor("_EmissiveColor", emisColor);


        healthText.text = "Battery: " + (int)health + "%";

      if(health < 0){
        
        DisplayMessage("Game Over",2);
        Time.timeScale = 0;
      }


      if(allPartsCollected && !player.getHolding() && !done){
        brainSpot.GetComponent<Renderer>().enabled=true;
        brainSpot.GetComponent<Collider>().enabled=true;
        DisplayMessage("You can now insert the brain!", 1.5f);
        done=true;
      }
    }

    public void bodyPartCollected(string bodyPart){
        if(bodyParts.ContainsKey(bodyPart)){
            bodyParts[bodyPart] = true;
        }
            bool temp = true;
         foreach (KeyValuePair<string, bool> kvp in bodyParts)
      {
         if(kvp.Value.Equals(false)){
            temp=false;
         }
     }
     allPartsCollected = temp;
    }

    public void increaseHealth(int amount){
        health += amount;

        if(health >= 100.0f){
            health = 101.0f;
        }
    }

    public void DisplayMessage(string msg, float time){
        StartCoroutine(Message(msg, time));
    }
    public IEnumerator Message(string msg, float time){
        UIMessage.SetActive(true);
        UIMessage.GetComponent<TextMeshProUGUI>().text = msg;
        yield return new WaitForSeconds(time);
        UIMessage.SetActive(false);
    }
    public void HideMessage(){
        UIMessage.SetActive(false);
    }

    public IEnumerator EndCutscene(){

        fadeToBlack.SetActive(true);

        yield return new WaitForSeconds(1);

        fadeToBlack.SetActive(false);

        mainCam.SetActive(false);

        otherCam.SetActive(true);

        fadeIn.SetActive(true);

        nathan.SetActive(true);

        GameObject.Find("Elevator").GetComponent<Animator>().Play("elevator");

        yield return new WaitForSeconds(3);

        GameObject.Find("Elevator").GetComponent<Animator>().Play("elevatorR");

        yield return new WaitForSeconds(2);

        fadeToBlack.SetActive(true);
    }
}
