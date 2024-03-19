using Unity.VisualScripting;
using UnityEngine;

public class state : MonoBehaviour
{
    /// <summary>
    /// This script counts the number of cars in the range of the trafic light of one side.
    /// </summary>
    public int count=0;

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.CompareTag("car")) count+=1;  
    }

    void OnTriggerExit2D(Collider2D collider){
        if (collider.CompareTag("car")) count-=1;  
    }
}
