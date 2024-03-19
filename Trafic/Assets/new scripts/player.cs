using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    /// <summary>
    /// takes an action
    /// weights for some time "interval"
    /// generate reward and set them
    /// repeats the cycle
    /// </summary>

    private int stateSize;
    private traficLight traficLight;
    private state[] states;
    private int initialcars;
    private int action;

    public TraficAgent tr;

    public double[] state;

    public float timer = 0f;
    public float Interval = 10f;

    public float learningRate = 10f;

    // Start is called before the first frame update
    void Start()
    {
        traficLight=GetComponent<traficLight>();   //calls traficLight so it can change the state of the signel
        //store all the four states of the light so that we can access them
        states =new state[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            states[i] = transform.GetChild(i).GetComponent<state>();


        // state will store the number of cars in each side of the signal
        stateSize = transform.childCount;
        state = new double[stateSize];

        takeaction();
        timer = 0;
    }


    
    

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= Interval)
        {
            timer = 0;
            StartCoroutine(next());
        }

    }

    IEnumerator next()
    {
        setreward();
        yield return new WaitForSeconds(Interval*0.1f);
        takeaction();
    }


    void takeaction(){
        timer = 0;
        initialcars = Countcars();


        state = new double[4];
        for (int i = 0; i < transform.childCount; i++)
        {
            state[i] = states[i].count;
        }

        int num = Random.Range(0, 100);
        int ac;

        if (num > learningRate) ac = tr.getAction(state);
        else ac = Random.Range(0, 3);
        traficLight.setlight(ac);

    }

    void setreward(){

        action = traficLight.state;
        int reward = initialcars - Countcars();

        tr.StoreAction(state, action, reward);

        Debug.Log("(" + state[0] + " " + state[1] + " " + state[2] + " " + state[3] + ") " + action + " " + reward);

    }



    int Countcars()
    {
        double[] cars = new double[stateSize];
        for (int i = 0; i < stateSize; i++)
        {
            cars[i] = states[i].count;
        }
        return FindSum(cars);
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

}
