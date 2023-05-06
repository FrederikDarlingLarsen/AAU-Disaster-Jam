using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    public int healthIncreaseAmount;
    private StatsManagerScript stats;
    void Start()
    {
        stats = GameObject.Find("StatsManagerObject").GetComponent<StatsManagerScript>();
    }
    public void OnTriggerEnter(Collider other){
        stats.increaseHealth(healthIncreaseAmount);
        Destroy(gameObject);
    }
}
