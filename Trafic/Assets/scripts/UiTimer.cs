using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiTimer : MonoBehaviour
{
    public TraficAgent1 ta;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Timer :" + (ta.maxTime-ta.timer).ToString("#.##");
    }
}
