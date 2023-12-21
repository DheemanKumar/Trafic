using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEditor;
using System;

public class TraficAgent1 : MonoBehaviour
{
actionValue1 av;

    public double[] state;



    public int stateSize;
    public int actionSize;

    // Start is called before the first frame update
    void Start()
    {
        av = new actionValue1(stateSize, actionSize);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            calculate();
        }
    }


    void calculate(){
        Debug.Log(av.get_action(state));

            av.set_Action(state, 1, 5);
            Debug.Log(av.get_S_state(state) + "  -   " + av.get_num_act(state) + "   -  " + av.get_S_action(state));
    }


}
