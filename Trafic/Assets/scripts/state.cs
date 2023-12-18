using Unity.VisualScripting;
using UnityEngine;

public class state : MonoBehaviour
{
    public int count=0;


    void OnTriggerEnter2D(Collider2D collider){
        if (collider.CompareTag("car")) count+=1;  
    }

    void OnTriggerExit2D(Collider2D collider){
        if (collider.CompareTag("car")) count-=1;  
    }

}
