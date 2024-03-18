using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiScore : MonoBehaviour
{
    public TraficAgent1 ta;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Score :" + (100-ta.fault).ToString("#.##");
    }
}
