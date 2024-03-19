using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraficAgent : MonoBehaviour
{
    public UnityToPython COMMUNICATOR;


    void Start()
    {
        string signal = "2inc";
        COMMUNICATOR.sendToPython(signal);
    }


    public void StoreAction(double[] state,int action, int reward)
    {
        //Debug.Log("s");
        string signal = "0";
        for (int i=0; i < 4; i++)
        {
            if (state[i] < 0 || state[i] > 9) return;
            signal += state[i].ToString();
        }
        if (action < 0 || action > 9) return;
        signal += action.ToString();

        if (reward > -100) signal += (100 + reward).ToString();
        else signal += 0.ToString();

        string message = COMMUNICATOR.sendToPython(signal);



        //Debug.Log("/s");
        //Debug.Log(message);
    }

    public int getAction(double[] state)
    {
        string signal = "1";
        for (int i = 0; i < 4; i++)
        {
            if (state[i] < 0 || state[i] > 9) return -1;
            signal += state[i].ToString();
        }

        string message = COMMUNICATOR.sendToPython(signal);



        //Debug.Log("action " +(message));
        //Debug.Log("action " + int.Parse( message));

        return int.Parse(message);
    }
}
