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
    }
    void Update()
    {
        health -= healthDecreaseSpeed * Time.deltaTime;
        healthText.text = "Battery: " + (int)health + "%";

      if(health < 0){
        
        DisplayMessage("Game Over",2);
        Time.timeScale = 0;
      }
    }

    public void bodyPartCollected(string bodyPart){
        if(bodyParts.ContainsKey(bodyPart)){
            bodyParts[bodyPart] = true;
        }
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
}
