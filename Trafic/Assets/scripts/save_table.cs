using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save_table : MonoBehaviour
{

    actionValue1 afinal;
    actionValue1 a;

    public TraficAgent[] traficAgents;
    public int count;

    public int stateSize;
    public int actionSize;


    // Start is called before the first frame update
    void Start()
    {
        afinal = new actionValue1(stateSize, actionSize);
        traficAgents =new TraficAgent[count];
        for(int i=0;i<count;i++){
            traficAgents[i]=transform.GetChild(i).GetComponent<TraficAgent>();
        }
    }

    public void save()
    {
        for (int i = 0; i < traficAgents.Length; i++)
        {
            a = traficAgents[i].get_qtable();
            afinal.add_qtable(a.qTable);
        }
        afinal.SaveQTable("qTable.dat");
    }



}
