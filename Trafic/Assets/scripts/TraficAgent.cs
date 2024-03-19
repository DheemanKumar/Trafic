using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEditor;
using System;

public class TraficAgent0
    : MonoBehaviour
{

    actionValue1 av;
    public int stateSize;
    public int actionSize;
    public traficLight traficLight;
    public state[] states;

    public int initialcars;
    public int finalcars;

    public int rew;

    public int action;
    public double[] state;

    public int total_states=0;

    int ac;

    bool weight;

    // Start is called before the first frame update
    void Start()
    {
        traficLight=GetComponent<traficLight>();
        av=new actionValue1(stateSize,actionSize);
        states=new state[transform.childCount];

        stateSize=transform.childCount;

        for (int i=0;i<transform.childCount;i++){
            states[i]=transform.GetChild(i).GetComponent<state>();
        }

        state=new double[stateSize];
        takeaction();

        traficLight.setlight(av.get_action(state));
        ac=0;

        weight=false;
        Time.timeScale=10;
    }


    public float timer = 0f;
    public float updateInterval = 10f;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.L)){
            av.LoadQTable("qTable.dat");
        }
        
        timer += Time.deltaTime;

        // Check if the timer has reached the desired interval
        if (timer >= updateInterval*0.1 && !weight)
        {
            takeaction();
            weight=true;
            initialcars=FindSum(state);
            //Debug.Log(initialcars);
        }
        if (timer >= updateInterval && weight)
        {
            getreward();
            weight=false;
            timer=0;
        }


        if (Input.GetKeyDown(KeyCode.S)){
            av.SaveQTablehistory("qTableHistory.txt");
        }

        total_states=av.qtl();

        

    }    // Update is called once per frame
   

    void takeaction(){
        //action=traficLight.state;
        state=new double[4];
        for (int i=0;i<transform.childCount;i++){
            state[i]=states[i].count;
        }

        ac=av.get_action(state);
        traficLight.setlight(ac);
        

    }

    void getreward(){

        double[] newstate=new double[4];
        for (int i=0;i<transform.childCount;i++){
            newstate[i]=states[i].count;
        }

        finalcars=FindSum(newstate);
            //Debug.Log(initialcars+"   "+finalcars);
            
        if(initialcars==0 && finalcars==0) rew=0;
        else if(initialcars==finalcars) rew=-finalcars;
        else rew=initialcars-finalcars;

        
        av.set_Action(state,traficLight.state,rew);
        //Debug.Log(state[0]+" "+state[1]+" "+state[2]+" "+state[3]+"     "+traficLight.state+"   "+(rew));
    }


    int FindSum(double[] array)
    {
        int sum = 0;

        foreach (int number in array)
        {
            
            sum += number;
        }

        return sum;
    }

     public actionValue1 get_qtable(){
        return av;
    }
}
