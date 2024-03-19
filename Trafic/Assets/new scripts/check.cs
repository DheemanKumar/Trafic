using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check : MonoBehaviour
{

    public UnityToPython COMMUNICATOR;

    public double[] state;

    public bool get;



    public void Start()
    {
        string signal = "2inc";
        COMMUNICATOR.sendToPython(signal);
        get = false;
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



        return int.Parse(message);
    }



    // Update is called once per frame
    void Update()
    {
        if (get)
        {
            get = false;
            Debug.Log(getAction(state));
        }
    }
}
