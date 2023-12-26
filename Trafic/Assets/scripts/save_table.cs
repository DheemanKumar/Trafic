using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save_table : MonoBehaviour
{

    actionValue1 afinal;
    actionValue1 a;
    
    public TraficAgent[] t;

public int stateSize;
    public int actionSize;


    // Start is called before the first frame update
    void Start()
    {
        afinal=new actionValue1(stateSize,actionSize);;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)){
            save();
        }
    }

    void save(){
        for (int i=0;i<t.Length;i++){
            a=t[i].get_qtable();
            afinal.add_qtable(a.qTable);
        }
        afinal.SaveQTable("qTable.dat");
    }


     
}
