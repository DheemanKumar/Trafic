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
        public List<double[]> allsums;
        public List<double[]> allcount;
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
            allsums = new List<double[]>();

            allcount = new List<double[]>();
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
            int index = Array.IndexOf(actions, 1);
            Sum[index] += reward;
            count[index] += 1;

            // {actions[0],actions[1],actions[2],actions[3]}
            allsums.Add(new double[]{Sum[0],Sum[1],Sum[2],Sum[3]});
            allcount.Add(new double[]{actions[0],actions[1],actions[2],actions[3]});

        }

        public void addAction(double[] oldSum, double[] oldCount)
        {
            for (int i = 0; i < Sum.Length; i++)
            {
                Sum[i] += oldSum[i];
                count[i] += oldCount[i];

                // allsums.Add(new double[]{Sum[0],Sum[1],Sum[2],Sum[3]});
                // allcount.Add(new double[]{count[0],count[1],count[2],count[3]});
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
            qTable.Add(s);
            if (random.NextDouble() < explorationProbability)
            {
                return random.Next(0, actionSize);

            }
            else
            {
                double[] qvalue = s.get_Qvalues();
                return Array.IndexOf(qvalue, qvalue.Max());
            }
        }
        else
        {

            if (random.NextDouble() < explorationProbability)
            {
                return random.Next(0, actionSize);

            }
            else
            {
                double[] qvalue = qTable[index].get_Qvalues();
                int Q = Array.IndexOf(qvalue, qvalue.Max());

                return Q;
            }
        }
    }

    public int qtl()
    {
        return qTable.Count;
    }

    public void set_Action(double[] statetable, int action, int reward)
    {
        State s = new State(stateSize, actionSize);
        s.setState(statetable);
        int index = stateIndex(s);

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

    public void SaveQTablehistory(string fileName)
    {
        string lines = "";
        for (int i = 0; i < qTable.Count; i++)
        {
            string st = "";
            //UnityEngine.Debug.Log(qTable[i].get_state().Length);
            for (int j = 0; j < qTable[i].get_state().Length; j++)
            {
                //UnityEngine.Debug.Log(j);
                st += qTable[i].get_state()[j].ToString() + " ";
            }
            lines += st + "\n";

            List<double[]> alls;
            alls = qTable[i].allsums;
            List<double[]> allc;
            allc = qTable[i].allcount;
            for (int j = 0; j < alls.Count; j++)
            {
                string qv = "   -"+j+"> s {";
                //UnityEngine.Debug.Log(all[j]);
                for (int k = 0; k < alls[j].Length; k++)
                {
                    qv += alls[j][k].ToString() + "  ";
                }
                qv+="}      c {";
                for (int k = 0; k < allc[j].Length; k++)
                {
                    qv += allc[j][k].ToString() + "  ";
                }
                lines+=qv+"}\n";
            }

            lines += "\n";

        }
        File.WriteAllText(fileName, lines);
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
            score.ResetGameData();
        }
        //UnityEngine.Debug.Log(File.Exists(fileName));
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