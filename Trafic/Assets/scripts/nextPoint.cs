using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class nextPoint : MonoBehaviour
{
    public GameObject[] next_points;
    public Transform[] curves;

    public (GameObject, Transform) get_next_point()
    {
        int index = Random.Range(0, next_points.Length);

        //Debug.Log(index);
        return (next_points[index], curves[index]);
    }


}

