
using Unity.VisualScripting;
using UnityEngine;

public class traficLight : MonoBehaviour
{


    public int state=0;

    void Start(){
        shift_light();
    }
    
    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.C)){
            state+=1;
            state=state%(transform.childCount);
            shift_light();
            
        }
    }

    void shift_light(){
        for (int i=0;i<transform.childCount;i++){
                if (i==state){
                    transform.GetChild(i).GetComponent<SpriteRenderer>().color=new Vector4(0,1,0,0.3f);
                    transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                    
                }
                else{
                    transform.GetChild(i).GetComponent<SpriteRenderer>().color=new Vector4(1,0,0,0.3f);
                    transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                }
            
        }
    }

    void OnMouseDown()
    {
        state+=1;
        state=state%(transform.childCount);
        shift_light();
    }

    

}
