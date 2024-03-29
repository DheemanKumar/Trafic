using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.IO;


public class actionValue
{
    private List<State> qTable;


    private int stateSize;
    private int actionSize;



    private double explorationProbability;

    [System.Serializable]
    private class Action
    {
        private double[] actiontable;
        private int reward;

        public Action(int actionSize)
        {
            actiontable = new double[actionSize];
        }

        public void setAction(double[] actions, int reward)
        {
            this.actiontable = actions;
            this.reward = reward;
        }

        public double[] getAction()
        {
            return actiontable;
        }

        public string getSAction()
        {
            string ans = "";
            for (int i = 0; i < actiontable.Length; i++)
            {
                ans += actiontable[i].ToString();
                ans += " ";
            }
            return ans;
        }

        public int getReward()
        {
            return reward;
        }



    }


    [System.Serializable]
    private class State
    {
        private double[] statetable;
        private int actionSize;
        private List<Action> Actions;

        public State(int stateSize, int actionSize)
        {
            this.actionSize = actionSize;
            statetable = new double[stateSize];
            Actions = new List<Action>();
        }

        public double[] get_state()
        {
            return statetable;
        }

        public List<Action> GetActions()
        {
            return Actions;
        }

        public void setState(double[] state)
        {
            this.statetable = state;
        }

        public void addAction(double[] actions, int reward)
        {
            Action newction = new Action(actionSize);
            newction.setAction(actions, reward);
            Actions.Add(newction);
        }

        public double[] get_Qvalues()
        {
            double[] Qvalues = new double[actionSize];
            double[] Sum = new double[actionSize];
            double[] count = new double[actionSize];


            for (int i = 0; i < Actions.Count; i++)
            {
                Action A = Actions[i];
                double[] a = A.getAction();
                int r = A.getReward();
                int index = Array.IndexOf(a, 1);
                Sum[index] += r;
                count[index] += 1;
            }
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


        // get functions 
        public string[] get_actions()
        {
            string[] ans = new string[Actions.Count];
            for (int i = 0; i < Actions.Count; i++)
            {
                ans[i] = Actions[i].getSAction();
            }
            return ans;
        }

        public string indexa()
        {
            string ans = "";
            for (int i = 0; i < Actions.Count; i++)
            {
                Action A = Actions[i];
                double[] a = A.getAction();
                int r = A.getReward();
                int index = Array.IndexOf(a, 1);
                ans += index.ToString() + " ";
            }
            return ans;
        }

        public string get_reward()
        {
            string ans = "";
            for (int i = 0; i < Actions.Count; i++)
            {
                ans += Actions[i].getReward().ToString();
                ans += " ";
            }
            return ans;
        }

        public string ToCsvString()
        {
            string result = string.Join(",", statetable) + "," + actionSize + "\n";

            foreach (Action action in Actions)
            {
                result += string.Join(",", action.getSAction())+ "\n";
            }

            return result;
        }

        public static State FromCsvString(string csv)
    {
        string[] lines = csv.Split('\n');
        string[] stateData = lines[0].Trim().Split(',');

        int stateSize = stateData.Length - 1;
        int actionSize = int.Parse(stateData[stateData.Length - 1]);

        State newState = new State(stateSize, actionSize);

        for (int i = 1; i < lines.Length - 1; i++)
        {
            string[] actionData = lines[i].Trim().Split(',');
            Action newAction = new Action(actionSize);
            double[] actions=new double[actionSize];

            for (int j = 0; j < actionSize; j++)
            {
                actions[j] = double.Parse(actionData[j]);
            }
            newAction.setAction(actions,1);

            newState.Actions.Add(newAction);
        }

        return newState;
    }

    }


    public actionValue(int stateSize, int actionSize, double explorationProbability = 0.1)
    {
        this.explorationProbability = explorationProbability;
        this.stateSize = stateSize;
        this.actionSize = actionSize;

        // Initialize Q-table
        qTable = new List<State>();
    }

    void Start()
    {
        explorationProbability = 0.1;
        stateSize = 4;
        actionSize = 4;

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
            if (random.NextDouble() > explorationProbability)
            {
                return random.Next(0, actionSize);

            }
            else
                return Array.IndexOf(s.get_Qvalues(), s.get_Qvalues().Max());
        }
        else
        {
            double[] qvalue = qTable[index].get_Qvalues();
            if (random.NextDouble() > explorationProbability)
            {
                return random.Next(0, actionSize);

            }
            else
                return Array.IndexOf(qvalue, qvalue.Max()); ;
            //extract the state from qtable
        }
    }

    public int qtl()
    {
        return qTable.Count;
    }


    public string get_S_action(double[] statetable)
    {
        State s = new State(stateSize, actionSize);
        s.setState(statetable);
        int index = stateIndex(s);

        if (index == -1)
        {
            //add state to qtable
            //qTable.Add(s);
            double[] q = s.get_Qvalues();
            string ans = "";
            for (int i = 0; i < q.Length; i++)
            {
                ans += q[i].ToString();
                ans += " ";
            }
            return ans;
        }
        else
        {
            double[] q = qTable[index].get_Qvalues();
            string ans = "";
            for (int i = 0; i < q.Length; i++)
            {
                ans += q[i].ToString();
                ans += " ";
            }
            return ans;
            //extract the state from qtable
        }

    }


    public string get_S_state(double[] statetable)
    {
        State s = new State(stateSize, actionSize);
        s.setState(statetable);
        int index = stateIndex(s);

        double[] a = qTable[index].get_state();

        string ans = "";
        for (int i = 0; i < a.Length; i++)
        {
            ans += a[i].ToString() + " ";
        }
        return ans;

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

    public int get_num_act(double[] statetable)
    {
        State s = new State(stateSize, actionSize);
        s.setState(statetable);
        int index = stateIndex(s);
        return qTable[index].get_actions().Length;
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


    public string SaveQTable(string fileName)
    {
        string csvContent = "";

        foreach (State state in qTable)
        {
            csvContent += state.ToCsvString();
        }

       return csvContent;
    }


    public void LoadQTable(string fileName)
    {
        if (File.Exists(fileName))
        {
            string csvContent = File.ReadAllText(fileName);
            string[] stateStrings = csvContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            

            UnityEngine.Debug.Log("qTable loaded from " + stateStrings[1]);
        }
        else
        {
            UnityEngine.Debug.LogWarning("File not found: " + fileName);
        }
    }


    public void extjason(){
        UnityEngine.Debug.Log(qTable[0].ToCsvString());
    }


}