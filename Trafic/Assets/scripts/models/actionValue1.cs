using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.IO;

using System.Runtime.Serialization.Formatters.Binary;


public class actionValue1
{
    public List<State> qTable;
    private int stateSize;
    private int actionSize;
    private double explorationProbability;

    [System.Serializable]
    public class State
    {

        private double[] statetable;
        private int actionSize;
        double[] Sum;
        double[] count;

        public State(int stateSize, int actionSize)
        {
            this.actionSize = actionSize;
            statetable = new double[stateSize];

            Sum = new double[actionSize];
            count = new double[actionSize];
        }

        public double[] get_state()
        {
            return statetable;
        }

        public void setState(double[] state)
        {
            this.statetable = state;
        }

        public void addAction(double[] actions, int reward)
        {
            string ans="";
            for (int i=0;i<actions.Length;i++)
                ans+=actions[i].ToString()+" ";
            UnityEngine.Debug.Log("actions   "+ans);
            UnityEngine.Debug.Log("reward    "+reward.ToString());

            int index = Array.IndexOf(actions, 1);
            Sum[index] += reward;
            count[index] += 1;

            ans="";
            for (int i=0;i<Sum.Length;i++)
                ans+=Sum[i].ToString()+" ";
            UnityEngine.Debug.Log("sum      "+ans);

            ans="";
            for (int i=0;i<count.Length;i++)
                ans+=count[i].ToString()+" ";
            UnityEngine.Debug.Log("count    "+ans);
            // //UnityEngine.Debug.Log(Sum[0].ToString()+"    len");

        }

        public void addAction(double[] oldSum, double[] oldCount)
        {
            for (int i = 0; i < Sum.Length; i++)
            {
                Sum[i] += oldSum[i];
                count[i] += oldCount[i];
            }
        }

        public double[] getSum()
        {
            return Sum;
        }
        public double[] getcount()
        {
            return count;
        }

        public double[] get_Qvalues()
        {
            double[] Qvalues = new double[actionSize];

            //UnityEngine.Debug.Log("action size   "+Actions.Count.ToString());

            for (int i = 0; i < actionSize; i++)
            {
                if (count[i] == 0)
                {
                    Qvalues[i] = 0;
                }
                else
                {
                    Qvalues[i] = Sum[i] / count[i];
                }
            }

            return Qvalues;
        }
    }



    public actionValue1(int stateSize, int actionSize, double explorationProbability = 0.33)
    {
        this.explorationProbability = explorationProbability;
        this.stateSize = stateSize;
        this.actionSize = actionSize;

        // Initialize Q-table
        qTable = new List<State>();
    }


    public int get_action(double[] statetable)
    {
        Random random = new Random();
        State s = new State(stateSize, actionSize);

        s.setState(statetable);
        int index = stateIndex(s);

        if (index == -1)
        {
            //add state to qtable
            qTable.Add(s);
            //UnityEngine.Debug.Log("state added "+qTable.Count);
            if (random.NextDouble() < explorationProbability)
            {
                //UnityEngine.Debug.Log("random   ");
                return random.Next(0, actionSize);

            }
            else
            {
                double[] qvalue = s.get_Qvalues();
                string q = "";
                for (int i = 0; i < qvalue.Length; i++)
                {
                    q += qvalue[i].ToString() + " ";
                }
                //UnityEngine.Debug.Log("new   "+q);
                return Array.IndexOf(s.get_Qvalues(), s.get_Qvalues().Max());
            }
        }
        else
        {

            if (random.NextDouble() < explorationProbability)
            {
                //UnityEngine.Debug.Log("random   ");
                return random.Next(0, actionSize);

            }
            else
            {
                double[] qvalue = qTable[index].get_Qvalues();

                string ans = "";
                for (int i=0;i<qvalue.Length;i++)
                    ans+=qvalue[i].ToString()+" ";
                UnityEngine.Debug.Log("qvalues   "+ans);

                // string q = "";
                // for (int i = 0; i < qvalue.Length; i++)
                // {
                //     q += qvalue[i].ToString() + " ";
                // }
                int Q=Array.IndexOf(qvalue, qvalue.Max());
                UnityEngine.Debug.Log("Q   "+Q);
                return Q;
            }
            //extract the state from qtable
        }
    }

    public int qtl()
    {
        return qTable.Count;
    }

    public void set_Action(double[] statetable, int action, int reward)
    {
        State s = new State(stateSize, actionSize);
        //UnityEngine.Debug.Log("sa  "+statetable[0]+" "+statetable[1]+" "+statetable[2]+" "+statetable[3]);
        s.setState(statetable);
        int index = stateIndex(s);
        //UnityEngine.Debug.Log("index  "+index+"   size "+qTable.Count);

        double[] actions = new double[actionSize];

        for (int i = 0; i < actionSize; i++)
        {
            if (i == action)
            {
                actions[i] = 1;
                break;
            }
        }
        qTable[index].addAction(actions, reward);
    }

    private int stateIndex(State state)
    {
        for (int i = 0; i < qTable.Count; i++)
        {

            if (compareState(qTable[i].get_state(), state.get_state()))
            {

                return i;
            }

        }
        return -1;
    }

    private bool compareState(double[] state1, double[] state2)
    {
        for (int i = 0; i < stateSize; i++)
        {

            if (state1[i] != state2[i])
            {
                return false;
            }
        }

        return true;
    }


    public void SaveQTable(string fileName)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(fileName);
        binaryFormatter.Serialize(fileStream, qTable);
        fileStream.Close();


        UnityEngine.Debug.Log("qTable saved to " + fileName);


    }


    public void LoadQTable(string fileName)
    {
        if (File.Exists(fileName))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(fileName, FileMode.Open);
            qTable = (List<State>)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            UnityEngine.Debug.Log("qTable loaded from " + fileName);
        }
        else
        {
            UnityEngine.Debug.LogWarning("File not found: " + fileName);
        }
    }

    public void add_qtable(List<State> Table)
    {
        for (int i = 0; i < Table.Count; i++)
        {
            int index = stateIndex(Table[i]);
            if (index == -1)
            {
                qTable.Add(Table[i]);
            }
            else
            {
                State st = Table[i];
                qTable[index].addAction(st.getSum(), st.getcount());
            }
        }

    }



}