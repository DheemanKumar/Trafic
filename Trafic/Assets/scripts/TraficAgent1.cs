using UnityEngine;

public class TraficAgent1 : MonoBehaviour
{
    actionValue1 av;

    public int stateSize;
    public int actionSize;

    public float scale; //

    public int Q_Table_Length=0;

    public bool IsResult=false;

    public float timer = 0;
    public float maxTime = 60;


    public float rtimer = 0;
    public float rmaxTime = 5;

    public float epoch;

    public Transform cars;
    public int fault=0;

    public float ep=0.25f;

    // Start is called before the first frame update
    void Start()
    {
        av = new actionValue1(stateSize, actionSize,ep);
        av.LoadQTable("qTable.dat");

        epoch = score.epoch;
        fault = 0;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.S)){
            savedata();
        }


        Time.timeScale = scale;
        timer += Time.deltaTime;
        rtimer += Time.deltaTime;

        if (!IsResult)
        {

            if (timer >= maxTime) createnew();
        }
        else
        {
            if (rtimer >= rmaxTime)
            {
                fault += cars.childCount;
                rtimer = 0;
            }


            if (timer >= maxTime)
            {
                timer = 0;
                scale = 0;
            }
        }

    }




    public int takeaction(double[] state)
    {
        Q_Table_Length=av.qtl();
        return av.get_action(state);
    }

    public void getreward(double[] state, int action, int reward)
    {
        if(!IsResult) av.set_Action(state, action, reward);
    }


    private void createnew()
    {
        score.epoch += 1;
        score.SaveGameData();
        savedata();
        GetComponent<restart>().Restart();
    }

    public void savedata(){
        av.SaveQTablehistory("qTableHistory.txt");
        av.SaveQTable("qTable.dat");
    }

}
