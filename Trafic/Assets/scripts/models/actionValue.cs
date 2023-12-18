using System;
using System.Linq;

public class QLearningAgent
{
    private int stateSize;
    private int actionSize;
    private double[,] qTable;
    private double learningRate;
    private double discountFactor;
    private double explorationProbability;

    public QLearningAgent(int stateSize, int actionSize, double learningRate = 0.1, double discountFactor = 0.9, double explorationProbability = 0.1)
    {
        this.stateSize = stateSize;
        this.actionSize = actionSize;
        this.learningRate = learningRate;
        this.discountFactor = discountFactor;
        this.explorationProbability = explorationProbability;

        // Initialize Q-table
        qTable = new double[stateSize, actionSize];
    }

    public int SelectAction(int state)
    {
        // Exploration-exploitation trade-off
        if (new Random().NextDouble() < explorationProbability)
        {
            return new Random().Next(actionSize); // Explore
        }
        else
        {
            // Exploit
            double[] qValues = new double[actionSize];
            for (int a = 0; a < actionSize; a++)
            {
                qValues[a] = qTable[state, a];
            }

            return Array.IndexOf(qValues, qValues.Max());
        }
    }

    public void UpdateQTable(int state, int action, double reward, int nextState)
    {
        // Q-value update using the action value method
        double bestNextActionValue = Array.IndexOf(qTable, nextState == 8 ? 1 : 0);
        double currentQValue = qTable[state, action];

        double newQValue = (1 - learningRate) * currentQValue +
                           learningRate * (reward + discountFactor * bestNextActionValue);

        qTable[state, action] = newQValue;
    }
}
