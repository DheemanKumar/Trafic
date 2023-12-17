using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class driver : MonoBehaviour
{
    bool car;
    bool tlight;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("car") ){
            car=true;
        }
        if (col.gameObject.CompareTag("light") ){
            tlight=true;
        }

        if(car || tlight)
        transform.parent.GetComponent<pathFollow>().Break=true;
        

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("car")  ){
            car=false;
        }
        if (col.gameObject.CompareTag("light") ){
            tlight=false;
        }

        if(!car && !tlight)
        transform.parent.GetComponent<pathFollow>().Break=false;
    }



    
}