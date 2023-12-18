using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class spawn_cars : MonoBehaviour
{
    public GameObject car;
    public GameObject[] initialPoints; 
    public float respawnTime=1;
    public float Timer=1;

    // Update is called once per frame
    void Update()
    {
        Timer-=Time.deltaTime;
        if(Timer<0){
            Timer=respawnTime;
            GameObject c=Instantiate(car,transform);
            c.GetComponent<pathFollow>().FirstPoint=initialPoints[Random.Range(0,initialPoints.Length)];
            c.GetComponent<pathFollow>().speed=Random.Range(0.7f,1.2f);
        }
    }

    
}
