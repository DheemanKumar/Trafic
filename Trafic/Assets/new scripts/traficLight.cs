using UnityEngine;

public class traficLight : MonoBehaviour
{
    /// <summary>
    /// changes the state of all the trafic light
    /// </summary>
    public int state=0;

    void Start(){
        shift_light();
    }
    
    void OnMouseOver()
    {
        //change the lights when c is pressed
        if (Input.GetKeyDown(KeyCode.C)){ 
            state+=1;
            state=state%(transform.childCount);
            shift_light();
            
        }
    }

    void OnMouseDown()
    {
        //change the lights when mouse is pressed
        state += 1;
        state = state % (transform.childCount);
        shift_light();
    }




    public void setlight(int state){
        //set trafic light of "state" to green
        this.state=state;
        shift_light();
    }

    void shift_light(){
        //sets its neext signel to green
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

    

    

}
