using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class environment : MonoBehaviour
{
    public int count;
    public int Rounds = 10;

    private void Start()
    {
        count = 1;
    }


    // Update is called once per frame
    void Update()
    {
        if (count > Rounds)
        {
            count = 1;
            Transform cars= transform.GetChild(2);
            foreach (Transform child in cars)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void nextRound()
    {
        count += 1;
    }
}
