using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkumScript : MonoBehaviour
{
    private int speed = 2;
    private float timer;
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        transform.position += transform.right*speed*Time.deltaTime;

        transform.localScale -= Vector3.one*(speed/2)*Time.deltaTime;

        if(timer>1.3f){
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Fire")){
            Destroy(other.gameObject);
        }
    }
}
