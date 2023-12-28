using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class TraficAgent : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        traficLight=GetComponent<traficLight>();

        av=new actionValue1(stateSize,actionSize);
        string filePath = "qTable.dat";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            av.LoadQTable(filePath);
        }

        states=new state[transform.childCount];

        stateSize=transform.childCount;

        for (int i=0;i<transform.childCount;i++){
            states[i]=transform.GetChild(i).GetComponent<state>();
        }

        state=new double[stateSize];
        initialcars=FindSum(state);

        traficLight.setlight(av.get_action(state));
        ac=0;


    }


    public float timer = 0f;
    public float updateInterval = 10f;

    private void Update()
    {

        
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            // Your update logic goes here
            calculate();
            // Reset the timer
            timer = 0f;
        }


        total_states=av.qtl();

        

    }    // Update is called once per frame
    void calculate()
    {
        //Debug.Log("length  "+av.qtl());
        finalcars=FindSum(state);

        //Debug.Log("difference  "+initialcars.ToString()+"  "+finalcars.ToString());

        av.set_Action(state,traficLight.state,initialcars-finalcars);
        rew=initialcars-finalcars;


        action=traficLight.state;

        Debug.Log("state  "+state[0]+" "+state[1]+" "+state[2]+" "+state[3]);

        //Debug.Log(ac+"  -  "+av.get_S_state(state)+"  -   "+ av.get_num_act(state) +"   -  "+av.get_S_action(state));

        state=new double[4];
        for (int i=0;i<transform.childCount;i++){
            state[i]=states[i].count;
        }

        ac=av.get_action(state);
        //Debug.Log(ac);
        traficLight.setlight(ac);
        //Debug.Log(av.get_S_state(state)+"  -   "+ av.get_num_act(state) +"   -  "+av.get_S_action(state));

        //av.set_Action(state,1,5);

        initialcars=finalcars;
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
