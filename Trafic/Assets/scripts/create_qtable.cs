using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class create_qtable : MonoBehaviour
{

    public int stateSize;
    public int actionSize;

    actionValue1 av;
    // Start is called before the first frame update
    void Start()
    {
        av = new actionValue1(stateSize, actionSize);
        string filePath = "qTable.dat";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            av.LoadQTable(filePath);
        }
        string final_table = "";

        for (int i = 0; i < av.qTable.Count; i++)
        {
            string state = "{ ";
            for (int j = 0; j < av.qTable[i].get_state().Length; j++)
            {
                state += av.qTable[i].get_state()[j].ToString() + " ";
            }
            state += "}";
            final_table += state + " \n";

            string sum = "";
            string count = "";
            string Qvalue = "";
            for (int j = 0; j < av.qTable[i].getSum().Length; j++)
            {
                sum += av.qTable[i].getSum()[j].ToString() + " ";
                count += av.qTable[i].getcount()[j].ToString() + " ";
                Qvalue += av.qTable[i].get_Qvalues()[j].ToString() + " ";

            }





            final_table += "  sum     -> { " + sum + " } \n";
            final_table += "  count   -> { " + count + " } \n";
            final_table += "  Qvalues -> { " + Qvalue + " } \n";


            final_table += "\n";
        }

        SaveData(final_table);

    }

    public void SaveData(string data)
    {
        // Specify the path to your file
        string sfilePath = "savedData.txt";

        // Write the data to the file
        File.WriteAllText(sfilePath, data);

        Debug.Log("Data saved to: " + sfilePath);
    }

}
