using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrialAgent : MonoBehaviour
{
    actionValue1 a;

    public double[] state;



    public int stateSize;
    public int actionSize;

    // Start is called before the first frame update

    void Start(){
        a=new actionValue1(stateSize,actionSize);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            double[] st=new double[stateSize];
            for (int i=0;i<stateSize;i++){
                st[i]=state[i];
            }
            a.get_action(st);
            a.set_Action(st, 1, 5);

            Debug.Log("saved " +a.qtl());
            //Debug.Log(a.qtl()+ "  -  " +a.get_S_state(state) + "  -   " + a.get_num_act(state) + "   -  " + a.get_S_action(state));
        }

        

        if (Input.GetKeyDown(KeyCode.L)){
            a.LoadQTable("qTable.dat");
        }
    }

    public actionValue1 get_qtable(){
        return a;
    }


}
