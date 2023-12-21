using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

public class actionValue
{
    private int stateSize;
    private int actionSize;
    private double[,] qTable;
    //private double learningRate;
    //private double discountFactor;
    private double explorationProbability;

    public actionValue(int stateSize, int actionSize, double explorationProbability = 0.1)
    {
        this.stateSize = stateSize;
        this.actionSize = actionSize;
        //this.learningRate = learningRate;
        //this.discountFactor = discountFactor;
        this.explorationProbability = explorationProbability;

        // Initialize Q-table
        qTable = new double[stateSize, actionSize];

    }

    public double[,] getqtable(){
        return qTable;
    }

    public void UpdateQTable(int state, int action, double reward, int nextState)
    {
        // Q-value update using the action value method
        double bestNextActionValue = GetMaxQValue(nextState);
        double currentQValue = qTable[state, action];

        double newQValue = (currentQValue + bestNextActionValue+reward);

        qTable[state, action] = newQValue;
    }

    public double GetMaxQValue(int state)
    {
        // Get the maximum Q-value for the next state
        double maxQValue = double.MinValue;
        for (int a = 0; a < actionSize; a++)
        {
            double qValue = qTable[state, a];
            if (qValue > maxQValue)
            {
                maxQValue = qValue;
            }
        }
        return maxQValue;
    }

    public string getMaxQValue(int state)
    {
        // Get the maximum Q-value for the next state
        string  ans= "";
        double maxQValue = double.MinValue;
        for (int a = 0; a < actionSize; a++)
        {
            double qValue = qTable[state, a];
            if (qValue > maxQValue)
            {
                maxQValue = qValue;
            }
            ans+=qValue.ToString();
            ans+=" ";
        }
        return ans;
    }
}
